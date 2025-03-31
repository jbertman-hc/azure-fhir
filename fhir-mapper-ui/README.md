# FHIR Mapper UI

This repository contains a React-based frontend application built with Vite. Its primary purpose is to serve as a **development and visualization tool** for a FHIR mapping project.

## Purpose

This UI connects to a separate backend server (typically running from the `claude_ac_directsql` repository) to:

1.  **Explore Legacy Data:** Fetch and display data from a legacy API (proxied through the backend) to aid developers in understanding the source data that needs mapping to FHIR.
2.  **Visualize Mapping Progress:** Display the status of C# FHIR mapper files (located in the backend repository's `FHIRMappers/` directory), highlighting areas marked with `<<< FLAG: ... >>>` comments that require attention.
3.  **Test API Endpoints:** Provide simple tools to directly interact with the legacy API endpoints (via the backend proxy).
4.  **Display API Specification:** Load and render an OpenAPI (Swagger) specification (`swagger.json`) for the legacy API, providing an interactive exploration interface.

**Note:** This UI is a **developer tool** and not intended as a production EHR frontend.

## Key Features

*   **Dashboard:** Overview of API details (from Swagger) and mapping progress summary.
*   **API Explorer:** Interactive view of the legacy API based on its Swagger definition.
*   **Mapping Status:** Detailed view of C# mapper files, target resources, and flags indicating areas for review.
*   **API Testing Tools:** Simple interface to send requests to specific legacy API endpoints.
*   **(Legacy) Patient Data Browser:** Allows loading and viewing lists/details of patient data fetched from the legacy API (primarily for understanding source data).

## Technical Stack

*   **Framework/Library:** React
*   **Build Tool:** Vite
*   **Styling:** Bootstrap 5 (check `index.html` for integration method - likely CDN or imported)
*   **API Client:** Axios
*   **State Management:** React Context API (`AppContext.jsx`)

## Getting Started

### Prerequisites

*   **Node.js:** Version compatible with Vite and its dependencies (check `package.json` or Vite documentation, likely v16+).
*   **npm** or **yarn**.
*   **Running Backend Server:** The `claude_ac_directsql` backend server must be running, as this UI relies on it for data and API proxying.

### Installation

1.  Clone this repository.
2.  Navigate to the repository root directory:
    ```bash
    cd fhir-mapper-ui
    ```
3.  Install dependencies:
    ```bash
    npm install
    # or
    # yarn install
    ```

### Running the Development Server

1.  Ensure the backend server (`claude_ac_directsql`) is running.
2.  Start the Vite development server:
    ```bash
    npm run dev
    # or
    # yarn dev
    ```
3.  Open your browser and navigate to the URL provided by Vite (usually `http://localhost:5173`).

## Configuration

*   **API Proxy:** The connection to the backend server is configured in `vite.config.js`. It proxies requests starting with `/api` to the backend server (defaulting to `http://localhost:3000`).
*   **Context:** Global state and API interaction logic are managed in `src/context/AppContext.jsx`.
*   **API Client:** Base Axios configuration is in `src/api/client.js`.

## Project Structure

*   **`public/`:** Static assets (like `swagger.json`).
*   **`src/`:** Main application source code.
    *   **`api/`:** API client configuration.
    *   **`assets/`:** Static assets like images, CSS.
    *   **`components/`:** Reusable UI components (Header, Sidebar, etc.).
    *   **`context/`:** React Context for global state management.
    *   **`views/`:** Page-level components corresponding to different sections (Dashboard, ApiExplorer, etc.).
    *   **`App.jsx`:** Main application component, routing logic.
    *   **`main.jsx`:** Application entry point.
*   **`index.html`:** Main HTML file.
*   **`vite.config.js`:** Vite configuration (including proxy settings).
*   **`package.json`:** Project dependencies and scripts.
