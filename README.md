# Legacy EHR to FHIR Mapping Project

This repository contains tools and code focused on **mapping data from a legacy EHR system (accessed via SQL or an existing API) to the FHIR R4 standard**.

## Primary Goal: FHIR Mapping

The core objective is to develop and refine C# mappers (located in `FHIRMappers/`) that transform data from the source system into valid FHIR resources (Patient, Observation, Condition, etc.).

## Supporting Frontend Application

This project also includes a web-based frontend (`index.html` served by `server.js`) designed to **support the mapping effort**. Its key functions are:

*   **Data Exploration:** Allows connecting to the legacy system's API (`apiserviceswin20250318.azurewebsites.net/api/`) to view raw patient data, aiding in understanding the source schema.
*   **Mapping Status Visualization:** Provides a dedicated view ("Mapping Status") to track the progress of the C# mappers, list identified resources, and highlight areas needing work (via `<<< FLAG: ... >>>` comments).
*   **API Testing:** Includes tools to directly test API endpoints.

**Note:** The frontend is a development tool, not a production EHR interface.

## Getting Started

### Prerequisites

*   Node.js (v14 or higher) & npm/yarn (for the frontend/server)
*   .NET SDK (compatible with the C# projects in `DataAccess`, `Models`, `FHIRMappers` - *check project files for specific version*)

### Installation (Frontend/Server)

1.  Clone the repository.
2.  Install Node.js dependencies:
    ```bash
    npm install
    ```

### Running the Frontend/Server

1.  **Start the Node.js Server:**
    ```bash
    cd /path/to/claude_ac_directsql # Navigate to the repo root
    npm run dev
    ```
    This starts the Express server (defaulting to port 3000) which serves the `index.html` frontend and provides supporting endpoints.

2.  **Access the Frontend Tool:**
    Open your web browser and navigate to `http://localhost:3000`.

## Using the Frontend Tool

### Mapping Status View

*   **Access:** Click "Mapping Status" in the sidebar.
*   **Purpose:** This is the primary view for tracking the FHIR mapping work.
*   **Features:**
    *   Lists C# mapper files found in `FHIRMappers/`.
    *   Shows the potential FHIR Resource target derived from the filename.
    *   Sorts mappers by the number of `<<< FLAG: ... >>>` comments (most flags first) to prioritize review.
    *   Displays all flags found within each mapper file.

### Data Exploration (via Dashboard & API Testing)

*   **Loading API Data:** The application *does not* load data automatically. Go to the **Dashboard** and click "Load Patient Data" to connect to the legacy API and fetch sample patient information.
*   **Viewing Source Data:** Once loaded, you can browse patient lists and details to understand the data that needs mapping.
*   **API Testing:** Use the "API Testing" section to interact with specific API endpoints directly.
*   **Activity Log:** The log in the "API Testing" section shows the API calls made during data loading or testing.

### Data Source Indicator

A badge in the header shows the connection status to the legacy API (Not Connected, Live API Data, etc.) after attempting a manual load.

## Key Components

*   **`FHIRMappers/`:** Contains the core C# FHIR mapping logic (the primary focus).
*   **`DataAccess/`, `Models/`:** Supporting C# projects for legacy data access patterns and models (potentially approximate).
*   **`index.html`:** Frontend application built with React/Bootstrap (via CDN).
*   **`server.js`:** Node.js/Express backend serving the frontend and providing helper endpoints (`/mapping-status`, API proxy, test endpoints).

## FHIR Mapping Layer Details

*   **Goal:** Transform data from the legacy system (represented by models in `Models/` and accessed via `DataAccess/`) into FHIR R4 resources.
*   **Status:** Mappers are under development. Refer to the "Mapping Status" view in the frontend and the comments (`<<< FLAG: ... >>>`) within the `.cs` files for specific areas needing attention.
*   **Detailed Docs:** Further details on the mapping approach and specific mapper status may be found in `mapping/README.md`.

## Technical Stack (Frontend/Server)

-   React (via CDN), Bootstrap 5
-   Node.js, Express.js
-   `axios`, `http-proxy-middleware`

## Features

- Dashboard with summary statistics
- Patient listing with search
- Detailed patient views:
  - Demographics
  - Problems list
  - Medications
  - Allergies
  - And more!

## Frontend Behavior

### Data Loading

*   **Manual Load:** Unlike previous versions, the application **does not** automatically load patient data from the API upon startup. 
*   **Trigger:** To load data, navigate to the **Dashboard** and click the "Load Patient Data" button.
*   **Process:** Clicking the button initiates the following sequence:
    1.  **Health Check:** Sends a request to `/health` to verify server reachability.
    2.  **List Patient IDs:** Calls `/list-all-patients` to get a list of potential patient IDs from various backend sources.
    3.  **Fetch Patient Details:** Calls `/test-patient-demographics/:id` for each unique ID (up to a limit) to retrieve detailed data.
*   **Status:** Loading progress and any errors encountered during this process are displayed on the Dashboard.

### Activity Log

*   **Location:** The detailed log of the API calls made during the manual data load (or via API Testing tools) can be found in the **API Testing** section.
*   **Content:** Shows each step (health check, ID list fetch, detail fetches), the outcome (success/failure), status codes, and data source information.
*   **Persistence:** The log reflects the *most recent* data load attempt or testing sequence.

### Data Source Indicator

A badge in the header indicates the current data source status:

*   **Not Connected:** Initial state before data loading is attempted.
*   **Live API Data:** Successfully connected to and retrieved data from the Azure API.
*   **Hybrid Data:** Connected, but using a mix of API and mock data (e.g., due to some endpoints failing).
*   **Mock Data:** Using mock data because the API connection failed.

## Debugging & Development Tools

### API Testing Interface

Click the **API Testing** link in the sidebar to access tools for interacting directly with the backend API (via the Node.js proxy) and viewing the activity log.

### FHIR Mapping Status

Click the **Mapping Status** link in the sidebar to view the progress of the FHIR mapping effort.

*   **Purpose:** Provides visibility into the C# FHIR mappers located in the `FHIRMappers` directory.
*   **Endpoint:** Fetches data from the `/mapping-status` endpoint on the Node.js server.
*   **Display:**
    *   Lists all identified mapper files (e.g., `PatientMapper.cs`).
    *   Attempts to identify the target FHIR Resource based on the filename (e.g., `Patient`).
    *   Groups mappers and displays them **sorted by the number of flags** (most flags first) to help prioritize work.
    *   Lists all flags (`<<< FLAG: ... >>>`) found within each mapper file, along with the line number and content.
*   **Flags:** These placeholders mark areas needing review, refinement, or further implementation in the mapping logic.

### Diagnostic Endpoints

The Node.js server provides several endpoints useful for direct testing (accessible via browser or tools like `curl`):

*   `/health`: Basic server health check.
*   `/test-api-connection`: Tests connection to the base `/Demographics` endpoint on the backend API.
*   `/test-endpoint/{endpoint}/{optional-id}`: Tests an arbitrary endpoint on the backend API (e.g., `/test-endpoint/Demographics/1001`).
*   `/test-patient-demographics/:id`: Tests the combined patient data retrieval logic for a specific ID.
*   `/list-all-patients`: Triggers the scan for all patient IDs.
*   `/mapping-status`: Returns the FHIR mapping status data as JSON.
*   *(Other legacy test endpoints might exist but refer to the API Testing UI for primary debugging)*

### Browser Console

Open your browser's developer tools console for detailed logs, especially React rendering information and potential frontend errors.

## Technical Details

- Frontend: React (via CDN), Bootstrap 5
- Backend: Node.js, Express.js
- API Proxy: `http-proxy-middleware` used in `server.js`

## API Integration & Data Handling

(Content summarizing primary API endpoints and 204 handling - largely unchanged, can be reviewed if needed)

## FHIR Mapping Layer

(Content summarizing the C# mapping project - largely unchanged, refer to `mapping/README.md` for details)