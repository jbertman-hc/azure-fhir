// Placeholder: Replace with actual Allergy item structure from API
namespace ClaudeAcDirectSql.Models
{
    public class AllergyItem
    {
        public int? AllergyId { get; set; } // Example
        public int? PatientId { get; set; }
        public string? SubstanceCode { get; set; } // e.g., RxNorm, UNII?
        public string? SubstanceName { get; set; }
        public string? ReactionDescription { get; set; }
        public string? Severity { get; set; } // e.g., "Mild", "Severe"
        public string? Criticality { get; set; } // e.g., "High", "Low"
        public string? Status { get; set; } // e.g., "Active", "Inactive"
        public string? RecordedDate { get; set; } // Consider DateTimeOffset
         public string? RecordedBy { get; set; } // Provider ID/Name?
       // Add other relevant fields from the actual source
    }
}
