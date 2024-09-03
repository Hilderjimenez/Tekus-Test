using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TekusAPI.Models
{
    public class Country
    {
        [Key]
        public int IdCountry { get; set; }

        public string CommonName { get; set; }
        public string? OfficialName { get; set; }

        public int ServiceId { get; set; }

        // Relación muchos a muchos con ServicesProvider
        public ICollection<ServicesProvider> Services { get; set; }
    }
}
