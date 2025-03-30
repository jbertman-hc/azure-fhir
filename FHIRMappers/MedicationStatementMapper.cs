using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class MedicationStatementMapper
    {
        // Maps a MedicationStatementItem (representing an item on a patient's reported medication list)
        public static MedicationStatement ToFhirMedicationStatement(MedicationStatementItem source)
        {
            if (source == null)
            {
                return null;
            }

            var medStatement = new MedicationStatement();

            // --- Set Resource ID ---
            // TODO: Define ID strategy (e.g., prefix + StatementId)
            if (source.StatementId.HasValue)
            {
                medStatement.Id = $"MedicationStatement-{source.StatementId.Value}";
            }

            // --- Status (Mandatory) ---
            // TODO: Map source.Status to FHIR MedicationStatement Status codes (active, completed, entered-in-error, intended, stopped, on-hold, unknown, not-taken)
            // medStatement.Status = MapStatus(source.Status); // <<< FLAG

            // --- Status Reason ---
            // TODO: Map reason for status if available (e.g., why stopped/not-taken).

            // --- Category ---
            // TODO: Map category if available (e.g., inpatient, outpatient, community, patientspecified).
            // Often 'community' or 'patientspecified' for medication lists.

            // --- Medication Reference or CodeableConcept (Mandatory) ---
            // TODO: Map source.MedicationCode/Name, similar to MedicationRequest.
            // Prefer CodeableConcept here as statements often reflect patient reporting, not strict orders.
            if (!string.IsNullOrWhiteSpace(source.MedicationCode) || !string.IsNullOrWhiteSpace(source.MedicationName))
            {
                var medConcept = new CodeableConcept();
                if (!string.IsNullOrWhiteSpace(source.MedicationCode))
                {
                    // TODO: Determine coding system (RxNorm preferred for US Core)
                    medConcept.Coding.Add(new Coding("http://www.nlm.nih.gov/research/umls/rxnorm", source.MedicationCode)); // <<< FLAG (Confirm system)
                }
                medConcept.Text = source.MedicationName;
                medStatement.Medication = medConcept;
            }
            else
            {
                // TODO: Handle missing required medication info (validation will fail)
            }

            // --- Subject (Patient Reference - Mandatory) ---
            // TODO: Create a Patient reference based on source.PatientId
            if (source.PatientId.HasValue)
            {
                medStatement.Subject = new ResourceReference($"Patient/{source.PatientId.Value}"); // <<< FLAG: Ensure Patient ID strategy matches
            }
            else
            {
                // TODO: Handle missing patient link (validation will fail)
            }

            // --- Context (Encounter/EpisodeOfCare Reference) ---
            // TODO: Link to context if known (e.g., encounter where list was reconciled).

            // --- Effective Date/Time/Period ---
            // TODO: Map period over which the medication was/is being taken, if known.
            // medStatement.Effective = new Period { Start = ..., End = ... };

            // --- Date Asserted ---
            // TODO: Map source.DateAsserted (when the statement was recorded/asserted).
            if (!string.IsNullOrWhiteSpace(source.DateAsserted))
            {
                 if (DateTime.TryParse(source.DateAsserted, out DateTime assertedDt))
                 {
                     medStatement.DateAssertedElement = new FhirDateTime(new DateTimeOffset(assertedDt));
                 }
            }

            // --- Information Source ---
            // TODO: Map source.InformationSource (Patient, Practitioner, RelatedPerson, etc.)
            // if (!string.IsNullOrWhiteSpace(source.InformationSource))
            // {
            //    medStatement.InformationSource = ResolveReference(source.InformationSource); // <<< FLAG (Needs logic based on source type)
            // }

            // --- Derived From ---
            // TODO: Link to source resource (e.g., MedicationRequest, Observation) if derived.

            // --- Reason Code / Reference ---
            // TODO: Map source.ReasonCode (indication for the medication) or link to Condition.
            // if (!string.IsNullOrWhiteSpace(source.ReasonCode))
            // {
            //    medStatement.ReasonCode.Add(new CodeableConcept(/* system */, source.ReasonCode)); // <<< FLAG
            // }

            // --- Note ---
            // TODO: Map any free-text notes.

            // --- Dosage ---
            // TODO: Map source.DosageText (patient reported dosage) to Dosage structure.
            if (!string.IsNullOrWhiteSpace(source.DosageText))
            {
                var dosage = new Dosage();
                dosage.Text = source.DosageText; // Patient reported sig
                // TODO: Attempt to parse structured elements if possible, but text is often primary here.
                medStatement.Dosage.Add(dosage);
            }

            // --- Set Profile ---
            // No specific US Core profile for MedicationStatement itself, but it represents the patient's medication list.
            // medStatement.Meta = new Meta();
            // medStatement.Meta.Profile = new List<string> { ... };

            return medStatement;
        }

        // --- TODO: Implement Helper Methods ---
        // private static MedicationStatement.MedicationStatementStatus MapStatus(string sourceStatus) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolveReference(string sourceRef) { /* ... */ throw new NotImplementedException(); }

    }
}
