using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TekusAPI.Models;
using TekusAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Country>>> GetCountries()
    {
        var countries = await _countryService.GetCountriesAsync();
        return Ok(countries);
    }

    [HttpPost("sync")]
    public async Task<IActionResult> SyncCountries()
    {
        await _countryService.GetCountriesAsync();
        return NoContent();
    }
}
