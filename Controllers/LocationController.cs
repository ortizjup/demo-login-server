using AutoMapper;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        
        private readonly IGenericRepository _genRepo;

        private IMapper _mapper;

        public LocationController(IGenericRepository genRepo, IMapper mapper)
        {
            _genRepo = genRepo;
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            return Ok(await _genRepo.GetCities());
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            return Ok(await _genRepo.GetCountries());
        }

        [HttpGet("states")]
        public async Task<IActionResult> GetStates()
        {
            return Ok(await _genRepo.GetStates());
        }
    }
}
