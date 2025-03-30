# Project: Legacy EHR to FHIR R4 Mapping

## Goal

To create a comprehensive FHIR R4 representation of the data accessible via the legacy EHR's backend API (defined in `swagger.json`). This FHIR layer should be suitable for powering presentation-only EHR (provider) and PHR (patient) applications, replicating the core functionality of the original Amazing Charts system.

## Source Artifacts

*   **API Definition:** `swagger.json` - Defines the primary contract for data interaction. This is the most critical source as it represents what the API actually exposes.
*   **SQL Data Structures:** `DataAccess/SQLPocos/` - C# classes representing the likely structure of data in the underlying SQL Server database used by the legacy system and the API (when in SQL mode). Useful for understanding potential data fields.
*   **SQL Data Access Logic:** `DataAccess/SQLRepos/` - C# classes showing how data *might* be retrieved from SQL Server (via direct queries or potentially calling stored procedures). Provides context on data origin and relationships.

## Key Challenge

The source code for stored procedures used by the legacy system is unavailable. These procedures likely contain significant business logic, data transformation rules, and handle implicit relationships crucial for accurate data representation. This necessitates a more analytical and iterative approach to mapping.

## Mapping Approach

The mapping will be performed systematically, resource by resource (e.g., Patient, Condition, MedicationRequest, etc.), using an iterative process combining artifact analysis and domain knowledge:

**Phase 1: Artifact Analysis & Initial Mapping Proposal (Per FHIR Resource)**

1.  **Analyze Swagger:** Identify relevant API endpoints and response schemas for the target FHIR resource.
2.  **Correlate with POCOs:** Match Swagger schemas to corresponding C# POCOs to understand the underlying SQL data structure. Note discrepancies.
3.  **Contextualize with Repos:** Examine related repository methods for clues about data retrieval logic (where available and not obscured by stored procedure calls).
4.  **Propose Initial FHIR Mapping:** Map fields from the **Swagger response schema** to the target FHIR resource elements.
5.  **Flag Gaps & Ambiguities:** Explicitly identify mappings requiring data/type transformation, terminology translation, complex logic, or where information seems incomplete (potentially due to missing stored procedure logic).

**Phase 2: Iterative Refinement & Addressing Gaps (Requires User Input)**

1.  **Present Findings:** Share the proposed mapping and flagged areas for the current resource.
2.  **Ask Targeted Questions:** Query the user (based on flags) to leverage their knowledge of the legacy system's behavior and data meaning to fill gaps left by missing stored procedure code.
3.  **Incorporate Knowledge:** Refine the mapping rules and logic based on user input.
4.  **Document Logic:** Clearly document the derived logic and mapping decisions.

**Phase 3: Verification Support**

1.  **FHIR Validation:** Utilize standard FHIR validators to ensure structural correctness of generated resources against FHIR R4 and relevant profiles.
2.  **Data Comparison:** Compare source data (via API or legacy app) with mapped FHIR resources for semantic accuracy.
3.  **Clinical Review:** Facilitate review by clinicians/end-users to confirm clinical validity.

## Mappers Implemented / In Progress

*   **PatientMapper.cs:** Maps `DemographicsDomain` -> `Patient` (US Core)
*   **VitalSignMapper.cs:** Maps `VitalSignItem` -> `Observation` (US Core Vital Signs)
*   **ConditionMapper.cs:** (Placeholder) Maps `ProblemListItem` -> `Condition`
*   **AllergyIntoleranceMapper.cs:** (Placeholder) Maps `AllergyItem` -> `AllergyIntolerance`
*   **MedicationRequestMapper.cs:** (Placeholder) Maps `MedicationRequestItem` -> `MedicationRequest`
*   **MedicationStatementMapper.cs:** (Placeholder) Maps `MedicationStatementItem` -> `MedicationStatement`
*   **ImmunizationMapper.cs:** (Placeholder) Maps `ImmunizationItem` -> `Immunization`
*   **LabResultMapper.cs:** (Placeholder) Maps `LabResultItem` -> `Observation` (for individual results)
*   **DiagnosticReportMapper.cs:** (Placeholder) Maps `LabReportItem` -> `DiagnosticReport` (for lab report header/context)
*   **ProcedureMapper.cs:** Maps `ProcedureItem` -> `Procedure` (References `PractitionerMapper`)
*   **EncounterMapper.cs:** Maps `EncounterItem` -> `Encounter` (References `PractitionerMapper` & `OrganizationMapper`)
*   **DocumentReferenceMapper.cs:** (Placeholder) Maps `NoteItem` -> `DocumentReference`
*   **OrganizationMapper.cs:** Maps `PracticeInfo` -> `Organization` (Placeholder data retrieval)
*   **PractitionerMapper.cs:** Maps `ReferProviders` -> `Practitioner` (Placeholder data retrieval)

## Data Models (Source POCOs - Placeholders)

*   Models/DemographicsDomain.cs
*   Models/VitalSignItem.cs
*   Models/ProblemListItem.cs
*   Models/AllergyItem.cs
*   Models/MedicationRequestItem.cs
*   Models/MedicationStatementItem.cs
*   Models/ImmunizationItem.cs
*   Models/LabResultItem.cs
*   Models/LabReportItem.cs
*   Models/ProcedureItem.cs
*   Models/EncounterItem.cs
*   Models/NoteItem.cs
*   Models/PracticeInfo.cs
*   Models/ProviderInfo.cs

## Current Status (As of Checkpoint 5 / 2025-03-30)

*   Initial mappers created for core US Core resources (Patient, Vital Signs).
*   Placeholders established for most other required resources (Condition, Allergy, Meds, Labs, etc.).
*   `PatientMapper` enhanced with US Core extension placeholders and identifier/telecom standardization.
*   `VitalSignMapper` includes logic for common vital signs, LOINC codes, and placeholder unit conversions.
*   Flags (`<<< FLAG: ... >>>`) added throughout mappers to identify areas needing source data clarification, mapping decisions, or team discussion.
*   Placeholder POCOs (`PracticeInfo`, `ProviderInfo`) and Mappers (`OrganizationMapper`, `PractitionerMapper`) created based on likely repository sources (`PracticeInfoRepository`, `ReferProvidersRepository`).
*   `OrganizationMapper` and `PractitionerMapper` updated with placeholder data retrieval logic and temporary internal domain models for compilation.
*   `EncounterMapper` updated to create references using `OrganizationMapper` and `PractitionerMapper`.
*   `ProcedureMapper` updated to create references using `PractitionerMapper`.

## Next Steps

1.  **Resolve References (`PatientMapper`):** Update `PatientMapper.cs` to resolve the `generalPractitioner` reference using `PractitionerMapper` and/or `OrganizationMapper` based on source data.
2.  **Address Flags:** Systematically review and resolve the `<<< FLAG: ... >>>` and `TODO:` comments across all modified mappers (`Organization`, `Practitioner`, `Encounter`, `Procedure`, `Patient`).
3.  **Implement Real Data Retrieval:** Replace placeholder data access in `OrganizationMapper` and `PractitionerMapper` with actual repository calls (likely requiring Dependency Injection setup).
4.  **Refactor Domain Models:** Remove temporary internal domain model classes from mappers once actual `POC.Domain.DomainModels` are integrated/available.
5.  **Ensure US Core Compliance:** Confirm all mandatory elements and required extensions for US Core profiles are correctly implemented.
6.  **Testing:** Develop unit tests for the mappers.

## Starting Point

Begin with the FHIR `Patient` resource, analyzing the `/Demographics` endpoint(s) in `swagger.json` as the primary input source.

## Verification Support

1.  **FHIR Validation:** Utilize standard FHIR validators to ensure structural correctness of generated resources against FHIR R4 and relevant profiles.
2.  **Data Comparison:** Compare source data (via API or legacy app) with mapped FHIR resources for semantic accuracy.
3.  **Clinical Review:** Facilitate review by clinicians/end-users to confirm clinical validity.
