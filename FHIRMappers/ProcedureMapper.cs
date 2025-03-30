using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class ProcedureMapper
    {
        public static Procedure ToFhirProcedure(ProcedureItem source)
        {
            if (source == null)
            {
                return null;
            }

            var procedure = new Procedure();

            // --- Set Resource ID ---
            // TODO: Define ID strategy (e.g., prefix + ProcedureId)
            if (source.ProcedureId.HasValue)
            {
                procedure.Id = $"Procedure-{source.ProcedureId.Value}";
            }

            // --- Status (Mandatory) ---
            // TODO: Map source.Status to FHIR EventStatus codes (preparation, in-progress, not-done, on-hold, stopped, completed, entered-in-error, unknown)
            // procedure.Status = MapStatus(source.Status); // <<< FLAG

            // --- Status Reason ---
            // TODO: Map reason if status is 'not-done'.

            // --- Category ---
            // TODO: Map procedure category if available (e.g., Surgical Procedure, Diagnostic Procedure).

            // --- Code (Mandatory) ---
            // TODO: Map source.ProcedureCode/Name to CodeableConcept.
            // TODO: Determine coding system (e.g., CPT, SNOMED CT, ICD-10-PCS).
            if (!string.IsNullOrWhiteSpace(source.ProcedureCode) || !string.IsNullOrWhiteSpace(source.ProcedureName))
            {
                procedure.Code = new CodeableConcept();
                if (!string.IsNullOrWhiteSpace(source.ProcedureCode))
                {
                    // Example using SNOMED CT - system needs confirmation
                    procedure.Code.Coding.Add(new Coding("http://snomed.info/sct", source.ProcedureCode)); // <<< FLAG (Confirm system)
                }
                procedure.Code.Text = source.ProcedureName;
            }
            else
            {
                // TODO: Handle missing required procedure code (validation will fail)
            }

            // --- Subject (Patient Reference - Mandatory) ---
            // TODO: Create a Patient reference based on source.PatientId
            if (source.PatientId.HasValue)
            {
                procedure.Subject = new ResourceReference($"Patient/{source.PatientId.Value}"); // <<< FLAG: Ensure Patient ID strategy matches
            }
            else
            {
                // TODO: Handle missing patient link (validation will fail)
            }

            // --- Encounter Reference ---
            // TODO: Create an Encounter reference based on source.EncounterId (if available)
            if (!string.IsNullOrWhiteSpace(source.EncounterId))
            {
                procedure.Encounter = new ResourceReference($"Encounter/{source.EncounterId}"); // <<< FLAG: Define Encounter ID strategy
            }

            // --- Performed Date/Time/Period (Mandatory) ---
            // TODO: Map source.PerformedDate to procedure.Performed[x] (dateTime, Period, string, Age, Range)
            if (!string.IsNullOrWhiteSpace(source.PerformedDate))
            {
                // Example mapping to DateTime - needs robust parsing
                if (DateTime.TryParse(source.PerformedDate, out DateTime performedDt))
                {
                    procedure.Performed = new FhirDateTime(new DateTimeOffset(performedDt));
                }
                // TODO: Consider mapping to Period if start/end times are available
                else
                {
                    procedure.Performed = new FhirString(source.PerformedDate);
                }
            }
            else
            {
                // TODO: Handle missing required performed date (validation will fail)
            }

            // --- Recorder ---
            // TODO: Map recorder if available (Practitioner/Patient/RelatedPerson).

            // --- Asserter ---
            // TODO: Map asserter if available.

            // --- Performer ---
            // <<< FLAG: Assumes source.PerformerId contains the ReferProviderID for the Practitioner >>>
            if (!string.IsNullOrWhiteSpace(source.PerformerId))
            {
                if (int.TryParse(source.PerformerId, out int practitionerId))
                {
                    // <<< FLAG: This creates a reference only. If the Practitioner resource needs to be *contained*, it must be added to procedure.Contained >>>
                    // <<< FLAG: Consider error handling if GetFhirPractitioner returns null >>>
                    var practitionerReference = new ResourceReference($"Practitioner/{PractitionerMapper.GetFhirPractitioner(practitionerId)?.Id}");

                    if (practitionerReference.Reference != null) // Check if ID was generated
                    {
                        var performer = new Procedure.PerformerComponent();
                        // TODO: Map performer function (e.g., primary surgeon, assistant) if known
                        performer.Function = new CodeableConcept { Text = "Primary Performer" }; // <<< FLAG: Example/Default function, needs mapping if source data exists
                        performer.Actor = practitionerReference;
                        procedure.Performer.Add(performer);
                    }
                    else
                    {
                        // <<< FLAG: Log or handle failure to create practitioner reference (e.g., practitioner ID not found by mapper) >>>
                    }
                }
                else
                {
                    // <<< FLAG: Log or handle error if PerformerId is not a valid integer >>>
                }
            }

            // --- Location Reference ---
            // TODO: Map location where procedure was performed, if available.

            // --- Reason Code / Reference ---
            // TODO: Map source.ReasonCode (indication) or link to Condition/Observation.
            // if (!string.IsNullOrWhiteSpace(source.ReasonCode))
            // {
            //     procedure.ReasonCode.Add(new CodeableConcept(/* system */, source.ReasonCode)); // <<< FLAG
            // }

            // --- Body Site ---
            // TODO: Map source.BodySite to CodeableConcept (using body site codes).
            // if (!string.IsNullOrWhiteSpace(source.BodySite))
            // {
            //     procedure.BodySite.Add(MapBodySite(source.BodySite)); // <<< FLAG
            // }

            // --- Outcome ---
            // TODO: Map source.Outcome to CodeableConcept.
            // if (!string.IsNullOrWhiteSpace(source.Outcome))
            // {
            //     procedure.Outcome = new CodeableConcept { Text = source.Outcome }; // <<< FLAG (Map to codes if possible)
            // }

            // --- Report Reference ---
            // TODO: Link to DiagnosticReport if this procedure generated one.

            // --- Complication / Detail ---
            // TODO: Map any documented complications.

            // --- Follow Up ---
            // TODO: Map any follow-up instructions.

            // --- Note ---
            // TODO: Map any free-text notes.

            // --- Focal Device ---
            // TODO: Map any devices used/implanted during the procedure (link to Device).

            // --- Used Reference / Code ---
            // TODO: Map items used during procedure (link to Device/Medication/Substance).


            // --- Set Profile ---
            procedure.Meta = new Meta();
            procedure.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-procedure" };

            return procedure;
        }

        // --- TODO: Implement Helper Methods ---
        // private static Procedure.ProcedureStatus MapStatus(string sourceStatus) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolvePractitionerOrOrganizationReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
        // private static CodeableConcept MapBodySite(string sourceBodySite) { /* ... */ throw new NotImplementedException(); }
    }
}
