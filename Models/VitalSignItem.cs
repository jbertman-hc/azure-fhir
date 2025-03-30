// Placeholder: Replace with actual Vital Sign item structure from API
namespace ClaudeAcDirectSql.Models
{
    public class VitalSignItem
    {
        public int? VitalSignId { get; set; } // Example
        public int? PatientId { get; set; }
        public string? VitalCode { get; set; } // e.g., LOINC for type (BP, HR, Temp)
        public string? VitalName { get; set; }
        public string? Value { get; set; } // Single value (e.g., HR, Temp)
        public string? SystolicValue { get; set; } // For BP
        public string? DiastolicValue { get; set; } // For BP
        public string? Unit { get; set; } // e.g., "bpm", "mmHg", "Cel"
        public string? EffectiveDate { get; set; } // Consider DateTimeOffset
        public string? Status { get; set; } // e.g., "final", "amended"
        public string? PerformerId { get; set; } // Provider ID/Name?
        public string? EncounterId { get; set; } // Link to Encounter?
        public string? Interpretation { get; set; } // e.g., "High", "Normal"
        public string? BodySite { get; set; } // e.g., Left Arm
         // Add other relevant fields from the actual source
    }
}
