// Placeholder: Replace with actual Problem List item structure from API
namespace ClaudeAcDirectSql.Models
{
    public class ProblemListItem // Or DiagnosisListItem?
    {
        public int? ProblemId { get; set; } // Example
        public int? PatientId { get; set; }
        public string? ProblemCode { get; set; } // e.g., ICD-10
        public string? ProblemDescription { get; set; }
        public string? Status { get; set; } // e.g., "Active", "Resolved"
        public string? OnsetDate { get; set; } // Consider DateTimeOffset
        public string? ResolvedDate { get; set; } // Consider DateTimeOffset
        public string? RecordedDate { get; set; } // Consider DateTimeOffset
        public string? RecordedBy { get; set; } // Provider ID/Name?
        public string? EncounterId { get; set; } // Link to Encounter?
        // Add other relevant fields from the actual source
    }
}
