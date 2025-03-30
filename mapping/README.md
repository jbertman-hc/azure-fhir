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

## Starting Point

Begin with the FHIR `Patient` resource, analyzing the `/Demographics` endpoint(s) in `swagger.json` as the primary input source.
