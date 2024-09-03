using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekusAPI.Data;
using TekusAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class IndicatorsController : ControllerBase
{
    private readonly ProvidersTekusDbContext _context;

    public IndicatorsController(ProvidersTekusDbContext context)
    {
        _context = context;
    }

    
    [HttpGet("indicator")]
    public async Task<ActionResult<object>> GetSummaryIndicators()
    {
        var servicesPerCountry = await _context.ServicesProvider
            .SelectMany(sp => sp.Countries)
            .GroupBy(c => c.CommonName)
            .Select(g => new { Country = g.Key, ServicesCount = g.Count() })
            .ToListAsync();

        var summary = new
        {
            ServicesPerCountry = servicesPerCountry,
            TotalProviders = await _context.ProvidersTekus.CountAsync(),
            TotalServices = await _context.ServicesProvider.CountAsync()
        };

        return Ok(summary);
    }
}

