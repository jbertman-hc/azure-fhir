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

Start the development server:

```bash
npm start
```

Or, if you want to use nodemon for auto-reloading during development:

```bash
npm run dev
```

The application will be available at http://localhost:3000.

### Debugging API Connectivity

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

- 🟢 **Live API Data** - Successfully connected to the Azure backend API with full data
- 🔵 **Hybrid Data** - Connected to the API but using a mix of API data and mock data
- 🟠 **Mock Data** - Using mock data because the API connection failed

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