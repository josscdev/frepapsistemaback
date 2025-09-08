using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.FREPAPMODULE.MODULOREGISTROS;
using RombiBack.Services.ROM.LOGIN.Company;
using RombiBack.Services.ROM.LOGIN.MGM_Country;
using System.Text.Json;

namespace RombiBack.Controllers.ROM.LOGIN.MGM_Country
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryServices _countryService;
        public CountryController(ICountryServices countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var countries= await _countryService.GetAll();
            string jsonString = JsonSerializer.Serialize(countries);

            //return Ok(countries);
            return Content(jsonString, "application/json");
        }


       
    }
}
