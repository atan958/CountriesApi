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
        
        [HttpGet("[action]")]
        public ActionResult<Country> Get(string id)
        {
            var country = dataService.Get(id);
            if (country is null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpPost("[action]")]
        public IActionResult Post([FromBody] Country country)
        {
            // Can perform checks to see if format of inputted object is correct

            dataService.Post(country);
            return Ok(country);
        }

        [HttpPut("[action]")]
        public IActionResult Put([FromQuery] string id, [FromBody] Country country)
        {
            var updateCountry = dataService.Get(id);
            if (updateCountry is null)
            {
                return NotFound();
            }
            dataService.Put(id, country);
            return Ok(country);
        }

        [HttpDelete("[action]")]
        public IActionResult Delete([FromQuery] string id)
        {
            Country deleteCountry = dataService.Get(id);
            if (deleteCountry is null)
            {
                return BadRequest();
            }

            dataService.Delete(id);
            return Ok(deleteCountry);
        }
    }
}
