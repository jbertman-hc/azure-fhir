using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class AllergyIntoleranceMapper
    {
        public static AllergyIntolerance ToFhirAllergyIntolerance(AllergyItem source)
        {
            if (source == null)
            {
                return null;
            }

            var allergy = new AllergyIntolerance();

            // --- Set Resource ID ---
            // TODO: Define ID strategy (e.g., prefix + AllergyId)
            if (source.AllergyId.HasValue)
            {
                allergy.Id = $"AllergyIntolerance-{source.AllergyId.Value}";
            }

            // --- Clinical Status (Mandatory in US Core) ---
            // TODO: Identify source field for clinical status (active, inactive, resolved)
            // TODO: Map source status to FHIR AllergyIntolerance Clinical Status codes
            // allergy.ClinicalStatus = MapClinicalStatus(source.Status); // <<< FLAG

            // --- Verification Status (Mandatory in US Core) ---
            // TODO: Identify source field for verification status (unconfirmed, provisional, differential, confirmed, refuted, entered-in-error)
            // TODO: Map source status to FHIR AllergyIntolerance Verification Status codes
            // allergy.VerificationStatus = MapVerificationStatus(source.VerificationStatus); // <<< FLAG (Assuming a field exists)

            // --- Type (allergy vs intolerance - Mandatory) ---
            // TODO: Determine if source represents Allergy or Intolerance. Assume Allergy for now.
            allergy.Type = AllergyIntolerance.AllergyIntoleranceType.Allergy; // <<< FLAG (Confirm type)

            // --- Category (food, medication, environment, biologic - Mandatory) ---
            // TODO: Determine category based on substance or source data.
            // allergy.CategoryElement.Add(new Code<AllergyIntolerance.AllergyIntoleranceCategory>(AllergyIntolerance.AllergyIntoleranceCategory.Medication)); // <<< FLAG (Example, determine correct category)

            // --- Criticality (low, high, unable-to-assess - Optional but important) ---
            // TODO: Identify source field for criticality.
            // TODO: Map source criticality to FHIR AllergyIntolerance Criticality codes.
            // allergy.Criticality = MapCriticality(source.Criticality); // <<< FLAG

            // --- Code (Substance - Mandatory in US Core) ---
            // TODO: Identify source field for substance code (e.g., RxNorm for meds, UNII for food/env)
            // TODO: Determine the coding system.
            // TODO: Map source code and name.
            if (!string.IsNullOrWhiteSpace(source.SubstanceCode) || !string.IsNullOrWhiteSpace(source.SubstanceName))
            {
                allergy.Code = new CodeableConcept();
                // TODO: Add coding based on SubstanceCode and system
                // allergy.Code.Coding.Add(new Coding( /* system */, source.SubstanceCode)); // <<< FLAG
                allergy.Code.Text = source.SubstanceName; // Use name as text fallback
            }
            else
            {
                // TODO: Handle missing required substance code/name (validation will fail)
            }

            // --- Patient (Patient Reference - Mandatory) ---
            // TODO: Create a Patient reference based on source.PatientId
            if (source.PatientId.HasValue)
            {
                allergy.Patient = new ResourceReference($"Patient/{source.PatientId.Value}"); // <<< FLAG: Ensure Patient ID strategy matches
            }
            else
            {
                // TODO: Handle missing patient link (validation will fail)
            }

            // --- Encounter Reference ---
            // TODO: Identify and map Encounter reference if available in source.
            // allergy.Encounter = new ResourceReference(...);

            // --- Onset ---
            // TODO: Identify and map onset information if available.
            // allergy.Onset = new FhirDateTime(...);

            // --- Recorded Date ---
            // TODO: Map source.RecordedDate
             if (!string.IsNullOrWhiteSpace(source.RecordedDate))
             {
                 if (DateTime.TryParse(source.RecordedDate, out DateTime recordedDt))
                 {
                     allergy.RecordedDateElement = new FhirDateTime(new DateTimeOffset(recordedDt));
                 }
             }

            // --- Recorder (Practitioner/Patient/RelatedPerson Reference) ---
            // TODO: Map source.RecordedBy to a Practitioner or other reference
            // if (!string.IsNullOrWhiteSpace(source.RecordedBy))
            // {
            //     allergy.Recorder = ResolvePractitionerReference(source.RecordedBy); // <<< FLAG
            // }

            // --- Asserter (Practitioner/Patient/RelatedPerson Reference) ---
            // TODO: Map asserter if different from recorder.

            // --- Last Occurrence Date ---
            // TODO: Map if available.

            // --- Note ---
            // TODO: Map any free-text notes if available.

            // --- Reaction Component ---
            // TODO: Map source.ReactionDescription and source.Severity to the Reaction structure.
            if (!string.IsNullOrWhiteSpace(source.ReactionDescription) || !string.IsNullOrWhiteSpace(source.Severity))
            {
                var reaction = new AllergyIntolerance.ReactionComponent();
                if (!string.IsNullOrWhiteSpace(source.ReactionDescription))
                {
                    // TODO: Potentially map ReactionDescription to Manifestation CodeableConcept if coded?
                    reaction.Manifestation.Add(new CodeableConcept { Text = source.ReactionDescription }); // <<< FLAG
                }
                if (!string.IsNullOrWhiteSpace(source.Severity))
                {
                    // TODO: Map source.Severity string to AllergyIntoleranceSeverity code
                    // reaction.Severity = MapSeverity(source.Severity); // <<< FLAG
                }
                // TODO: Map reaction onset, exposure route if available.
                allergy.Reaction.Add(reaction);
            }

            // --- Set Profile ---
            allergy.Meta = new Meta();
            allergy.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-allergyintolerance" };

            return allergy;
        }

        // --- TODO: Implement Helper Methods ---
        // private static CodeableConcept MapClinicalStatus(string sourceStatus) { /* ... */ throw new NotImplementedException(); }
        // private static CodeableConcept MapVerificationStatus(string sourceVerificationStatus) { /* ... */ throw new NotImplementedException(); }
        // private static AllergyIntolerance.AllergyIntoleranceCriticality MapCriticality(string sourceCriticality) { /* ... */ throw new NotImplementedException(); }
        // private static AllergyIntolerance.AllergyIntoleranceSeverity MapSeverity(string sourceSeverity) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolvePractitionerReference(string sourcePractitionerIdOrName) { /* ... */ throw new NotImplementedException(); }
    }
}
