using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class ConditionMapper
    {
        public static Condition ToFhirCondition(ProblemListItem source)
        {
            if (source == null)
            {
                return null;
            }

            var condition = new Condition();

            // --- Set Resource ID ---
            // TODO: Define ID strategy (e.g., prefix + ProblemId)
            if (source.ProblemId.HasValue)
            {
                condition.Id = $"Condition-{source.ProblemId.Value}";
            }

            // --- Clinical Status (Mandatory in US Core if known) ---
            // TODO: Identify source field for clinical status (active, recurrence, relapse, inactive, remission, resolved)
            // TODO: Map source status to FHIR Condition Clinical Status codes
            // condition.ClinicalStatus = MapClinicalStatus(source.Status); // <<< FLAG

            // --- Verification Status (Mandatory in US Core if known) ---
            // TODO: Identify source field for verification status (unconfirmed, provisional, differential, confirmed, refuted, entered-in-error)
            // TODO: Map source status to FHIR Condition Verification Status codes
            // condition.VerificationStatus = MapVerificationStatus(source.VerificationStatus); // <<< FLAG (Assuming a field exists)

            // --- Category (Mandatory in US Core) ---
            // TODO: Determine category (problem-list-item, encounter-diagnosis, health-concern)
            // Usually 'problem-list-item' for this kind of source, but confirm.
            condition.Category.Add(new CodeableConcept("http://terminology.hl7.org/CodeSystem/condition-category", "problem-list-item", "Problem List Item")); // <<< FLAG (Confirm category)

            // --- Code (Diagnosis Code - Mandatory in US Core) ---
            // TODO: Identify source field for diagnosis code (e.g., ICD-10)
            // TODO: Determine the coding system (e.g., http://hl7.org/fhir/sid/icd-10-cm)
            // TODO: Map source code
            if (!string.IsNullOrWhiteSpace(source.ProblemCode))
            {
                condition.Code = new CodeableConcept(
                    "http://hl7.org/fhir/sid/icd-10-cm", // <<< FLAG: Confirm system
                    source.ProblemCode,
                    source.ProblemDescription // Optional display text
                );
            }
            else
            {
                // TODO: Handle missing required code (validation will fail)
                // Might need a default 'unknown' code or data-absent-reason
            }

            // --- Subject (Patient Reference - Mandatory) ---
            // TODO: Create a Patient reference based on source.PatientId
            if (source.PatientId.HasValue)
            {
                condition.Subject = new ResourceReference($"Patient/{source.PatientId.Value}"); // <<< FLAG: Ensure Patient ID strategy matches
            }
            else
            {
                // TODO: Handle missing patient link (validation will fail)
            }

            // --- Encounter Reference ---
            // TODO: Create an Encounter reference based on source.EncounterId (if available)
            if (!string.IsNullOrWhiteSpace(source.EncounterId))
            {
                condition.Encounter = new ResourceReference($"Encounter/{source.EncounterId}"); // <<< FLAG: Define Encounter ID strategy
            }

            // --- Onset ---
            // TODO: Map source.OnsetDate to Condition.Onset[x] (dateTime, Age, Period, Range, string)
            if (!string.IsNullOrWhiteSpace(source.OnsetDate))
            {
                // Example: Mapping to dateTime - add robust parsing
                if (DateTime.TryParse(source.OnsetDate, out DateTime onsetDt))
                {
                    condition.Onset = new FhirDateTime(new DateTimeOffset(onsetDt));
                }
                else
                {
                     // Could map to FhirString if parsing fails or format varies
                     condition.Onset = new FhirString(source.OnsetDate);
                }
            }

            // --- Abatement (Resolved Date) ---
            // TODO: Map source.ResolvedDate to Condition.Abatement[x]
            if (!string.IsNullOrWhiteSpace(source.ResolvedDate))
            {
                 if (DateTime.TryParse(source.ResolvedDate, out DateTime resolvedDt))
                 {
                     condition.Abatement = new FhirDateTime(new DateTimeOffset(resolvedDt));
                     // If resolved date exists, clinical status might need to be updated to resolved/inactive
                     // TODO: Coordinate ClinicalStatus mapping with Abatement mapping
                 }
                 else
                 {
                      condition.Abatement = new FhirString(source.ResolvedDate);
                 }
            }

             // --- Recorded Date ---
             // TODO: Map source.RecordedDate
             if (!string.IsNullOrWhiteSpace(source.RecordedDate))
             {
                 if (DateTime.TryParse(source.RecordedDate, out DateTime recordedDt))
                 {
                     condition.RecordedDateElement = new FhirDateTime(new DateTimeOffset(recordedDt));
                 }
             }

            // --- Recorder (Practitioner/Patient/RelatedPerson Reference) ---
            // TODO: Map source.RecordedBy to a Practitioner or other reference
            // if (!string.IsNullOrWhiteSpace(source.RecordedBy))
            // {
            //     condition.Recorder = ResolvePractitionerReference(source.RecordedBy); // <<< FLAG
            // }

            // --- Set Profile ---
            condition.Meta = new Meta();
            condition.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-condition-problems-health-concerns" };

            return condition;
        }

        // --- TODO: Implement Helper Methods ---
        // private static CodeableConcept MapClinicalStatus(string sourceStatus) { /* ... */ throw new NotImplementedException(); }
        // private static CodeableConcept MapVerificationStatus(string sourceVerificationStatus) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolvePractitionerReference(string sourcePractitionerIdOrName) { /* ... */ throw new NotImplementedException(); }
    }
}
