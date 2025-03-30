// Placeholder: Replace with actual Immunization item structure from API
namespace ClaudeAcDirectSql.Models
{
    public class ImmunizationItem
    {
        public int? ImmunizationId { get; set; } // Example
        public int? PatientId { get; set; }
        public string? VaccineCode { get; set; } // e.g., CVX
        public string? VaccineName { get; set; }
        public string? Status { get; set; } // e.g., "completed", "entered-in-error"
        public string? OccurrenceDate { get; set; } // Consider DateTimeOffset
        public string? LotNumber { get; set; }
        public string? Manufacturer { get; set; }
        public string? Site { get; set; } // Body site code?
        public string? Route { get; set; } // Route code?
        public string? PerformerId { get; set; } // Provider ID/Name?
        public string? EncounterId { get; set; } // Link to Encounter?
        public bool? PrimarySource { get; set; } // Reported by patient vs administered?
        // Add other relevant fields from the actual source
    }
}
