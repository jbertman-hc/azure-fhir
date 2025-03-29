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

The application includes several tools to diagnose API connectivity issues:

1. **Test API Connection**:  
   Visit `http://localhost:3000/test-api-connection` to test the base Demographics endpoint.

2. **Test Specific Endpoint**:  
   Visit `http://localhost:3000/test-endpoint/Demographics/1001` to test a specific endpoint and ID (e.g., patient William Saffire).
   - Format: `/test-endpoint/{endpoint}/{optional-id}`
   - Examples:
     - `/test-endpoint/Demographics/1001` - Get William Saffire's demographics
     - `/test-endpoint/Addendum/1` - Get specific Addendum
     - `/test-endpoint/PatientIndex` - Get all patient indices

3. **Legacy Addendum Test**:  
   Visit `http://localhost:3000/test-addendum/1` to test a specific Addendum record (maintained for backward compatibility).

4. **Test All Available Endpoints**:  
   Visit `http://localhost:3000/test-available-endpoints` to run tests against multiple endpoints and see which ones are available.

4. **Browser Console**:  
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
- `/ListAllergies/Patient/{id}` - Patient allergies
- `/ListProblem/Patient/{id}` - Patient problems/diagnoses
- `/ListMEDS/Patient/{id}` - Patient medications

When the actual API endpoints are unavailable, the application falls back to demo data to showcase functionality.