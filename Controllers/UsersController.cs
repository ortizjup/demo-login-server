using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
    }
}
