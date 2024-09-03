namespace TekusAPI.Models
{
    public class UpdateServiceProviderDto
    {
        public string Name { get; set; }
        public int? ProviderId { get; set; }
        public List<int>? CountryIds { get; set; } 
        public List<UpdateCustomFieldDto>? CustomFields { get; set; }
    }

    public class UpdateCustomFieldDto
    {
        public int IdCustomField { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public int? ProviderId { get; set; }
    }
}
