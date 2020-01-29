using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
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
        public async Task<IActionResult> GetUsers()
        {
            var users = await _genRepo.GetUsers();

            var userForListDto = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(userForListDto);
        }

        [HttpGet("{id}")]
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

            _mapper.Map(userDto, userFromRepo);

            if (await _genRepo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {userFromRepo.Id} failed on save");
        }
    }
}
