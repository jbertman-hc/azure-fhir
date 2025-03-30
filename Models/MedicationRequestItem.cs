// Placeholder: Replace with actual Medication Order/Request structure from API
namespace ClaudeAcDirectSql.Models
{
    public class MedicationRequestItem // Or PrescriptionItem?
    {
        public int? RequestId { get; set; } // Example
        public int? PatientId { get; set; }
        public string? MedicationCode { get; set; } // e.g., RxNorm
        public string? MedicationName { get; set; }
        public string? DosageInstruction { get; set; } // Sig
        public string? Quantity { get; set; }
        public string? Refills { get; set; }
        public string? Status { get; set; } // e.g., "Active", "Completed"
        public string? PrescriberId { get; set; } // Provider ID/Name?
        public string? DateWritten { get; set; } // Consider DateTimeOffset
        public string? StartDate { get; set; } // Consider DateTimeOffset
        public string? EndDate { get; set; } // Consider DateTimeOffset
        public string? EncounterId { get; set; } // Link to Encounter?
        // Add other relevant fields from the actual source
    }
}
