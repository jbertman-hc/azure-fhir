using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class ImmunizationMapper
    {
        public static Immunization ToFhirImmunization(ImmunizationItem source)
        {
            if (source == null)
            {
                return null;
            }

            var immunization = new Immunization();

            // --- Set Resource ID ---
            // TODO: Define ID strategy (e.g., prefix + ImmunizationId)
            if (source.ImmunizationId.HasValue)
            {
                immunization.Id = $"Immunization-{source.ImmunizationId.Value}";
            }

            // --- Status (Mandatory) ---
            // TODO: Map source.Status to FHIR Immunization Status codes (completed, entered-in-error, not-done)
            // immunization.Status = MapStatus(source.Status); // <<< FLAG

            // --- Status Reason ---
            // TODO: Map reason if status is 'not-done'.

            // --- Vaccine Code (Mandatory) ---
            // TODO: Map source.VaccineCode/Name to CodeableConcept.
            // TODO: Determine coding system (CVX is standard for US Core).
            if (!string.IsNullOrWhiteSpace(source.VaccineCode) || !string.IsNullOrWhiteSpace(source.VaccineName))
            {
                immunization.VaccineCode = new CodeableConcept();
                if (!string.IsNullOrWhiteSpace(source.VaccineCode))
                {
                    immunization.VaccineCode.Coding.Add(new Coding("http://hl7.org/fhir/sid/cvx", source.VaccineCode)); // <<< FLAG (Confirm system is CVX)
                }
                immunization.VaccineCode.Text = source.VaccineName;
            }
            else
            {
                // TODO: Handle missing required vaccine code (validation will fail)
            }

            // --- Patient (Patient Reference - Mandatory) ---
            // TODO: Create a Patient reference based on source.PatientId
            if (source.PatientId.HasValue)
            {
                immunization.Patient = new ResourceReference($"Patient/{source.PatientId.Value}"); // <<< FLAG: Ensure Patient ID strategy matches
            }
            else
            {
                // TODO: Handle missing patient link (validation will fail)
            }

            // --- Encounter Reference ---
            // TODO: Create an Encounter reference based on source.EncounterId (if available)
            if (!string.IsNullOrWhiteSpace(source.EncounterId))
            {
                immunization.Encounter = new ResourceReference($"Encounter/{source.EncounterId}"); // <<< FLAG: Define Encounter ID strategy
            }

            // --- Occurrence Date/Time (Mandatory) ---
            // TODO: Map source.OccurrenceDate to immunization.Occurrence[x] (dateTime or string)
            if (!string.IsNullOrWhiteSpace(source.OccurrenceDate))
            {
                if (DateTime.TryParse(source.OccurrenceDate, out DateTime occurredDt))
                {
                    immunization.Occurrence = new FhirDateTime(new DateTimeOffset(occurredDt));
                }
                else
                {
                    immunization.Occurrence = new FhirString(source.OccurrenceDate);
                }
            }
            else
            {
                // TODO: Handle missing required occurrence date (validation will fail)
            }

            // --- Primary Source (Mandatory) ---
            // TODO: Map source.PrimarySource or infer based on context (e.g., data source)
            // Indicates if the information came from the patient/caregiver or from the administering provider.
            // immunization.PrimarySource = source.PrimarySource ?? true; // <<< FLAG (Defaulting to true, needs verification)

            // --- Report Origin ---
            // TODO: Map source of the information if available (e.g., state registry).

            // --- Location Reference ---
            // TODO: Map location where immunization was given, if available.

            // --- Manufacturer ---
            // TODO: Map source.Manufacturer (potentially needs mapping to Organization resource).
            // if (!string.IsNullOrWhiteSpace(source.Manufacturer))
            // {
            //    immunization.Manufacturer = ResolveOrganizationReference(source.Manufacturer); // <<< FLAG
            // }

            // --- Lot Number ---
            // TODO: Map source.LotNumber
            if (!string.IsNullOrWhiteSpace(source.LotNumber))
            {
                immunization.LotNumber = source.LotNumber;
            }

            // --- Expiration Date ---
            // TODO: Map vaccine expiration date if available.

            // --- Site ---
            // TODO: Map source.Site to CodeableConcept (using body site codes).
            // immunization.Site = MapBodySite(source.Site); // <<< FLAG

            // --- Route ---
            // TODO: Map source.Route to CodeableConcept (using route codes).
            // immunization.Route = MapRoute(source.Route); // <<< FLAG

            // --- Dose Quantity ---
            // TODO: Map dose quantity if available.

            // --- Performer ---
            // TODO: Map source.PerformerId to Practitioner/Organization reference.
            // if (!string.IsNullOrWhiteSpace(source.PerformerId))
            // {
                 // Add performer component with function (e.g., administrator, orderer)
            //     var performer = new Immunization.PerformerComponent();
            //     performer.Function = new CodeableConcept( /* code for administrator */ ); // <<< FLAG
            //     performer.Actor = ResolvePractitionerOrOrganizationReference(source.PerformerId); // <<< FLAG
            //     immunization.Performer.Add(performer);
            // }

            // --- Note ---
            // TODO: Map any free-text notes.

            // --- Reason Code / Reference ---
            // TODO: Map reason for immunization (or not giving) if available.

            // --- Is Subpotent ---
            // TODO: Map if relevant (e.g., due to storage issues).

            // --- Program Eligibility ---
            // TODO: Map VFC eligibility etc. if available.

            // --- Funding Source ---
            // TODO: Map funding source if available.

            // --- Reaction ---
            // TODO: Link to Observation resource documenting any adverse reaction.

            // --- Protocol Applied ---
            // TODO: Map details about the protocol/series if available.


            // --- Set Profile ---
            immunization.Meta = new Meta();
            immunization.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-immunization" };

            return immunization;
        }

        // --- TODO: Implement Helper Methods ---
        // private static Immunization.ImmunizationStatus MapStatus(string sourceStatus) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolvePractitionerOrOrganizationReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
        // private static CodeableConcept MapBodySite(string sourceSite) { /* ... */ throw new NotImplementedException(); }
        // private static CodeableConcept MapRoute(string sourceRoute) { /* ... */ throw new NotImplementedException(); }

    }
}
