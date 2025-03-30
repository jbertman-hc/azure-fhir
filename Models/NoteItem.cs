// Placeholder: Replace with actual Note item structure from API
// This might come directly from EncounterItem.NoteText or a separate /api/Notes endpoint
namespace ClaudeAcDirectSql.Models
{
    public class NoteItem // Or ClinicalNoteItem?
    {
        public int? NoteId { get; set; } // Example
        public int? PatientId { get; set; }
        public string? EncounterId { get; set; } // Link to Encounter
        public string? NoteTitle { get; set; } // e.g., "Progress Note", "Consult Note"
        public string? NoteType { get; set; } // e.g., LOINC code for type
        public string? AuthorId { get; set; } // Practitioner ID/Name?
        public string? AuthoredDate { get; set; } // Consider DateTimeOffset
        public string? Status { get; set; } // e.g., "final", "amended"
        public string? Content { get; set; } // The actual text content
        public string? ContentType { get; set; } // e.g., "text/plain", "text/html"
        // Add other relevant fields from the actual source
    }
}
