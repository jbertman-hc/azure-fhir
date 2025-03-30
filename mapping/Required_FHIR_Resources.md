# Core FHIR Resources for EHR/PHR Mapping (US Core Focus)

This document outlines the essential FHIR R4 resources typically required to represent core clinical and administrative data for an EHR/PHR application, with a focus on achieving US Core conformance.

## Key Resource Domains and FHIR Mappings

1.  **Patient Demographics:**
    *   **FHIR Resource:** `Patient` ([US Core Patient Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-patient))
    *   **Source Data:** Primarily `DemographicsDomain` (from `/api/Demographics`).
    *   **Status:** Initial mapping started in `Patient_Mapping.md` and `PatientMapper.cs`.

2.  **Problems / Conditions:**
    *   **FHIR Resource:** `Condition` ([US Core Condition Problems and Health Concerns Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-condition-problems-health-concerns))
    *   **Source Data:** Need to identify source (e.g., `/api/ProblemList`, `/api/Diagnoses`, fields within Encounters).
    *   **Considerations:** Mapping diagnosis codes (ICD-10), status (active, resolved), onset dates.

3.  **Allergies and Intolerances:**
    *   **FHIR Resource:** `AllergyIntolerance` ([US Core AllergyIntolerance Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-allergyintolerance))
    *   **Source Data:** Need to identify source (e.g., `/api/Allergies`, `allergiesDemo` field in `DemographicsDomain`?).
    *   **Considerations:** Mapping substances (RxNorm, UNII), reaction details, criticality, status (active, inactive, resolved).

4.  **Medications:**
    *   **FHIR Resources:**
        *   `MedicationRequest` ([US Core MedicationRequest Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-medicationrequest)): For active prescriptions/orders.
        *   `MedicationStatement` ([US Core MedicationStatement Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-medicationstatement)): For patient-reported medications or historical lists.
        *   `Medication` ([US Core Medication Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-medication)): Referenced by the above, defines the medication product.
    *   **Source Data:** Need to identify source (e.g., `/api/Medications`, `/api/Prescriptions`). The `takesNoMeds` flag in `DemographicsDomain` is relevant.
    *   **Considerations:** Mapping medication codes (RxNorm), dosages, sigs (directions), status, prescribers.

5.  **Immunizations:**
    *   **FHIR Resource:** `Immunization` ([US Core Immunization Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-immunization))
    *   **Source Data:** Need to identify source (e.g., `/api/Immunizations`). The VFC fields in `DemographicsDomain` might be relevant.
    *   **Considerations:** Mapping vaccine codes (CVX), dates administered, status.

6.  **Vital Signs:**
    *   **FHIR Resource:** `Observation` ([US Core Vital Signs Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-vital-signs))
    *   **Source Data:** Need to identify source (e.g., `/api/Vitals`, within Encounters).
    *   **Considerations:** Mapping specific vitals (Height, Weight, BP, Temp, HR, RR, BMI, SpO2) using LOINC codes, values, units (UCUM), dates.

7.  **Procedures:**
    *   **FHIR Resource:** `Procedure` ([US Core Procedure Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-procedure))
    *   **Source Data:** Need to identify source (e.g., `/api/Procedures`, within Encounters).
    *   **Considerations:** Mapping procedure codes (CPT, SNOMED CT), dates, performers, status.

8.  **Encounters:**
    *   **FHIR Resource:** `Encounter` ([US Core Encounter Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-encounter))
    *   **Source Data:** Likely `/api/ListEncounter` or similar.
    *   **Considerations:** Mapping encounter types, dates/times, participants (practitioners), locations, linked diagnoses (`Encounter.diagnosis`). **Crucially, may contain source note data.**

9.  **Clinical Notes:**
    *   **FHIR Resources:**
        *   `DocumentReference` ([US Core DocumentReference Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-documentreference)): Primary resource to represent a note's metadata.
        *   `Composition`: Can be used to structure notes with sections (referenced by `DocumentReference`).
        *   `Binary`: Can hold the note content (e.g., plain text, PDF, RTF) referenced by `DocumentReference.content.attachment.url` or embedded in `DocumentReference.content.attachment.data` (Base64).
    *   **Source Data:** Potentially concatenated text strings within `ListEncounter` responses or dedicated note endpoints.
    *   **Challenge:** If notes are just concatenated text strings within an encounter payload:
        *   **Option 1 (Simple):** Create a `DocumentReference` linked to the `Encounter`. Store the entire concatenated string as Base64 text in `DocumentReference.content.attachment.data` with `contentType` = `text/plain`. Assign a suitable `DocumentReference.type` (e.g., Progress Note LOINC code).
        *   **Option 2 (Structured - if possible):** If the concatenated string has *discernible sections* based on delimiters or patterns, attempt to parse it and create a `Composition` resource with structured sections (`Composition.section`). Link the `Composition` from the `DocumentReference`. Store the original raw string in a `Binary` referenced by the `DocumentReference` for fidelity.
        *   **Metadata:** Extract date, author, status from the encounter or source data for the `DocumentReference`.

10. **Laboratory Results:**
    *   **FHIR Resource:** `Observation` ([US Core Laboratory Result Observation Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-observation-lab))
    *   **Source Data:** Need to identify source (e.g., `/api/Labs`, `/api/Results`). `LabDemographicsDomain` might be relevant metadata.
    *   **Considerations:** Mapping test codes (LOINC), values, units (UCUM), reference ranges, status, dates.

11. **Diagnostic Reports (Imaging, Pathology, etc.):**
    *   **FHIR Resource:** `DiagnosticReport` ([US Core DiagnosticReport Profile for Report and Note Exchange](http://hl7.org/fhir/us/core/StructureDefinition/us-core-diagnosticreport-note))
    *   **Source Data:** Need to identify source (e.g., `/api/ImagingResults`, `/api/Reports`).
    *   **Considerations:** Linking results (`Observation`), presented form (`Attachment` - PDF, text), status, codes.

12. **Care Plans:**
    *   **FHIR Resource:** `CarePlan` ([US Core CarePlan Profile](http://hl7.org/fhir/us/core/StructureDefinition/us-core-careplan))
    *   **Source Data:** Need to identify source (e.g., `/api/CarePlan`).
    *   **Considerations:** Representing goals, activities, addresses (linked `Condition`s).

13. **Supporting Resources:**
    *   `Practitioner`: Represents doctors, nurses, etc. (Referenced by many resources).
    *   `Organization`: Represents hospitals, clinics, labs. (Referenced by many resources).
    *   `Location`: Represents physical locations. (Referenced by `Encounter`).
    *   `Coverage`: Represents insurance information (See `Patient_Mapping.md`).
    *   `ServiceRequest`: Represents orders (Labs, Imaging, Referrals). Source needed.

## Next Steps

1.  Identify the specific API endpoints and source data structures corresponding to each FHIR resource domain listed above.
2.  Prioritize the order of mapping based on core EHR/PHR functionality required.
3.  For each domain, create detailed mapping documentation (similar to `Patient_Mapping.md`) and implement the C# mapper class.
4.  Address the specific strategy for handling concatenated notes within Encounters.
