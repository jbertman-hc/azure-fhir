// Placeholder: Replace with actual Medication List item structure from API (if different from Request)
namespace ClaudeAcDirectSql.Models
{
    public class MedicationStatementItem
    {
        public int? StatementId { get; set; } // Example
        public int? PatientId { get; set; }
        public string? MedicationCode { get; set; } // e.g., RxNorm
        public string? MedicationName { get; set; }
        public string? DosageText { get; set; } // As reported
        public string? Status { get; set; } // e.g., "Active", "Intended"
        public string? ReasonCode { get; set; }
        public string? DateAsserted { get; set; } // Consider DateTimeOffset
        public string? InformationSource { get; set; } // Patient, Provider?
         // Add other relevant fields from the actual source
    }
}
