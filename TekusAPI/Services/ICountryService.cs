using TekusAPI.Models;

namespace TekusAPI.Services
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountriesAsync();
    }
}
