using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;
using System.Text;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class DocumentReferenceMapper
    {
        // Option 1: Map directly from a NoteItem (if distinct API exists)
        public static DocumentReference ToFhirDocumentReference(NoteItem source)
        {
            if (source == null || string.IsNullOrWhiteSpace(source.Content))
            {
                return null;
            }

            var docRef = new DocumentReference();

            // --- Set Resource ID ---
            // TODO: Define ID strategy (e.g., prefix + NoteId)
            if (source.NoteId.HasValue)
            {
                docRef.Id = $"DocumentReference-Note-{source.NoteId.Value}";
            }

            // --- Status (Mandatory) ---
            // TODO: Map source.Status to DocumentReferenceStatus (current, superseded, entered-in-error)
            docRef.Status = DocumentReferenceStatus.Current; // <<< FLAG (Defaulting to current, needs mapping)
            // docRef.Status = MapStatus(source.Status);

            // --- Type (Note Type - Important) ---
            // TODO: Map source.NoteType/Title to a LOINC code for the document type.
            if (!string.IsNullOrWhiteSpace(source.NoteType))
            {
                 // Example using LOINC - needs confirmation of system/codes used
                 docRef.Type = new CodeableConcept("http://loinc.org", source.NoteType, source.NoteTitle); // <<< FLAG
            }
            else if (!string.IsNullOrWhiteSpace(source.NoteTitle))
            {
                 docRef.Type = new CodeableConcept { Text = source.NoteTitle }; // Text fallback
            }
             else
            {
                 // TODO: Handle missing type - US Core requires type
                 // Use a default like "11488-4" (Consult note) or "18842-5" (Discharge summary) ? <<< FLAG
                 docRef.Type = new CodeableConcept("http://loinc.org", "34109-9", "Note"); // Generic fallback
            }

            // --- Category ---
            // TODO: Map category if available (e.g., Clinical Note).
            // docRef.Category.Add(new CodeableConcept(/* system */, /* code */));

            // --- Subject (Patient Reference - Mandatory) ---
            // TODO: Create a Patient reference based on source.PatientId
            if (source.PatientId.HasValue)
            {
                docRef.Subject = new ResourceReference($"Patient/{source.PatientId.Value}"); // <<< FLAG: Ensure Patient ID strategy matches
            }
            else
            {
                // TODO: Handle missing patient link (validation will fail)
            }

            // --- Date (When document was created/authored - Mandatory in US Core if known) ---
            // TODO: Map source.AuthoredDate
            if (!string.IsNullOrWhiteSpace(source.AuthoredDate))
            {
                if (DateTimeOffset.TryParse(source.AuthoredDate, out DateTimeOffset authoredDto))
                {
                    docRef.Date = authoredDto;
                }
            }
            // else { /* TODO: Handle potentially missing date */ }

            // --- Author ---
            // TODO: Map source.AuthorId to Practitioner/Organization/Device/Patient reference.
            // if (!string.IsNullOrWhiteSpace(source.AuthorId))
            // {
            //     docRef.Author.Add(ResolvePractitionerReference(source.AuthorId)); // <<< FLAG
            // }

            // --- Authenticator ---
            // TODO: Map authenticator/verifier if available.

            // --- Custodian (Organization Reference) ---
            // TODO: Identify and map the custodian organization.
            // docRef.Custodian = ResolveOrganizationReference(/* Org ID/Name */); // <<< FLAG

            // --- Relates To ---
            // TODO: Link related documents (e.g., addenda).

            // --- Description ---
            // TODO: Use source.NoteTitle or other summary field.
             if (!string.IsNullOrWhiteSpace(source.NoteTitle))
             {
                 docRef.Description = source.NoteTitle;
             }

            // --- Security Labels ---
            // TODO: Add confidentiality codes if applicable.

            // --- Content (Mandatory) ---
            // TODO: Embed the note content directly in an Attachment.
            var attachment = new Attachment();
            // TODO: Map source.ContentType (e.g., text/plain, text/html, application/pdf)
            attachment.ContentType = source.ContentType ?? "text/plain"; // <<< FLAG (Defaulting to text/plain)
            // TODO: Consider language if known.
            // attachment.Language = "en-US";
            // TODO: Encode content as Base64.
            if (!string.IsNullOrWhiteSpace(source.Content))
            {
                 attachment.Data = Encoding.UTF8.GetBytes(source.Content); // Base64 encoded bytes
            }
            // TODO: Provide Title in attachment?
             if (!string.IsNullOrWhiteSpace(source.NoteTitle))
             {
                 attachment.Title = source.NoteTitle;
             }
            // TODO: Provide Creation date in attachment?
            if (docRef.Date.HasValue)
            {
                 attachment.CreationElement = new FhirDateTime(docRef.Date.Value);
            }
            docRef.Content.Add(new DocumentReference.ContentComponent { Attachment = attachment });

            // --- Context (Encounter, EpisodeOfCare, etc.) ---
            // TODO: Link to the relevant Encounter based on source.EncounterId.
            if (!string.IsNullOrWhiteSpace(source.EncounterId))
            {
                if (docRef.Context == null) docRef.Context = new DocumentReference.ContextComponent();
                docRef.Context.Encounter.Add(new ResourceReference($"Encounter/{source.EncounterId}")); // <<< FLAG: Define Encounter ID strategy
            }
            // TODO: Add Period context if applicable.
            // TODO: Add Related references (e.g., link back to the Problem this note is about).


            // --- Set Profile ---
            docRef.Meta = new Meta();
            docRef.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-documentreference" };

            return docRef;
        }

        // Option 2: Map from EncounterItem's NoteText (If notes are embedded in encounters)
         public static DocumentReference ToFhirDocumentReferenceFromEncounter(EncounterItem sourceEncounter)
         {
             if (sourceEncounter == null || string.IsNullOrWhiteSpace(sourceEncounter.NoteText))
             {
                 return null;
             }

             var docRef = new DocumentReference();

             // --- Set Resource ID ---
             // TODO: Define ID strategy (e.g., prefix + EncounterId + "-Note")
             if (sourceEncounter.EncounterId.HasValue)
             {
                 docRef.Id = $"DocumentReference-Encounter-{sourceEncounter.EncounterId.Value}-Note";
             }

             // --- Status (Mandatory) ---
             // Assume 'current' unless encounter status indicates otherwise?
             docRef.Status = DocumentReferenceStatus.Current; // <<< FLAG

             // --- Type (Note Type - Important) ---
             // TODO: Infer type from EncounterType or use a generic default.
             // Example: Inferring from Encounter class might be too broad.
             // <<< FLAG: Need a reliable way to determine note type from Encounter context
             docRef.Type = new CodeableConcept("http://loinc.org", "34109-9", "Note"); // Generic fallback
             if (!string.IsNullOrWhiteSpace(sourceEncounter.EncounterType))
             {
                  docRef.Type.Text = $"{sourceEncounter.EncounterType} Note"; // Add encounter type to text
             }

             // --- Subject (Patient Reference - Mandatory) ---
             if (sourceEncounter.PatientId.HasValue)
             {
                 docRef.Subject = new ResourceReference($"Patient/{sourceEncounter.PatientId.Value}"); // <<< FLAG
             }
             else { /* Validation Error */ return null; }

             // --- Date (When document was created/authored - Mandatory in US Core if known) ---
             // Use Encounter EndDate or StartDate?
             // TODO: Determine best date mapping from Encounter
             if (!string.IsNullOrWhiteSpace(sourceEncounter.EndDate))
             {
                 if (DateTimeOffset.TryParse(sourceEncounter.EndDate, out DateTimeOffset endDto))
                 {
                     docRef.Date = endDto;
                 }
             }
             else if (!string.IsNullOrWhiteSpace(sourceEncounter.StartDate))
             {
                  if (DateTimeOffset.TryParse(sourceEncounter.StartDate, out DateTimeOffset startDto))
                 {
                     docRef.Date = startDto;
                 }
             }
              // else { /* TODO: Handle potentially missing date */ }

             // --- Author ---
             // TODO: Map Encounter.Participant (Practitioner) as Author?
             // if (!string.IsNullOrWhiteSpace(sourceEncounter.ParticipantId))
             // {
             //     docRef.Author.Add(ResolvePractitionerReference(sourceEncounter.ParticipantId)); // <<< FLAG
             // }

             // --- Custodian ---
             // TODO: Map Encounter.ServiceProvider (Organization)?
             // if (!string.IsNullOrWhiteSpace(sourceEncounter.ServiceProviderId))
             // {
             //    docRef.Custodian = ResolveOrganizationReference(sourceEncounter.ServiceProviderId); // <<< FLAG
             // }

             // --- Content (Mandatory) ---
             var attachment = new Attachment();
             attachment.ContentType = "text/plain"; // <<< FLAG (Assume plain text)
             attachment.Data = Encoding.UTF8.GetBytes(sourceEncounter.NoteText);
             if (docRef.Date.HasValue)
             {
                 attachment.CreationElement = new FhirDateTime(docRef.Date.Value);
             }
             if (!string.IsNullOrWhiteSpace(docRef.Type?.Text))
             {
                 attachment.Title = docRef.Type.Text;
             }
             docRef.Content.Add(new DocumentReference.ContentComponent { Attachment = attachment });

             // --- Context (Encounter) ---
             // Link back to the Encounter resource itself
             if (!string.IsNullOrWhiteSpace(docRef.Id))
             {
                 if (docRef.Context == null) docRef.Context = new DocumentReference.ContextComponent();
                 docRef.Context.Encounter.Add(new ResourceReference($"Encounter/{sourceEncounter.EncounterId.Value}")); // <<< FLAG
             }

             // --- Set Profile ---
             docRef.Meta = new Meta();
             docRef.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-documentreference" };

             return docRef;
         }

        // --- TODO: Implement Helper Methods ---
        // private static DocumentReferenceStatus MapStatus(string sourceStatus) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolvePractitionerReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolveOrganizationReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
    }
}
