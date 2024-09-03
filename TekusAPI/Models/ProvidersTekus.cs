using System.ComponentModel.DataAnnotations;

namespace TekusAPI.Models
{
    public class ProvidersTekus
    {
        [Key]
        public int IdProviders { get; set; }
        public string NIT { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        
    }
}
