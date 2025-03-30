// Placeholder: Replace with actual Procedure item structure from API
namespace ClaudeAcDirectSql.Models
{
    public class ProcedureItem
    {
        public int? ProcedureId { get; set; } // Example
        public int? PatientId { get; set; }
        public string? ProcedureCode { get; set; } // e.g., CPT, SNOMED CT
        public string? ProcedureName { get; set; }
        public string? Status { get; set; } // e.g., "completed", "in-progress"
        public string? PerformedDate { get; set; } // Consider DateTimeOffset or Period
        public string? PerformerId { get; set; } // Provider ID/Name?
        public string? EncounterId { get; set; } // Link to Encounter?
        public string? ReasonCode { get; set; } // Indication code?
        public string? BodySite { get; set; } // Body site code?
        public string? Outcome { get; set; }
         // Add other relevant fields from the actual source
    }
}
