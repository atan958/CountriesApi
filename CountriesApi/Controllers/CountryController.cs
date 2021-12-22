using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using CountriesApi.Models;
using CountriesApi.Utilities;

namespace CountriesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly IDataService dataService;

        public CountryController(IDataService dataService)
        {
            this.dataService = dataService;
            Console.WriteLine("Creating Country Controller");
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<Country>> GetAll([FromQuery] int count = 5)
        {
            return Ok(dataService.GetAll().Take(count));
        }
        
        [HttpGet("default")]
        public ActionResult<Country> Get(string id)
        {
            var country = DbQueryManager.GetA(id);
            if (country is null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpPost("default")]
        public IActionResult Post([FromBody] Country country)
        {
            // Can perform checks to see if format of inputted object is correct

            DbQueryManager.Post(country);
            return Ok(country);
        }

        [HttpPut("default")]
        public IActionResult Put([FromQuery] string id, [FromBody] Country country)
        {
            var updateCountry = DbQueryManager.GetA(id);
            if (updateCountry is null)
            {
                return NotFound();
            }
            DbQueryManager.Put(id, country);
            return Ok(country);
        }

        [HttpDelete("default")]
        public IActionResult Delete([FromQuery] string id)
        {
            Country deleteCountry = DbQueryManager.GetA(id);
            if (deleteCountry is null)
            {
                return BadRequest();
            }

            DbQueryManager.Delete(id);
            return Ok(deleteCountry);
        }
    }
}
