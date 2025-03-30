// Placeholder POCO for Provider/Practitioner Information
// <<< FLAG: Verify properties against actual schema/data from ReferProvidersRepository >>>
namespace ClaudeAcDirectSql.Models
{
    public class ProviderInfo
    {
        public int? ProviderId { get; set; } // <<< FLAG: Confirm primary key / identifier field name
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Suffix { get; set; }
        public string? Npi { get; set; } // <<< FLAG: Confirm NPI field name and format
        public string? Specialty { get; set; } // <<< FLAG: Need mapping strategy to FHIR coding if possible
        public string? LicenseNumber { get; set; }
        public string? DeaNumber { get; set; } // <<< FLAG: Sensitive, ensure proper handling
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        // Add other relevant fields based on ReferProvidersRepository
    }
}
