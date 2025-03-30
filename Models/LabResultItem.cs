// Placeholder: Replace with actual Lab Result item structure from API
namespace ClaudeAcDirectSql.Models
{
    public class LabResultItem
    {
        public int? ResultId { get; set; } // Example
        public int? OrderId { get; set; } // Link to the Lab Order (DiagnosticReport?)
        public int? PatientId { get; set; }
        public string? LabCode { get; set; } // e.g., LOINC for the test
        public string? LabName { get; set; }
        public string? Value { get; set; } // Numeric or Coded Value
        public string? ValueType { get; set; } // e.g., "Numeric", "Coded", "String"
        public string? Unit { get; set; } // e.g., "mg/dL"
        public string? ReferenceRange { get; set; }
        public string? InterpretationCode { get; set; } // e.g., "H", "L", "N"
        public string? Status { get; set; } // e.g., "final", "preliminary", "corrected"
        public string? EffectiveDate { get; set; } // Consider DateTimeOffset
        public string? PerformerId { get; set; } // Performing Lab ID/Name?
        public string? EncounterId { get; set; } // Link to Encounter?
        // Add other relevant fields from the actual source
    }
}
