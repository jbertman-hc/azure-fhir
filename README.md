# Legacy EHR to FHIR Mapping Project (`azure-fhir`)

This repository contains the components for a project focused on mapping data from a legacy EHR system to the FHIR R4 standard.

It includes:

1.  **C# FHIR Mappers (`FHIRMappers/`):** The core C# logic for transforming legacy data models into FHIR resources.
2.  **Node.js Backend/Proxy Server (`server.js`):** Acts as an API gateway/proxy to the real legacy API. It also serves a simple HTML-based EHR interface ([index.html](./index.html)) and provides helper endpoints (e.g., `/mapping-status`) used by the React UI.
3.  **React Frontend UI (`fhir-mapper-ui/`):** A more comprehensive Vite-based React application that serves as a **development and visualization tool** for the C# mapping process. It interacts with the Node.js backend.

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

1.  **Start the Backend Server & Basic UI:**
    Open a terminal in the `azure-fhir` root directory:
    ```bash
    node server.js
    ```
    (Leave this running. Provides the backend proxy and serves the basic UI at http://localhost:3000).

2.  **Start the React Development UI:**
    Open *another* terminal in the `azure-fhir/fhir-mapper-ui` directory:
    ```bash
    npm run dev
    ```
    (Leave this running. Provides the React development UI, typically at http://localhost:5173).

3.  **Access the UIs:**
    *   **Basic UI:** Open your web browser to the backend server's URL (e.g., `http://localhost:3000`).
    *   **React Dev UI:** Open your web browser to the Vite dev server's URL (e.g., `http://localhost:5173`).

## Configuration Notes

*   **Backend (`server.js`):** Legacy API URL and proxy settings are configured here.
*   **Frontend (`fhir-mapper-ui/vite.config.js`):** Configures the proxy target (must match the backend server's address/port).
*   **Frontend (`fhir-mapper-ui/src/context/AppContext.jsx`):** Manages global state and API interaction logic for the UI.