using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class MedicationRequestMapper
    {
        public static MedicationRequest ToFhirMedicationRequest(MedicationRequestItem source)
        {
            if (source == null)
            {
                return null;
            }

            var medRequest = new MedicationRequest();

            // --- Set Resource ID ---
            // TODO: Define ID strategy (e.g., prefix + RequestId)
            if (source.RequestId.HasValue)
            {
                medRequest.Id = $"MedicationRequest-{source.RequestId.Value}";
            }

            // --- Status (Mandatory) ---
            // TODO: Map source.Status to FHIR MedicationRequest Status codes (active, on-hold, cancelled, completed, entered-in-error, stopped, draft, unknown)
            // medRequest.Status = MapStatus(source.Status); // <<< FLAG

            // --- Status Reason ---
            // TODO: Map reason for status if available.

            // --- Intent (Mandatory) ---
            // TODO: Determine intent (proposal, plan, order, original-order, reflex-order, filler-order, instance-order, option)
            // Typically 'order' for prescriptions from an EHR.
            medRequest.Intent = MedicationRequest.MedicationRequestIntent.Order; // <<< FLAG (Confirm intent)

            // --- Category ---
            // TODO: Determine category if possible (e.g., inpatient, outpatient, community, discharge)
            // medRequest.Category.Add(new CodeableConcept(...));

            // --- Priority ---
            // TODO: Map priority if available (routine, urgent, asap, stat)

            // --- Do Not Perform ---
            // TODO: Map if applicable (usually false for requests)
            // medRequest.DoNotPerform = false;

            // --- Reported ---
            // TODO: Determine if reported by Patient/Practitioner vs. prescribed.
            // Typically false if this represents an order.
            // medRequest.Reported = new FhirBoolean(false);

            // --- Medication Reference or CodeableConcept (Mandatory) ---
            // TODO: Map source.MedicationCode/Name to either a contained Medication resource or a CodeableConcept.
            // US Core prefers referencing a separate Medication resource if possible, otherwise use CodeableConcept.
            if (!string.IsNullOrWhiteSpace(source.MedicationCode) || !string.IsNullOrWhiteSpace(source.MedicationName))
            {
                // Option 1: CodeableConcept (Simpler, US Core allows this)
                var medConcept = new CodeableConcept();
                if (!string.IsNullOrWhiteSpace(source.MedicationCode))
                {
                    // TODO: Determine coding system (RxNorm is preferred for US Core)
                    medConcept.Coding.Add(new Coding("http://www.nlm.nih.gov/research/umls/rxnorm", source.MedicationCode)); // <<< FLAG (Confirm system)
                }
                medConcept.Text = source.MedicationName;
                medRequest.Medication = medConcept;

                // Option 2: Reference a Contained Medication (More complex, better practice)
                // var medication = new Medication { Code = medConcept, Id = $"med-{source.RequestId ?? 'temp'}" };
                // medRequest.Contained.Add(medication);
                // medRequest.Medication = new ResourceReference($"#{medication.Id}");
            }
            else
            {
                // TODO: Handle missing required medication info (validation will fail)
            }

            // --- Subject (Patient Reference - Mandatory) ---
            // TODO: Create a Patient reference based on source.PatientId
            if (source.PatientId.HasValue)
            {
                medRequest.Subject = new ResourceReference($"Patient/{source.PatientId.Value}"); // <<< FLAG: Ensure Patient ID strategy matches
            }
            else
            {
                // TODO: Handle missing patient link (validation will fail)
            }

            // --- Encounter Reference ---
            // TODO: Create an Encounter reference based on source.EncounterId (if available)
            if (!string.IsNullOrWhiteSpace(source.EncounterId))
            {
                medRequest.Encounter = new ResourceReference($"Encounter/{source.EncounterId}"); // <<< FLAG: Define Encounter ID strategy
            }

            // --- Supporting Information ---
            // TODO: Link other relevant resources (e.g., Condition for indication) if available.

            // --- Authored On Date (Mandatory if status is active/on-hold/completed/...) ---
            // TODO: Map source.DateWritten
            if (!string.IsNullOrWhiteSpace(source.DateWritten))
            {
                if (DateTime.TryParse(source.DateWritten, out DateTime authoredDt))
                {
                    medRequest.AuthoredOnElement = new FhirDateTime(new DateTimeOffset(authoredDt));
                }
            }
            else
            {
                // TODO: Handle potentially missing required authoredOn date (validation rules depend on status)
            }

            // --- Requester (Practitioner/Organization/Patient... Reference) ---
            // TODO: Map source.PrescriberId to a Practitioner reference
            // if (!string.IsNullOrWhiteSpace(source.PrescriberId))
            // {
            //     medRequest.Requester = ResolvePractitionerReference(source.PrescriberId); // <<< FLAG
            // }

            // --- Performer ---
            // TODO: Map performer (e.g., specific nurse) if available.

            // --- Recorder ---
            // TODO: Map recorder if available and different from requester.

            // --- Reason Code / Reference ---
            // TODO: Map indication/reason for the medication if available (link to Condition?).

            // --- Course Of Therapy Type ---
            // TODO: Map if available (e.g., acute, continuous, seasonal).

            // --- Insurance ---
            // TODO: Link to Coverage resource if available.

            // --- Note ---
            // TODO: Map any free-text notes.

            // --- Dosage Instruction ---
            // TODO: Map source.DosageInstruction (SIG) to Dosage structure.
            if (!string.IsNullOrWhiteSpace(source.DosageInstruction))
            {
                var dosage = new Dosage();
                dosage.Text = source.DosageInstruction; // Simple text mapping
                // TODO: Add structured dosage elements if possible (timing, route, doseAndRate, etc.)
                medRequest.DosageInstruction.Add(dosage);
            }

            // --- Dispense Request ---
            // TODO: Map refills, quantity, expected supply duration if available.
            var dispenseRequest = new MedicationRequest.DispenseRequestComponent();
            bool dispensePopulated = false;
            // TODO: Map source.Refills (needs parsing from string to integer)
            // if (int.TryParse(source.Refills, out int refills))
            // {
            //    dispenseRequest.NumberOfRepeatsAllowed = refills;
            //    dispensePopulated = true;
            // }
            // TODO: Map source.Quantity to SimpleQuantity
            // if (!string.IsNullOrWhiteSpace(source.Quantity))
            // {
            //     dispenseRequest.Quantity = new SimpleQuantity { Value = decimal.Parse(source.Quantity) /* Needs parsing & units */ }; // <<< FLAG (Needs units)
            //     dispensePopulated = true;
            // }
            // TODO: Map expected supply duration if available
            // TODO: Map dispense validity period (source.StartDate, source.EndDate?)
            // if (dispensePopulated)
            // {
            //     medRequest.DispenseRequest = dispenseRequest;
            // }

            // --- Substitution ---
            // TODO: Map substitution allowance if available.

            // --- Prior Prescription ---
            // TODO: Link to previous prescription if this is a renewal/change.

            // --- Detected Issues ---
            // TODO: Link to any related alerts/issues (e.g., Drug-Drug Interaction from source).

            // --- Event History ---
            // TODO: Map any fulfillment/administration events if available in source.


            // --- Set Profile ---
            medRequest.Meta = new Meta();
            medRequest.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-medicationrequest" };

            return medRequest;
        }

        // --- TODO: Implement Helper Methods ---
        // private static MedicationRequest.MedicationrequestStatus MapStatus(string sourceStatus) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolvePractitionerReference(string sourcePractitionerIdOrName) { /* ... */ throw new NotImplementedException(); }

    }
}
