using System.Collections.Generic;

namespace TekusAPI.Models
{
    public class ServicesProviderWithCustomFields
    {
        public ServicesProvider Service { get; set; }
        public List<CustomField> CustomFields { get; set; } = new List<CustomField>();
    }
}
