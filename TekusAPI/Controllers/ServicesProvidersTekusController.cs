using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TekusAPI.Data;
using TekusAPI.Models;

namespace TekusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesProvidersTekusController : ControllerBase
    {
        private readonly ProvidersTekusDbContext _context;

        public ServicesProvidersTekusController(ProvidersTekusDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicesProvider>>> GetServicesProvider()
        {
            return await _context.ServicesProvider.Include(s => s.Countries).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServicesProvider>> GetServiceProvider(int id)
        {
            var serviceProvider = await _context.ServicesProvider
                .Include(s => s.Countries)
                .Include(s => s.CustomFields)
                .FirstOrDefaultAsync(s => s.IdServices == id);

            if (serviceProvider == null)
            {
                return NotFound();
            }

            return serviceProvider;
        }


        [HttpPost]
        public async Task<ActionResult<ServicesProvider>> PostServiceProvider(ServiceProviderRequest request)
        {
            
            var provider = await _context.ProvidersTekus.FindAsync(request.ProviderId);
            if (provider == null)
            {
                return BadRequest("Proveedor no encontrado.");
            }

            
            var serviceProvider = new ServicesProvider
            {
                Name = request.Name,
                HourlyRate = request.HourlyRate,
                ProviderId = request.ProviderId
            };

            
            if (request.CountryIds != null && request.CountryIds.Any())
            {
                serviceProvider.Countries = await _context.Countries
                    .Where(c => request.CountryIds.Contains(c.IdCountry))
                    .ToListAsync();
            }

            
            if (request.CustomFields != null && request.CustomFields.Any())
            {
                foreach (var fieldRequest in request.CustomFields)
                {
                    var customField = new CustomField
                    {
                        FieldName = fieldRequest.FieldName,
                        FieldValue = fieldRequest.FieldValue,
                        ProviderId = provider.IdProviders,
                        //Service = serviceProvider // Asociar al servicio recién creado
                    };
                    _context.CustomFields.Add(customField);
                }
            }

            
            _context.ServicesProvider.Add(serviceProvider);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceProvider), new { id = serviceProvider.IdServices }, serviceProvider);
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchServiceProvider(int id, [FromBody] UpdateServiceProviderDto updateDto)
        {
            var existingServiceProvider = await _context.ServicesProvider
                .Include(sp => sp.CustomFields)
                .Include(sp => sp.Countries)
                .FirstOrDefaultAsync(sp => sp.IdServices == id);

            if (existingServiceProvider == null)
            {
                return NotFound();
            }

            bool providerChanged = false;

            if (!string.IsNullOrEmpty(updateDto.Name))
            {
                existingServiceProvider.Name = updateDto.Name;
            }

            if (updateDto.ProviderId.HasValue)
            {
                
                var provider = await _context.ProvidersTekus.FindAsync(updateDto.ProviderId.Value);
                if (provider == null)
                {
                    return BadRequest("Provider ID is invalid.");
                }

               
                if (existingServiceProvider.ProviderId != updateDto.ProviderId.Value)
                {
                    existingServiceProvider.ProviderId = updateDto.ProviderId.Value;
                    providerChanged = true;
                }
            }

            if (updateDto.CountryIds != null)
            {
                
                var countries = await _context.Countries
                    .Where(c => updateDto.CountryIds.Contains(c.IdCountry))
                    .ToListAsync();
                if (countries.Count != updateDto.CountryIds.Count)
                {
                    return BadRequest("One or more Country IDs are invalid.");
                }
                existingServiceProvider.Countries = countries;
            }

            if (updateDto.CustomFields != null)
            {
                foreach (var customFieldDto in updateDto.CustomFields)
                {
                    var existingCustomField = existingServiceProvider.CustomFields
                        .FirstOrDefault(cf => cf.IdCustomField == customFieldDto.IdCustomField);

                    if (existingCustomField != null)
                    {
                        existingCustomField.FieldName = customFieldDto.FieldName;
                        existingCustomField.FieldValue = customFieldDto.FieldValue;

                        
                        if (providerChanged)
                        {
                            existingCustomField.ProviderId = updateDto.ProviderId.Value;
                        }
                        else
                        {
                            
                            if (customFieldDto.ProviderId > 0)
                            {
                                var provider = await _context.ProvidersTekus.FindAsync(customFieldDto.ProviderId);
                                if (provider != null)
                                {
                                    existingCustomField.ProviderId = (int)customFieldDto.ProviderId;
                                }
                                else
                                {
                                    return BadRequest($"Custom Field Provider ID {customFieldDto.ProviderId} is invalid.");
                                }
                            }
                            else
                            {
                                
                                existingCustomField.ProviderId = existingCustomField.ProviderId;
                            }
                        }
                    }
                    else
                    {
                        
                        existingServiceProvider.CustomFields.Add(new CustomField
                        {
                            IdCustomField = customFieldDto.IdCustomField,
                            FieldName = customFieldDto.FieldName,
                            FieldValue = customFieldDto.FieldValue,
                            ProviderId = (int)(updateDto.ProviderId.HasValue ? updateDto.ProviderId.Value : customFieldDto.ProviderId)
                        });
                    }
                }
            }


            _context.Entry(existingServiceProvider).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
