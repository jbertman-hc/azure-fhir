// Placeholder: Replace with actual Lab Order/Report structure from API
namespace ClaudeAcDirectSql.Models
{
    public class LabReportItem // Or LabOrderItem?
    {
        public int? ReportId { get; set; } // Example
        public int? PatientId { get; set; }
        public string? EncounterId { get; set; } // Link to Encounter?
        public string? OrderCode { get; set; } // Code for the ordered panel/test (LOINC?)
        public string? OrderName { get; set; }
        public string? Status { get; set; } // e.g., "final", "registered", "partial"
        public string? EffectiveDate { get; set; } // Date of report/collection (Consider DateTimeOffset)
        public string? RequesterId { get; set; } // Ordering Provider ID/Name?
        public string? PerformerId { get; set; } // Performing Lab ID/Name?
        public string? ReportText { get; set; } // Narrative summary, if any
        // Note: Individual results might be linked via ReportId or be nested here
        // List<LabResultItem> Results { get; set; } // <<< FLAG: How are results associated?
        // Add other relevant fields from the actual source
    }
}
