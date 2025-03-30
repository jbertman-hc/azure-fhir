// Placeholder POCO for Practice/Organization Information
// <<< FLAG: Verify properties against actual schema/data from PracticeInfoRepository >>>
namespace ClaudeAcDirectSql.Models
{
    public class PracticeInfo
    {
        public int? PracticeId { get; set; } // <<< FLAG: Confirm primary key / identifier field name
        public string? Name { get; set; }
        public string? Npi { get; set; } // <<< FLAG: Confirm NPI field name and format
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; } // <<< FLAG: Needs mapping to standard abbreviation if needed
        public string? ZipCode { get; set; }
        public string? Country { get; set; } // <<< FLAG: Default or confirm source
        public string? PhoneNumber { get; set; }
        public string? FaxNumber { get; set; }
        public string? Email { get; set; }
        // Add other relevant fields based on PracticeInfoRepository
    }
}
