using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TekusAPI.Models
{
    public class ServicesProvider
    {
        [Key]
        public int IdServices { get; set; }
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
       
        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public ProvidersTekus Provider { get; set; }

        // Relación muchos a muchos con Country
        public ICollection<Country> Countries { get; set; }

        // Campos personalizados asociados con el proveedor
        public List<CustomField> CustomFields { get; set; } = new List<CustomField>();
    }
}
