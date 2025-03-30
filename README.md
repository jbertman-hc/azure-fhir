# EHR System Frontend

This is a frontend application for an Electronic Health Record (EHR) system that interacts with an API at `apiserviceswin20250318.azurewebsites.net/api/`.

## Features

- Dashboard with summary statistics
- Patient listing with search
- Detailed patient views:
  - Demographics
  - Problems list
  - Medications
  - Allergies
  - And more!

## Getting Started

### Prerequisites

- Node.js (v14 or higher)
- npm or yarn

### Installation

1. Clone the repository
2. Install dependencies:

```bash
npm install
```

### Running the Application

1.  **Start the Node.js Server:**
    ```bash
    cd /path/to/claude_ac_directsql
    npm install
    npm run dev
    ```
    This starts the Express server (defaulting to port 3000) which acts as a proxy to the Azure API and serves the `index.html` frontend.

2.  **Access the Frontend:**
    Open your web browser and navigate to `http://localhost:3000` (or the port specified in the server startup logs).

## Frontend Loading Sequence & Activity Log

The frontend (`index.html`) implements a specific sequence when loading patient data:

1.  **Health Check:** Upon initial load or refresh, the frontend first sends a request to the server's `/health` endpoint. This acts as a quick ping to ensure the server is running and reachable before proceeding.
2.  **List Patient IDs:** If the health check is successful, it calls the server's `/list-all-patients` endpoint. This endpoint scans various potential backend data sources (e.g., `PatientIndex`, `PatientDemographics`, `PatientDemographicsAddendum` on the Azure API) to compile a list of all *potential* patient IDs.
3.  **Fetch Patient Details:** For each unique patient ID returned (up to a limit, currently 13), the frontend calls the server's `/test-patient-demographics/:id` endpoint. This endpoint gathers detailed demographic and potentially other data for that specific patient ID from multiple underlying Azure API endpoints, combining the results.

**Activity Log:**

*   **During Loading:** While the above sequence is in progress, an activity log is displayed showing each step (health check, ID list fetch, detail fetches) and the outcome (success/failure, status codes, data found). Log messages appear with the newest entry at the top.
*   **Persistent Log:** The complete activity log from the most recent data fetch sequence is preserved. It can be viewed after loading is complete by navigating to the "API Testing" section in the sidebar. This allows reviewing the steps taken even after the main UI is rendered. A "Clear Log" button is available in the API Testing view.

## Key Components

*   **`index.html`:** Contains the entire frontend application logic (React components, state management via `useContext`, API call functions, UI rendering).
*   **`server.js`:** The Node.js Express backend. Handles:
    *   Serving `index.html`.

## Debugging API Connectivity

The application includes an integrated API Testing interface and several diagnostic tools:

1. **API Testing Interface**:  
   Click the "API Testing" link in the navigation bar to access the testing interface with the following features:
   - Test available endpoints
   - Find all patients
   - Test SQL repositories
   - Custom endpoint testing with optional patient ID
   - Patient demographics testing for specific patient IDs
   - JSON-formatted results display

2. **Test API Connection**:  
   Visit `http://localhost:3000/test-api-connection` to test the base Demographics endpoint.

3. **Test Specific Endpoint**:  
   Visit `http://localhost:3000/test-endpoint/Demographics/1001` to test a specific endpoint and ID (e.g., patient William Saffire).
   - Format: `/test-endpoint/{endpoint}/{optional-id}`
   - Examples:
     - `/test-endpoint/Demographics/1001` - Get William Saffire's demographics
     - `/test-endpoint/Addendum/1` - Get specific Addendum
     - `/test-endpoint/PatientIndex` - Get all patient indices

4. **Patient Demographics Test**:  
   Visit `http://localhost:3000/test-patient-demographics/1001` to test patient demographics with improved 204 handling.

5. **List All Patients**:  
   Visit `http://localhost:3000/list-all-patients` to scan for all available patients across multiple endpoints.

6. **SQL Repositories Test**:  
   Visit `http://localhost:3000/sql-repositories` to test direct SQL repository access.

7. **Test All Available Endpoints**:  
   Visit `http://localhost:3000/test-available-endpoints` to run tests against multiple endpoints and see which ones are available.

8. **Legacy Addendum Test**:  
   Visit `http://localhost:3000/test-addendum/1` to test a specific Addendum record (maintained for backward compatibility).

9. **Browser Console**:  
   Open your browser's developer tools console to see a detailed table of API endpoint test results and connection logs.

The application also displays a badge in the top navigation bar indicating the current data source:

- **Live API Data** - Successfully connected to the Azure backend API with full data
- **Hybrid Data** - Connected to the API but using a mix of API data and mock data
- **Mock Data** - Using mock data because the API connection failed

If you're experiencing connectivity issues:
- Check that the Azure API is online and accessible
- Verify that your network can reach the API endpoint
- Ensure no firewall or security policies are blocking the connection
- Check browser console for detailed error messages

## Technical Details

- The frontend is built with React (loaded via CDN for simplicity)
- Bootstrap 5 for styling
- Express.js server that provides:
  - Static file serving
  - CORS proxy to the backend API

The CORS proxy is necessary to avoid cross-origin issues when making requests from the browser directly to the API.

## API Integration

The application integrates with the following API endpoints:

- `/Demographics` - Patient demographic information
- `/Demographics/{id}` - Specific patient demographic information
- `/PatientIndex/{id}` - Alternative patient information source
- `/Addendum/{id}` - Patient notes and addenda
- `/ListAllergies/Patient/{id}` - Patient allergies
- `/ListProblem/Patient/{id}` - Patient problems/diagnoses
- `/ListMEDS/Patient/{id}` - Patient medications

The application also includes experimental support for direct SQL repository access:
- `/DemographicsRepository` - Direct access to demographics repository
- `/PatientIndexRepository` - Direct access to patient index repository

When endpoints return 204 No Content or are unavailable, the application automatically tries alternative data sources and falls back to demo data to showcase functionality.

## Handling 204 No Content Responses

The application is designed to handle 204 No Content responses from the API by:

1. Trying multiple alternative endpoints to find patient data
2. Using a tiered data source approach:
   - First try Demographics endpoint
   - Then try PatientIndex
   - Then try Addendum
   - Finally fall back to mock data

This ensures the application can display useful information even when primary data sources are unavailable.

## FHIR Mapping Layer

In addition to the frontend application, this repository now includes a C# project focused on creating a FHIR R4 mapping layer. The goal is to transform data from the legacy system's underlying database (accessed via SQL repositories) into standardized FHIR resources.

### Progress

*   **Mapper Development:** C# classes (`FHIRMappers/*.cs`) have been created to map legacy data models (approximated in `Models/*.cs`) to various FHIR resources (Patient, Observation, Condition, Encounter, Procedure, Organization, Practitioner, etc.).
*   **Organization & Practitioner Mapping:** Focused effort on mapping `PracticeInfo` and `ReferProviders` data (from `DataAccess/SQLRepos/`) to FHIR `Organization` and `Practitioner` resources, respectively. Placeholder data retrieval logic is currently implemented.
*   **Reference Integration:** The `EncounterMapper` and `ProcedureMapper` have been updated to utilize the `OrganizationMapper` and `PractitionerMapper` for resolving references like `serviceProvider`, `participant`, and `performer`.
*   **Flags for Refinement:** Placeholders (`<<< FLAG: ... >>>`) are used extensively within the mappers to mark areas requiring further investigation, confirmation of mapping logic, or implementation of specific transformations.
*   **Repository Change:** The primary Git remote for this repository has been updated to `https://github.com/jbertman-hc/azure-fhir.git`.

### Documentation & Next Steps

Detailed documentation regarding the mapping approach, specific mapper status, and a full list of next steps can be found in the [mapping/README.md](./mapping/README.md) file.

The current immediate focus is on integrating the `OrganizationMapper` and `PractitionerMapper` into the `PatientMapper` to handle the `generalPractitioner` reference.