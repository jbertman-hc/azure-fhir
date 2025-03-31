# Legacy EHR to FHIR Mapping Project (`azure-fhir`)

This repository contains components for a project focused on mapping data from existing Amazing Charts legacy EHR system to the FHIR R4 standard. The azure endpoint is at: https://apiserviceswin20250318.azurewebsites.net/api/

It includes:

1.  **C# FHIR Mappers (`FHIRMappers/`):** The core C# logic for transforming legacy data models into FHIR resources.
2.  **Mapping Discovery System (`FHIRMappers/Discovery/`):** Automated tools for discovering, configuring, and generating FHIR mappings from legacy data models.
3.  **Node.js Backend/Proxy Server (`server.js`):** Acts as an API gateway/proxy to the real legacy API. It also serves a simple HTML-based EHR interface ([index.html](./index.html)) and provides helper endpoints (e.g., `/mapping-status`) used by the React UI.
4.  **React Frontend UI (`fhir-mapper-ui/`):** A more comprehensive Vite-based React application that serves as a **development and visualization tool** for the C# mapping process. It interacts with the Node.js backend.

**IMPORTANT:** The Node.js server and both UIs are primarily **tools to aid development** of the C# mappers. They are **not** intended as a production EHR system or interface.

## Repository Structure

```
azure-fhir/
├── FHIRMappers/         # Core C# FHIR mapping logic
├── fhir-mapper-ui/      # React frontend application (Vite)
│   ├── public/
│   │   └── swagger.json # OpenAPI spec for legacy API
│   ├── src/             # Frontend source code (components, views, context)
│   ├── index.html
│   ├── package.json     # Frontend dependencies
│   └── vite.config.js   # Vite config (including proxy to backend)
├── DataAccess/          # Supporting C# Data Access project
├── Models/              # Supporting C# Models project
├── server.js            # Node.js backend server
├── package.json         # Backend dependencies
└── README.md            # This file
```

## Core Goal: FHIR Mapping (`FHIRMappers/`)

The primary objective is to develop and refine the C# classes within the `FHIRMappers/` directory. These mappers are responsible for converting data queried via `DataAccess/` (using `Models/`) into valid FHIR R4 resources.

*   **Technology:** C#
*   **Status:** Development is ongoing. Use the "Mapping Status" view in the frontend or examine `<<< FLAG: ... >>>` comments within the `.cs` files to identify areas needing work.
*   **Building:** Build these C# projects using the .NET SDK.

## Development Support Tools (Backend & Frontend UIs)

To facilitate the development of the C# mappers, this repository includes a Node.js server (acting as backend/proxy) and two distinct frontend UIs.

### 1. Node.js Backend (`server.js`)

*   **Purpose:**
    *   Acts as an API gateway/proxy to the real legacy API.
    *   Serves a basic EHR-like interface via the root [index.html](./index.html) file.
    *   Provides helper endpoints needed by the *React* frontend (e.g., `/mapping-status`).
*   **Key Endpoints:**
    *   `/api/*`: Proxies requests to the configured legacy API.
    *   `/mapping-status`: Reads `FHIRMappers/`, analyzes `.cs` files for flags, returns JSON summary.
    *   `/list-all-patients`: Retrieves patient IDs from the legacy backend.
    *   Various `/test-*` endpoints for diagnostics.
*   **Running:**
    ```bash
    # From the azure-fhir root directory
    npm install  # Install backend dependencies
    node server.js
    ```
    (Serves the `index.html` UI and backend API proxy, typically on port 3000)

### 2. React Frontend (`fhir-mapper-ui/`)

*   **Purpose:**
    *   Visualizes data from the legacy API to aid understanding.
    *   Displays the progress and status of C# mappers via the `/mapping-status` endpoint.
    *   Provides an interactive API explorer based on `swagger.json`.
    *   Offers tools to test legacy API endpoints directly (via the backend proxy).
*   **Key Features:** Dashboard, API Explorer, Mapping Status view, API Testing tools.
*   **Technology:** React, Vite, Bootstrap, Axios, Context API.
*   **Running (Development):**
    ```bash
    # From the azure-fhir root directory
    cd fhir-mapper-ui
    npm install  # Install frontend dependencies
    npm run dev
    ```
    (Runs the React development UI, typically on port 5173. It makes calls to the backend server running on port 3000)

### 3. Mapping Discovery Tool

*   **Purpose:**
    *   Automates the discovery of potential mappings between legacy data models and FHIR resources.
    *   Generates mapping configurations based on user-selected mappings.
    *   Previews FHIR resources using the generated configurations.
    *   Generates C# mapper classes that can be integrated into the application.
*   **Key Features:**
    *   **Analyze Legacy Data:** Upload or generate sample legacy data and analyze it for potential FHIR mappings.
    *   **Interactive Mapping Selection:** Review and select appropriate mappings from the discovered options.
    *   **Configuration Generation:** Automatically generate mapping configurations based on selected mappings.
    *   **FHIR Preview:** Preview the resulting FHIR resource using the generated configuration.
    *   **C# Mapper Generation:** Generate a C# mapper class that can be used in the application.
*   **Technology:** 
    *   **Frontend:** React components in the `fhir-mapper-ui/src/views/MappingDiscoveryView.jsx`
    *   **Backend:** Node.js controller in `controllers/mappingDiscoveryController.js`
    *   **Azure API:** Integration with Azure backend API for mapping analysis and generation
*   **Usage:**
    1. Navigate to the "Mapping Discovery" page in the React UI.
    2. Select a FHIR resource type from the dropdown.
    3. Either generate sample data or enter your own legacy data in JSON format.
    4. Click "Analyze" to discover potential mappings.
    5. Review and select the appropriate mappings for each FHIR field.
    6. Generate the mapping configuration and preview the resulting FHIR resource.
    7. Generate a C# mapper class that can be integrated into the application.

## Getting Started (Full Stack)

### Prerequisites

*   **Node.js:** **v15.0.0 or higher** (for backend), compatible version for Vite (check `fhir-mapper-ui/package.json`, likely v16+).
    *   _Note: As of this writing, **v20+** is required for `fhir-mapper-ui` dependencies._
*   **npm**.
*   **(Optional) .NET SDK:** Required only for building/modifying C# projects.

### Installation

1.  Clone this repository (`azure-fhir`).
2.  Install backend dependencies:
    ```bash
    cd /path/to/azure-fhir
    npm install
    ```
3.  Install frontend dependencies:
    ```bash
    cd fhir-mapper-ui
    npm install
    ```

### Running (Development)

#### Option 1: Using the Start Script (Recommended)

A convenience script `start.sh` is provided to launch both the backend and the React UI simultaneously.
This script also attempts to automatically set the required Node.js version (currently v20.17.0) using `nvm` if it's installed in the standard location (`$HOME/.nvm`).

1.  **Ensure the script is executable:**
    ```bash
    # Run this once from the azure-fhir root directory
    chmod +x start.sh
    ```
2.  **Run the script:**
    ```bash
    # From the azure-fhir root directory
    ./start.sh
    ```
    This will start both servers in the background.

#### Option 2: Manual Startup

1.  **Set Node.js Version:**
    Ensure you are using the correct Node.js version (v20.17.0 or as required by `fhir-mapper-ui/package.json`). Use `nvm` or your preferred version manager:
    ```bash
    nvm use 20.17.0
    ```
2.  **Start the Backend Server & Basic UI:**
    *(Requires Node.js v15+)*
    Open a terminal in the `azure-fhir` root directory:
    ```bash
    node server.js
    ```
    (Leave this running. Provides the backend proxy and serves the basic UI at http://localhost:3000).

3.  **Start the React Development UI:**
    *(Skip if using `start.sh`)*
    *(Requires Node.js v20+)*
    Open *another* terminal in the `azure-fhir/fhir-mapper-ui` directory:
    ```bash
    npm run dev
    ```
    (Leave this running. Provides the React development UI, typically at http://localhost:5173).

4.  **Access the UIs:**
    *(URLs apply to both manual and script startup)*
    *   **Basic UI:** Open your web browser to the backend server's URL (e.g., `http://localhost:3000`).
    *   **React Dev UI:** Open your web browser to the Vite dev server's URL (e.g., `http://localhost:5173`).

## Configuration Notes

*   **Backend (`server.js`):** Legacy API URL and proxy settings are configured here. The server acts as a proxy to the Azure API at `https://apiserviceswin20250318.azurewebsites.net/api`.
*   **Frontend (`fhir-mapper-ui/vite.config.js`):** Configures the proxy target (must match the backend server's address/port).
*   **Frontend (`fhir-mapper-ui/src/context/AppContext.jsx`):** Manages global state and API interaction logic for the UI.
*   **Mapping Discovery Controller (`controllers/mappingDiscoveryController.js`):** Handles requests for the Mapping Discovery tool and forwards them to the Azure API.