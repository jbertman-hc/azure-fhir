using Hl7.Fhir.Model;
using Hl7.Fhir.R4;
using ClaudeAcDirectSql.Models;
using System.Collections.Generic;
using System;

namespace ClaudeAcDirectSql.FHIRMappers
{
    public static class EncounterMapper
    {
        public static Encounter ToFhirEncounter(EncounterItem source)
        {
            if (source == null)
            {
                return null;
            }

            var encounter = new Encounter();

            // --- Set Resource ID ---
            // <<< FLAG: Define final ID strategy (e.g., prefix + EncounterId) >>>
            if (source.EncounterId.HasValue)
            {
                encounter.Id = $"Encounter-{source.EncounterId.Value}";
            }

            // --- Status (Mandatory) ---
            // <<< FLAG: Map source.Status to FHIR Encounter Status codes (planned, arrived, triaged, in-progress, onleave, finished, cancelled, ...) >>>
            // encounter.Status = MapStatus(source.Status);
            encounter.Status = Encounter.EncounterStatus.Unknown; // Placeholder until mapping function exists

            // --- Class (Mandatory) ---
            // <<< FLAG: Map source.EncounterType to Encounter Class Coding (e.g., AMB, IMP, EMER, HH, ... from system http://terminology.hl7.org/CodeSystem/v3-ActCode) >>>
            // encounter.Class = MapEncounterClass(source.EncounterType); // Placeholder until mapping function exists
            // Setting a default for validation - needs actual mapping
            encounter.Class = new Coding("http://terminology.hl7.org/CodeSystem/v3-ActCode", "AMB", "ambulatory");

            // --- Type ---
            // <<< FLAG: Map source.EncounterType to Encounter Type CodeableConcept (more specific than class, e.g., office visit, follow-up) >>>
            // encounter.Type.Add(MapEncounterType(source.EncounterType));

            // --- Service Type ---
            // TODO: Map service type if available (e.g., Cardiology, General Practice).

            // --- Priority ---
            // TODO: Map encounter priority if available.

            // --- Subject (Patient Reference - Mandatory) ---
            // <<< FLAG: Ensure Patient ID strategy matches where Patient references are created >>>
            if (source.PatientId.HasValue)
            {
                encounter.Subject = new ResourceReference($"Patient/{source.PatientId.Value}");
            }
            else
            {
                // <<< CRITICAL FLAG: Encounter MUST have a subject. Handle missing patient link. >>>
                 // Throw exception? Return null? Log error? Depends on system requirements.
                 // For now, let it be null, but this will fail validation.
            }

            // --- Episode Of Care ---
            // TODO: Link to EpisodeOfCare if managing longitudinal care episodes.

            // --- Based On ---
            // TODO: Link to ServiceRequest if this encounter fulfills one.

            // --- Participant ---
            // <<< FLAG: Assumes source.ParticipantId contains the ReferProviderID for the Practitioner >>>
            // <<< FLAG: Participant Type needs mapping (e.g., PPRF - primary performer). Is this determinable from EncounterItem? >>>
            if (!string.IsNullOrWhiteSpace(source.ParticipantId))
            {
                 if (int.TryParse(source.ParticipantId, out int practitionerId))
                 {
                     // <<< FLAG: This creates a reference only. If the Practitioner resource needs to be *contained*, it must be added to encounter.Contained >>>
                     // <<< FLAG: Consider error handling if GetFhirPractitioner returns null >>>
                     var practitionerReference = new ResourceReference($"Practitioner/{PractitionerMapper.GetFhirPractitioner(practitionerId)?.Id}");

                     if (practitionerReference.Reference != null) // Check if ID was generated
                     {
                        var participant = new Encounter.ParticipantComponent();
                        // TODO: Determine participant type code (e.g., primary performer PPRF, admitting ADM, consultant CON)
                        participant.Type.Add(new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-ParticipationType", "PPRF", "primary performer")); // Defaulting to PPRF
                        participant.Individual = practitionerReference;
                        encounter.Participant.Add(participant);
                     }
                     else
                     {
                         // <<< FLAG: Log or handle failure to create practitioner reference (e.g., practitioner ID not found by mapper) >>>
                     }
                 }
                 else
                 {
                      // <<< FLAG: Log or handle error if ParticipantId is not a valid integer >>>
                 }
            }
            // TODO: Add other participants if available (e.g., admitting physician, different role).

            // --- Appointment ---
            // TODO: Link to Appointment resource if scheduled.

            // --- Period (Mandatory) ---
            // <<< FLAG: Ensure source date/time format is reliably parsable. Consider timezones. >>>
            encounter.Period = new Period();
            bool periodSet = false;
            if (!string.IsNullOrWhiteSpace(source.StartDate))
            {
                 if (DateTime.TryParse(source.StartDate, out DateTime startDt))
                 {
                     encounter.Period.StartElement = new FhirDateTime(new DateTimeOffset(startDt));
                     periodSet = true;
                 }
            }
            if (!string.IsNullOrWhiteSpace(source.EndDate))
            {
                 if (DateTime.TryParse(source.EndDate, out DateTime endDt))
                 {
                     encounter.Period.EndElement = new FhirDateTime(new DateTimeOffset(endDt));
                     periodSet = true;
                 }
            }
            if (!periodSet)
            {
                // <<< CRITICAL FLAG: Encounter MUST have a period. Handle missing required period. >>>
                 // Throw exception? Return null? Log error? Depends on system requirements.
                 // For now, let it be null, but this will fail validation.
            }

            // --- Length ---
            // TODO: Calculate duration if start and end are present.

            // --- Reason Code / Reference ---
            // TODO: Map source.ReasonCode or link to Condition/Procedure/Observation/ImmunizationRecommendation.
            // if (!string.IsNullOrWhiteSpace(source.ReasonCode))
            // {
            //    encounter.ReasonCode.Add(new CodeableConcept(/* system */, source.ReasonCode)); // <<< FLAG
            // }

            // --- Diagnosis ---
            // TODO: Map source.DiagnosisCode or link to Condition/Procedure resource.
            // if (!string.IsNullOrWhiteSpace(source.DiagnosisCode))
            // {
            //     var diagnosis = new Encounter.DiagnosisComponent();
            //     // TODO: Determine use (e.g., admission, discharge diagnosis)
            //     diagnosis.Use = new CodeableConcept(/* system */, /* code */); // <<< FLAG
            //     diagnosis.Condition = ResolveConditionReference(source.DiagnosisCode); // <<< FLAG (Or map code directly)
                 // TODO: Determine Rank if multiple diagnoses.
            //     encounter.Diagnosis.Add(diagnosis);
            // }

            // --- Account ---
            // TODO: Link to Account resource for billing.

            // --- Hospitalization ---
            // TODO: Map hospitalization details if applicable (admit source, discharge disposition, etc.).
            // encounter.Hospitalization = new Encounter.HospitalizationComponent { /* ... */ }; // <<< FLAG

            // --- Location ---
            // TODO: Map source.LocationId to Encounter.LocationComponent (link to Location resource).
            // if (!string.IsNullOrWhiteSpace(source.LocationId))
            // {
            //     var location = new Encounter.LocationComponent();
            //     location.Location = ResolveLocationReference(source.LocationId); // <<< FLAG
            //     // TODO: Determine status (planned, active, completed) and period for the location if available
            //     encounter.Location.Add(location);
            // }

            // --- Service Provider (Organization Reference) ---
            // <<< FLAG: Assumes source.ServiceProviderId contains the PracticeInfo.ID for the Organization >>>
            if (!string.IsNullOrWhiteSpace(source.ServiceProviderId))
            {
                if (int.TryParse(source.ServiceProviderId, out int organizationId))
                {
                    // <<< FLAG: This creates a reference only. If the Organization resource needs to be *contained*, it must be added to encounter.Contained >>>
                    // <<< FLAG: Consider error handling if GetFhirOrganization returns null >>>
                    encounter.ServiceProvider = new ResourceReference($"Organization/{OrganizationMapper.GetFhirOrganization(organizationId)?.Id}");
                }
                 else
                 {
                      // <<< FLAG: Log or handle error if ServiceProviderId is not a valid integer >>>
                 }
            }

            // --- Part Of ---
            // TODO: Link to larger encounter if this is part of one (e.g., inpatient stay).

            // --- NOTE MAPPING --- <<< CRITICAL FLAG
             // TODO: Decide strategy for source.NoteText:
             // 1. Create a DocumentReference referencing this Encounter.
             // 2. Create a Composition referencing this Encounter.
             // 3. Store *brief* notes in encounter.reasonCode.text or encounter.type.text if appropriate (unlikely for full notes).
             // This mapper currently DOES NOT map the note text directly.
             // A separate process/mapper might handle creating DocumentReference/Composition from Encounters or Notes API.

            // --- Set Profile ---
            // No specific US Core profile for Encounter itself, but it's fundamental.
            // encounter.Meta = new Meta();
            // encounter.Meta.Profile = new List<string> { ... };

            return encounter;
        }

        // --- TODO: Implement Helper Methods ---
        // private static Encounter.EncounterStatus MapStatus(string sourceStatus) { /* ... */ throw new NotImplementedException(); }
        // private static Coding MapEncounterClass(string sourceEncounterType) { /* ... */ throw new NotImplementedException(); }
        // private static CodeableConcept MapEncounterType(string sourceEncounterType) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolvePractitionerReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolveConditionReference(string sourceCodeOrId) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolveLocationReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }
        // private static ResourceReference ResolveOrganizationReference(string sourceIdOrName) { /* ... */ throw new NotImplementedException(); }

    }
}
