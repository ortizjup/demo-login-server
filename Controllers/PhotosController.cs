using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IGenericRepository _genericRepository;

        private readonly IMapper _mapper;

        private readonly IOptions<CloudinarySettings> _cloudinarySettings;

        private readonly Cloudinary _cloudinary;

        public PhotosController(IGenericRepository genericRepository
                                , IMapper mapper
                                , IOptions<CloudinarySettings> cloudinarySettings)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _cloudinarySettings = cloudinarySettings;

            Account acc = new Account(
                 _cloudinarySettings.Value.CloudinaryName,
                 _cloudinarySettings.Value.ApiKey,
                 _cloudinarySettings.Value.ApiSecret
                );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name="GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _genericRepository.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _genericRepository.GetUser(userId);

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if(file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            else
            {
                return BadRequest("Please provide a photo!");
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            Photo photo = new Photo();

            photo.DateAdded = photoForCreationDto.DateAdded;
            photo.Description = photoForCreationDto.Description;
            photo.PublicId = photoForCreationDto.PublicId;
            photo.Url = photoForCreationDto.Url;
          
            if (!userFromRepo.Photos.Any())
            {
                photo.IsMain = true;
            }

            userFromRepo.Photos.Add(photo);

            if(await _genericRepository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);

                return CreatedAtRoute("GetPhoto", new { userId, id = photo.Id }, photoToReturn);
            }

            return BadRequest("Error while adding photo!");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _genericRepository.GetUser(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _genericRepository.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("This is already the main photo");

            var currentMainPhoto = user.Photos.Where(w => w.IsMain).FirstOrDefault();
            currentMainPhoto.IsMain = false;

            photoFromRepo.IsMain = true; 
            
            if(await _genericRepository.SaveAll())
            {
                var usersPhotos = await _genericRepository.GetUser(userId);

                return Ok(usersPhotos.Photos);
            }

            return BadRequest("Couldn't set the current photo to main photo!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _genericRepository.GetUser(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var photoFromRepo = await _genericRepository.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("This is the main photo please change it and then delete it!");

            if (!string.IsNullOrEmpty(photoFromRepo.PublicId))
            {
                var cloudinaryDeleteParams = new DeletionParams(photoFromRepo.PublicId);

                var result = _cloudinary.Destroy(cloudinaryDeleteParams);

                if (result.Result.ToLower() == "ok")
                {
                    _genericRepository.Delete(photoFromRepo);
                }
            }

            if (string.IsNullOrEmpty(photoFromRepo.PublicId))
            {
                _genericRepository.Delete(photoFromRepo);
            }

            if (await _genericRepository.SaveAll())
                return Ok();
            
            return BadRequest("Fail to delete photo!");
        }
    }
}
