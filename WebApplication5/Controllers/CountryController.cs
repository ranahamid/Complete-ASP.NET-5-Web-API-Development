using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication5.IRepository;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [Route("api/Country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IUnitofWork _unitOfWork; 
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;
              
        public CountryController(IUnitofWork unitOfWork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper= mapper; 
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _unitOfWork.Countries.GetAll();
                var result = _mapper.Map<IList<CountryDTO>>(countries);

                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}");
                return StatusCode(500,"Internal Server Error. Please try again later.");
            }
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                var country = await _unitOfWork.Countries.Get(x=>x.Id==id, new List<string> { "Hotels"});
                var result = _mapper.Map<CountryDTO>(country);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetCountry)}");
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
        }
    }
}
