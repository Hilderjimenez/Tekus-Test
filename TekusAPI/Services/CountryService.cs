using TekusAPI.Models;
using Newtonsoft.Json;
using TekusAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace TekusAPI.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _httpClient;
        private readonly ProvidersTekusDbContext _context;

        public CountryService(HttpClient httpClient, ProvidersTekusDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://restcountries.com/v3.1/all");
                var countriesDto = JsonConvert.DeserializeObject<List<dynamic>>(response);

                

                // Ajusta el mapeo para la nueva estructura
                var countries = countriesDto.Select(dto => new Country
                {
                    CommonName = dto.name.common != null ? (string)dto.name.common : "Desconocido",
                    OfficialName = dto.name.official != null ? (string)dto.name.official : "Desconocido",
                    

                }).ToList();

                var existingCountries = await _context.Countries.ToListAsync();
                var existingCountryNames = existingCountries.Select(c => c.CommonName).ToHashSet();

                var newCountries = countries
                    .Where(c => !existingCountryNames.Contains(c.CommonName))
                    .ToList();

                if (newCountries.Any())
                {
                    _context.Countries.AddRange(newCountries);
                    await _context.SaveChangesAsync();
                }

                return await _context.Countries.ToListAsync();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Error al hacer la solicitud HTTP a la API externa", e);
            }
            catch (JsonException e)
            {
                throw new Exception("Error al deserializar la respuesta JSON", e);
            }
            catch (Exception e)
            {
                throw new Exception("Ocurrió un error inesperado", e);
            }
        }
    }

   
}
