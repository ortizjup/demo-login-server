using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGenericRepository _genRepo;

        private IMapper _mapper;

        public UsersController(IGenericRepository genRepo, IMapper mapper)
        {
            _mapper = mapper;
            _genRepo = genRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromrepo = await this._genRepo.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = userFromrepo.Gender == "male" ? "female" : "male";
            }
            var users = await _genRepo.GetUsers(userParams);

            var userForListDto = _mapper.Map<IEnumerable<UserForListDto>>(users);

            //The pagination information is being retrieve to the client in the headers not in the user object itself.
            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(userForListDto);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _genRepo.GetUser(id);

            var userForDetailDto = _mapper.Map<UserForDetailDto>(user);

            return Ok(userForDetailDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserForUpdateDto userDto)
        {
            if (userDto.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _genRepo.GetUser(userDto.Id);

            userFromRepo.Interests = userDto.Interests;
            userFromRepo.Introduction = userDto.Introduction;
            userFromRepo.LookingFor = userDto.LookingFor;
            userFromRepo.CityId = userDto.City.Id;
            userFromRepo.CountryId = userDto.Country.Id;
            userFromRepo.StateId = userDto.State.Id;

            if (await _genRepo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {userFromRepo.Id} failed on save");
        }
    }
}
