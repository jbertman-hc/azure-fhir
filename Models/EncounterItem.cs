// Placeholder: Replace with actual Encounter item structure from API
namespace ClaudeAcDirectSql.Models
{
    public class EncounterItem
    {
        public int? EncounterId { get; set; } // Example
        public int? PatientId { get; set; }
        public string? Status { get; set; } // e.g., "finished", "in-progress", "cancelled"
        public string? EncounterType { get; set; } // e.g., "Office Visit", "Inpatient"
        public string? StartDate { get; set; } // Consider DateTimeOffset
        public string? EndDate { get; set; } // Consider DateTimeOffset
        public string? ServiceProviderId { get; set; } // Link to Organization/Location?
        public string? ParticipantId { get; set; } // Link to Practitioner?
        public string? LocationId { get; set; } // Link to Location?
        public string? ReasonCode { get; set; } // Reason for visit code?
        public string? DiagnosisCode { get; set; } // Diagnosis associated with encounter?
        public string? NoteText { get; set; } // <<< FLAG: Important field for concatenated notes
        // Add other relevant fields from the actual source
    }
}
