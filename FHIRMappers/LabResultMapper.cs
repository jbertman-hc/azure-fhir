using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class LabResultMapper
    {
        // Maps a single LabResultItem to a FHIR Observation
        public static Observation ToFhirLabResult(LabResultItem source)
        {
            if (source == null)
            {
                return null;
            }

            var observation = new Observation();

            // --- Set Resource ID ---
            // TODO: Define ID strategy (e.g., prefix + ResultId)
            if (source.ResultId.HasValue)
            {
                observation.Id = $"Observation-Lab-{source.ResultId.Value}";
            }

            // --- Based On (Reference to ServiceRequest/MedicationRequest) ---
            // TODO: Link to the original lab order (ServiceRequest) if available.

            // --- Part Of (Reference to DiagnosticReport) ---
            // TODO: Link to the parent DiagnosticReport using source.OrderId/ReportId
            if (source.OrderId.HasValue) // Assuming OrderId links to the Report
            {
                 observation.PartOf.Add(new ResourceReference($"DiagnosticReport/{source.OrderId.Value}")); // <<< FLAG: Confirm ID strategy for DiagnosticReport
            }

            // --- Status (Mandatory) ---
            // TODO: Map source.Status to FHIR ObservationStatus (registered, preliminary, final, amended, corrected, ...)
            observation.Status = ObservationStatus.Final; // <<< FLAG (Defaulting to final, needs mapping)
            // observation.Status = MapStatus(source.Status);

            // --- Category (Mandatory) ---
            // TODO: Set category, usually 'laboratory'
            observation.Category.Add(new CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category", "laboratory", "Laboratory"));

            // --- Code (LOINC Code for the Test - Mandatory) ---
            // TODO: Map source.LabCode/Name to LOINC CodeableConcept.
            if (!string.IsNullOrWhiteSpace(source.LabCode) || !string.IsNullOrWhiteSpace(source.LabName))
            {
                observation.Code = new CodeableConcept();
                if (!string.IsNullOrWhiteSpace(source.LabCode))
                {
                     observation.Code.Coding.Add(new Coding("http://loinc.org", source.LabCode)); // <<< FLAG (Confirm system is LOINC)
                }
                observation.Code.Text = source.LabName;
            }
            else
            {
                // TODO: Handle missing required test code (validation will fail)
            }

            // --- Subject (Patient Reference - Mandatory) ---
            // TODO: Create a Patient reference based on source.PatientId
            if (source.PatientId.HasValue)
            {
                observation.Subject = new ResourceReference($"Patient/{source.PatientId.Value}"); // <<< FLAG: Ensure Patient ID strategy matches
            }
            else
            {
                // TODO: Handle missing patient link (validation will fail)
            }

            // --- Focus ---
            // TODO: Link to specimen or other relevant resource if needed.

            // --- Encounter Reference ---
            // TODO: Create an Encounter reference based on source.EncounterId (if available)
            if (!string.IsNullOrWhiteSpace(source.EncounterId))
            {
                observation.Encounter = new ResourceReference($"Encounter/{source.EncounterId}"); // <<< FLAG: Define Encounter ID strategy
            }

            // --- Effective Date/Time/Period (Mandatory) ---
            // TODO: Map source.EffectiveDate (Specimen Collection or Result Time?)
            if (!string.IsNullOrWhiteSpace(source.EffectiveDate))
            {
                if (DateTime.TryParse(source.EffectiveDate, out DateTime effectiveDt))
                {
                    observation.Effective = new FhirDateTime(new DateTimeOffset(effectiveDt));
                }
                else
                {
                     observation.Effective = new FhirString(source.EffectiveDate);
                }
            }
            else
            {
                // TODO: Handle missing required effective date (validation will fail)
            }

            // --- Issued Date/Time (When result was released) ---
            // TODO: Map issued time if available and different from effective time.
            // observation.Issued = ...;

            // --- Performer ---
            // TODO: Map source.PerformerId (Performing Lab) to Organization/Practitioner reference.
            // if (!string.IsNullOrWhiteSpace(source.PerformerId))
            // {
            //     observation.Performer.Add(ResolveOrganizationReference(source.PerformerId)); // <<< FLAG (Assuming Lab is Org)
            // }

            // --- Value[x] (The Result Value - Important) ---
            // TODO: Map source.Value based on source.ValueType
            if (!string.IsNullOrWhiteSpace(source.Value))
            {
                switch (source.ValueType?.ToLowerInvariant()) // <<< FLAG (ValueType mapping needed)
                {
                    case "numeric":
                        if (decimal.TryParse(source.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal numValue))
                        {
                            // TODO: Map source.Unit to UCUM code
                            string ucumUnit = MapUnitToUcum(source.Unit); // <<< FLAG
                            string unitSystem = "http://unitsofmeasure.org";
                            observation.Value = new Quantity(numValue, ucumUnit, unitSystem);
                        }
                        else { /* Handle parsing error */ }
                        break;
                    case "coded":
                        // TODO: Map coded value (e.g., Positive/Negative) to CodeableConcept
                        // observation.Value = MapCodedValue(source.Value); // <<< FLAG
                        observation.Value = new CodeableConcept { Text = source.Value }; // Fallback
                        break;
                    case "string":
                    default:
                        observation.Value = new FhirString(source.Value);
                        break;
                }
            }
            // else { /* Consider DataAbsentReason if value is truly absent */ }

            // --- Interpretation ---
            // TODO: Map source.InterpretationCode (H, L, N, A, etc.) to Observation Interpretation CodeableConcept
            // if (!string.IsNullOrWhiteSpace(source.InterpretationCode))
            // {
            //     observation.Interpretation.Add(MapInterpretationCode(source.InterpretationCode)); // <<< FLAG
            // }

            // --- Note ---
            // TODO: Map any comments/notes associated with the result.

            // --- Body Site ---
            // TODO: Map specimen source site if available.

            // --- Method ---
            // TODO: Map test method if available.

            // --- Specimen Reference ---
            // TODO: Link to Specimen resource if managing specimens separately.

            // --- Reference Range ---
            // TODO: Map source.ReferenceRange
            if (!string.IsNullOrWhiteSpace(source.ReferenceRange))
            {
                 var range = new Observation.ReferenceRangeComponent();
                 range.Text = source.ReferenceRange;
                 // TODO: Parse low, high, type, appliesTo if possible from the string
                 // range.Low = new SimpleQuantity { ... };
                 // range.High = new SimpleQuantity { ... };
                 observation.ReferenceRange.Add(range);
            }

            // --- Has Member ---
            // TODO: Link related observations (e.g., components of a panel handled as separate Obs).

            // --- Derived From ---
            // TODO: Link source observation if this is a derived result.

            // --- Component ---
            // TODO: Use components if a single source item contains multiple related results (e.g., differential count within WBC).


            // --- Set Profile ---
            observation.Meta = new Meta();
            observation.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-observation-lab" };

            return observation;
        }

        // --- TODO: Implement Helper Methods ---
        // private static ObservationStatus MapStatus(string sourceStatus) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolvePatientReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolveEncounterReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolveOrganizationReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
        // private static string MapUnitToUcum(string sourceUnit) { /* ... */ throw new NotImplementedException(); }
        // private static CodeableConcept MapCodedValue(string sourceValue) { /* ... */ throw new NotImplementedException(); }
        // private static CodeableConcept MapInterpretationCode(string sourceCode) { /* ... */ throw new NotImplementedException(); }

    }
}
