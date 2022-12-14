using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        private readonly IConfiguration _config;

        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _config = config;
            _repo = repo;
            _mapper = mapper;
        }

        // POST: api/Auth/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if (await _repo.UserExist(userForRegisterDto.UserName))
                return BadRequest("Username already exists");

            var newUser = new User
            {
                Email = userForRegisterDto.Email,
                UserName = userForRegisterDto.Email,
                Adress = userForRegisterDto.Adress,
                Adress2 = userForRegisterDto.Adress2,
                Zip = userForRegisterDto.Zip,
                Phone = userForRegisterDto.Phone,
                CityId = userForRegisterDto.City.Id,
                CountryId = userForRegisterDto.Country.Id,
                StateId = userForRegisterDto.State.Id,
                Gender = userForRegisterDto.Gender,
                KnownAs = userForRegisterDto.KnownAs,
                DateOfBirth = userForRegisterDto.DateOfBirth
            };

            var userToCreate = await _repo.RegisterUser(newUser, userForRegisterDto.Password);

            return CreatedAtRoute("GetUser", new { contoller="Users", 
                                                   id= userToCreate.Id},
                                                   _mapper.Map<UserForDetailDto>(userToCreate));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(double.Parse(_config.GetSection("AppSettings:Expires").Value)),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });
        }


    }
}