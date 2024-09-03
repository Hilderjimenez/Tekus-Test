using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TekusAPI.Models
{
    public class CustomField
    {
        [Key]
        public int IdCustomField { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public int ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public ProvidersTekus Provider { get; set; }

    }
}
