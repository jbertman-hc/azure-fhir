using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class VitalSignMapper
    {
        // Maps a single VitalSignItem potentially representing multiple results (like BP)
        // or a single result (like Height, Weight) to a list of FHIR Observations.
        public static List<Observation> ToFhirVitalSignObservations(VitalSignItem source)
        {
            var observations = new List<Observation>();
            if (source == null)
            {
                return observations;
            }

            // Determine if this is Blood Pressure based on type or available fields
            // TODO: Refine this logic based on actual VitalSignType values or structure
            bool isBloodPressure = source.VitalSignType?.ToLowerInvariant().Contains("blood pressure") == true ||
                                   (!string.IsNullOrWhiteSpace(source.SystolicValue) && !string.IsNullOrWhiteSpace(source.DiastolicValue));

            // --- Common Elements --- 

            // Base ID - used for individual Observations
            string baseId = null;
            if (source.VitalSignId.HasValue)
            {
                baseId = $"Observation-Vital-{source.VitalSignId.Value}";
            }
            else
            {
                // TODO: Handle missing ID - essential for linking BP components
                // Maybe generate a GUID if missing?
                baseId = $"Observation-Vital-{Guid.NewGuid()}"; // <<< FLAG: ID generation strategy
            }

            // Status (Mandatory)
            // TODO: Map source.Status to ObservationStatus (final, amended, preliminary...)
            var status = ObservationStatus.Final; // <<< FLAG (Defaulting to final)

            // Subject (Patient Reference - Mandatory)
            ResourceReference patientRef = null;
            if (source.PatientId.HasValue)
            { 
                patientRef = new ResourceReference($"Patient/{source.PatientId.Value}"); // <<< FLAG: Ensure Patient ID strategy matches
            }
            else
            {
                // TODO: Handle missing patient link (Validation Error)
                return observations; // Cannot create Obs without Subject
            }

            // Effective Date/Time (Mandatory)
            FhirDateTime effective = null;
            if (!string.IsNullOrWhiteSpace(source.EffectiveDate))
            {
                if (DateTime.TryParse(source.EffectiveDate, out DateTime effectiveDt))
                {
                    effective = new FhirDateTime(new DateTimeOffset(effectiveDt));
                }
                else
                {
                    // TODO: Handle invalid date format?
                    // effective = new FhirString(source.EffectiveDate); // Not ideal for Observation.effective[x]
                }
            }
            else
            {
                 // TODO: Handle missing effective date (Validation Error)
                 return observations; // Cannot create Obs without Effective Date
            }

            // Optional: Encounter Reference
            ResourceReference encounterRef = null;
            if (!string.IsNullOrWhiteSpace(source.EncounterId))
            {
                encounterRef = new ResourceReference($"Encounter/{source.EncounterId}"); // <<< FLAG: Define Encounter ID strategy
            }

            // Optional: Performer Reference
            List<ResourceReference> performerRefs = new List<ResourceReference>();
            // if (!string.IsNullOrWhiteSpace(source.PerformerId))
            // {
            //     performerRefs.Add(ResolvePractitionerReference(source.PerformerId)); // <<< FLAG
            // }

            // --- Create Observation(s) ---

            if (isBloodPressure)
            {
                // Create Systolic Observation
                if (!string.IsNullOrWhiteSpace(source.SystolicValue))
                {
                    var systolicObs = CreateBaseObservation(baseId + "-systolic", status, patientRef, effective, encounterRef, performerRefs);
                    // Mandatory: Category (vital-signs)
                    systolicObs.Category.Add(new CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category", "vital-signs", "Vital Signs"));
                    // Mandatory: Code (LOINC for Systolic)
                    systolicObs.Code = new CodeableConcept("http://loinc.org", "8480-6", "Systolic blood pressure"); // <<< FLAG (Confirm LOINC)
                    // Mandatory: ValueQuantity
                    if (decimal.TryParse(source.SystolicValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal sysValue))
                    {
                        // TODO: Confirm unit mapping for BP (mmHg)
                        systolicObs.Value = new Quantity(sysValue, "mm[Hg]", "http://unitsofmeasure.org"); // <<< FLAG (Confirm UCUM)
                    }
                    // TODO: Add Interpretation, BodySite, Method if available
                    // Set US Core Profile
                    systolicObs.Meta = new Meta { Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-blood-pressure" } };
                    observations.Add(systolicObs);
                }

                // Create Diastolic Observation
                if (!string.IsNullOrWhiteSpace(source.DiastolicValue))
                {
                    var diastolicObs = CreateBaseObservation(baseId + "-diastolic", status, patientRef, effective, encounterRef, performerRefs);
                    diastolicObs.Category.Add(new CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category", "vital-signs", "Vital Signs"));
                    diastolicObs.Code = new CodeableConcept("http://loinc.org", "8462-4", "Diastolic blood pressure"); // <<< FLAG (Confirm LOINC)
                    if (decimal.TryParse(source.DiastolicValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal diasValue))
                    {
                        diastolicObs.Value = new Quantity(diasValue, "mm[Hg]", "http://unitsofmeasure.org"); // <<< FLAG (Confirm UCUM)
                    }
                    diastolicObs.Meta = new Meta { Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-blood-pressure" } };
                    observations.Add(diastolicObs);
                }

                // If both components were added, link them using HasMember
                if (observations.Count == 2)
                {
                    // Option 1: Create a parent BP Observation linking the components (Preferred by US Core)
                    var bpPanel = CreateBaseObservation(baseId, status, patientRef, effective, encounterRef, performerRefs);
                    bpPanel.Category.Add(new CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category", "vital-signs", "Vital Signs"));
                    bpPanel.Code = new CodeableConcept("http://loinc.org", "85354-9", "Blood pressure panel with all children optional"); // <<< FLAG (Confirm LOINC)
                    bpPanel.HasMember.Add(new ResourceReference($"#{observations[0].Id}")); // Reference systolic
                    bpPanel.HasMember.Add(new ResourceReference($"#{observations[1].Id}")); // Reference diastolic
                    bpPanel.Meta = new Meta { Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-blood-pressure" } };
                    
                    // Make components contained within the panel
                    observations[0].Id = observations[0].Id.Replace(baseId + "-", ""); // Make ID relative
                    observations[1].Id = observations[1].Id.Replace(baseId + "-", ""); // Make ID relative
                    bpPanel.Contained.Add(observations[0]);
                    bpPanel.Contained.Add(observations[1]);

                    observations.Clear(); // Clear individual obs
                    observations.Add(bpPanel); // Add the panel instead

                    // Option 2 (Simpler, less common): Link components directly without panel
                    // observations[0].DerivedFrom.Add(new ResourceReference($"Observation/{observations[1].Id}"));
                    // observations[1].DerivedFrom.Add(new ResourceReference($"Observation/{observations[0].Id}"));
                }
                 else if (observations.Count == 1)
                 {
                      // Only one component found - TODO: Decide if this is valid or requires error handling / different profile
                 }
            }
            else // Handle other vital signs (assuming one vital per VitalSignItem source)
            {
                var observation = CreateBaseObservation(baseId, status, patientRef, effective, encounterRef, performerRefs);
                observation.Category.Add(new CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category", "vital-signs", "Vital Signs"));

                bool vitalMapped = false;
                string profileUrl = null;
                Quantity valueQuantity = null;

                // Parse the value and unit
                if (!string.IsNullOrWhiteSpace(source.Value) && decimal.TryParse(source.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal vitalValue))
                {
                     // TODO: Map source.Unit to UCUM code. Add more cases as needed.
                     string ucumUnit = source.Unit; // Default to source unit
                     string ucumSystem = "http://unitsofmeasure.org";
                     switch (source.Unit?.ToLowerInvariant())
                     {
                         case "cm": ucumUnit = "cm"; break;
                         case "in": ucumUnit = "[in_i]"; break; // Inch_international
                         case "kg": ucumUnit = "kg"; break;
                         case "lb":
                         case "lbs": ucumUnit = "[lb_av]"; break; // Pound_avoirdupois
                         case "c":
                         case "cel": ucumUnit = "Cel"; break; // Celsius
                         case "f":
                         case "degf": ucumUnit = "[degF]"; break; // Fahrenheit
                         case "/min":
                         case "bpm": ucumUnit = "/min"; break;
                         case "%": ucumUnit = "%"; break;
                         // <<< FLAG: Add more unit mappings (e.g., kg/m2 for BMI if source provides it)
                         default: ucumSystem = null; ucumUnit = source.Unit; break; // Use original unit if not mapped
                     }
                     valueQuantity = new Quantity(vitalValue, ucumUnit, ucumSystem);
                     if (ucumSystem == null && !string.IsNullOrWhiteSpace(ucumUnit))
                     {
                        valueQuantity.Unit = ucumUnit; // Set unit text if system unknown
                     }
                }
                 else
                 {
                      // TODO: Handle non-numeric or missing value - maybe use dataAbsentReason?
                 }

                // TODO: Determine vital type reliably (e.g., from source.VitalSignType)
                // This example uses the presence of specific value fields, which might not be ideal.
                // A switch on source.VitalSignType is generally better.
                // switch (source.VitalSignType?.ToLowerInvariant())

                // Example mapping based on assumed common VitalSignType values or presence of specific fields:
                if (source.VitalSignType?.ToLowerInvariant().Contains("height") == true) // <<< FLAG: Adjust condition based on actual source data
                {
                    observation.Code = new CodeableConcept("http://loinc.org", "8302-2", "Body height"); // <<< FLAG (Confirm LOINC)
                    profileUrl = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-body-height";
                    observation.Value = valueQuantity;
                    vitalMapped = true;
                }
                else if (source.VitalSignType?.ToLowerInvariant().Contains("weight") == true) // <<< FLAG: Adjust condition
                {
                    observation.Code = new CodeableConcept("http://loinc.org", "29463-7", "Body weight"); // <<< FLAG (Confirm LOINC)
                    profileUrl = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-body-weight";
                    observation.Value = valueQuantity;
                    vitalMapped = true;
                }
                else if (source.VitalSignType?.ToLowerInvariant().Contains("bmi") == true || source.VitalSignType?.ToLowerInvariant().Contains("body mass index") == true) // <<< FLAG: Adjust condition
                {
                    observation.Code = new CodeableConcept("http://loinc.org", "39156-5", "Body mass index (BMI) [Ratio]"); // <<< FLAG (Confirm LOINC)
                    profileUrl = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-bmi";
                    // BMI unit is typically kg/m2
                    if (valueQuantity != null && valueQuantity.Unit != "kg/m2")
                    {
                         // <<< FLAG: Need to ensure BMI value is in kg/m2 or handle unit conversion/clarification
                         // Or if source unit isn't kg/m2, map it appropriately.
                         if(valueQuantity.Unit == null) valueQuantity.Unit = "kg/m2"; // Assume if unitless
                         if(valueQuantity.System == null) valueQuantity.System = "http://unitsofmeasure.org";
                    }
                    observation.Value = valueQuantity;
                    vitalMapped = true;
                }
                else if (source.VitalSignType?.ToLowerInvariant().Contains("temperature") == true) // <<< FLAG: Adjust condition
                {
                    observation.Code = new CodeableConcept("http://loinc.org", "8310-5", "Body temperature"); // <<< FLAG (Confirm LOINC)
                    profileUrl = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-body-temperature";
                    observation.Value = valueQuantity;
                    vitalMapped = true;
                }
                 else if (source.VitalSignType?.ToLowerInvariant().Contains("heart rate") == true) // <<< FLAG: Adjust condition
                {
                    observation.Code = new CodeableConcept("http://loinc.org", "8867-4", "Heart rate"); // <<< FLAG (Confirm LOINC)
                    profileUrl = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-heart-rate";
                    observation.Value = valueQuantity;
                    vitalMapped = true;
                }
                 else if (source.VitalSignType?.ToLowerInvariant().Contains("respiratory rate") == true) // <<< FLAG: Adjust condition
                {
                    observation.Code = new CodeableConcept("http://loinc.org", "9279-1", "Respiratory rate"); // <<< FLAG (Confirm LOINC)
                    profileUrl = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-respiratory-rate";
                    observation.Value = valueQuantity;
                    vitalMapped = true;
                }
                 else if (source.VitalSignType?.ToLowerInvariant().Contains("oxygen saturation") == true) // <<< FLAG: Adjust condition
                {
                    observation.Code = new CodeableConcept("http://loinc.org", "59408-5", "Oxygen saturation in Arterial blood by Pulse oximetry"); // Using SpO2 LOINC <<< FLAG (Confirm LOINC - 2708-6 is also common)
                    profileUrl = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-spo2";
                    observation.Value = valueQuantity;
                    vitalMapped = true;
                }
                // <<< Add more vital signs here (e.g., Head Circumference 8287-5) >>>

                if (vitalMapped && observation.Value != null)
                {
                    if (!string.IsNullOrWhiteSpace(profileUrl))
                    {
                        observation.Meta = new Meta { Profile = new List<string> { profileUrl } };
                    }
                    observations.Add(observation);
                }
                 else
                 {
                      // TODO: Handle unmapped vital sign type or missing/invalid value
                 }
            }

            return observations;
        }

        // Helper to create a basic Observation shell with common fields
        private static Observation CreateBaseObservation(
            string id,
            ObservationStatus status,
            ResourceReference subject,
            FhirDateTime effective,
            ResourceReference encounter,
            List<ResourceReference> performers)
        {
            var obs = new Observation();
            if (!string.IsNullOrWhiteSpace(id))
            {
                 obs.Id = id;
            }
            obs.Status = status;
            obs.Subject = subject;
            obs.Effective = effective;
            if (encounter != null)
            {
                 obs.Encounter = encounter;
            }
            if (performers != null && performers.Count > 0)
            {
                 obs.Performer.AddRange(performers);
            }
            return obs;
        }

         // --- TODO: Implement Helper Methods ---
        // private static ResourceReference ResolvePractitionerReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
    }
}
