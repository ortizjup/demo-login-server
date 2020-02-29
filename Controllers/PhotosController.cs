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
    }
}
