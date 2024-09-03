namespace TekusAPI.Models
{
    public class ServiceProviderRequest
    {
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public int ProviderId { get; set; }
        public List<int> CountryIds { get; set; } // IDs de los países asociados
        public List<CustomFieldRequest> CustomFields { get; set; }
    }

    public class CustomFieldRequest
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }

}
