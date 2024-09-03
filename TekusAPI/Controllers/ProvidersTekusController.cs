using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekusAPI.Data;
using TekusAPI.Models;

namespace TekusAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvidersTekusController : ControllerBase
    {
        private readonly ProvidersTekusDbContext _context;

        public ProvidersTekusController(ProvidersTekusDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvidersTekus>>> GetProvidersTekus()
        {
            return await _context.ProvidersTekus.ToListAsync();
        }

        
        [HttpGet("{NIT}")]
        public async Task<ActionResult<ProvidersTekus>> GetProviderTekus(string NIT)
        {
            var provider = await _context.ProvidersTekus
                .Where(p => p.IsActive)
                .FirstOrDefaultAsync(p => p.NIT == NIT && p.IsActive);

            if (provider == null)
            {
                return NotFound();
            }

            return provider;
        }


        [HttpPost]
        public async Task<ActionResult<ProvidersTekus>> PostProvider(ProvidersTekus provider)
        {
            if (provider == null)
            {
                return BadRequest("Provider cannot be null");
            }

            _context.ProvidersTekus.Add(provider);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProviderTekus), new { NIT = provider.NIT }, provider);

        }

        [HttpPut("{NIT}")]
        public async Task<IActionResult> PutProvider(string NIT, [FromBody] ProvidersTekus updatedProvider)
        {
            
            var existingProvider = await _context.ProvidersTekus
                .FirstOrDefaultAsync(p => p.NIT == NIT);

            if (existingProvider == null)
            {
                return NotFound();
            }

            
            existingProvider.Name = updatedProvider.Name;
            existingProvider.Email = updatedProvider.Email;
            existingProvider.IsActive = updatedProvider.IsActive;

            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                
                return StatusCode(500, "Internal server error");
            }

            return NoContent(); 
        }




        [HttpPatch("{NIT}/inactivate")]
        public async Task<IActionResult> InactivateProvider(string NIT)
        {
            
            var provider = await _context.ProvidersTekus
                .FirstOrDefaultAsync(p => p.NIT == NIT);

            
            if (provider == null)
            {
                return NotFound();
            }

            
            provider.IsActive = false;

            
            _context.Entry(provider).Property(p => p.IsActive).IsModified = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{NIT}/activate")]
        public async Task<IActionResult> ActivateProvider(string NIT)
        {
            
            var provider = await _context.ProvidersTekus
                .FirstOrDefaultAsync(p => p.NIT == NIT);

            
            if (provider == null)
            {
                return NotFound();
            }

            
            provider.IsActive = true;

            
            _context.Entry(provider).Property(p => p.IsActive).IsModified = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}
