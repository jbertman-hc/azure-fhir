import React from 'react';
import { useAppContext } from '../context/AppContext';
import LoadingWithLog from '../components/LoadingWithLog';

const Dashboard = () => {
    // Get necessary state and functions from context
    const { 
        fetchPatients, // Function to trigger patient list fetch
        loading, 
        error, 
        patients, // Keep track of loaded patients (from legacy fetch)
        swaggerDefinition, // Get swagger data
        logActivity, // Correct function name from context
        setView // Add setView to change the current view
    } = useAppContext();

    const handleFetchClick = () => {
        logActivity('Dashboard button clicked, initiating fetchPatients...');
        fetchPatients();
    };

    // Calculate API summary from swagger definition
    const getApiSummary = () => {
        if (!swaggerDefinition || !swaggerDefinition.info) {
            return { title: 'N/A', version: 'N/A', pathCount: 0 };
        }
        const pathCount = swaggerDefinition.paths ? Object.keys(swaggerDefinition.paths).length : 0;
        return {
            title: swaggerDefinition.info.title || 'Untitled API',
            version: swaggerDefinition.info.version || 'N/A',
            pathCount: pathCount
        };
    };

    const apiSummary = getApiSummary();

    return (
        <div className="container-fluid p-4">
            <h2>FHIR Mapper Dashboard</h2>
            <p className="text-muted">Overview of the API and mapping progress.</p>
            <hr />

            {/* API Overview Section */}
            <div className="card mb-4">
                <div className="card-header">API Overview</div>
                <div className="card-body">
                    {/* Show loading/error specifically for Swagger definition */}
                    {!swaggerDefinition && !error && <div className="text-center text-muted">Loading API definition...</div>}
                    {error && !swaggerDefinition && <div className="alert alert-warning">Failed to load API definition: {error}</div>}
                    {swaggerDefinition && (
                        <dl className="row mb-0">
                            <dt className="col-sm-3">API Title:</dt>
                            <dd className="col-sm-9">{apiSummary.title}</dd>

                            <dt className="col-sm-3">Version:</dt>
                            <dd className="col-sm-9"><span className="badge bg-secondary">{apiSummary.version}</span></dd>

                            <dt className="col-sm-3">Total Paths:</dt>
                            <dd className="col-sm-9">{apiSummary.pathCount}</dd>
                        </dl>
                    )}
                </div>
            </div>

            {/* Mapping Progress Section - Placeholder */}
            <div className="card mb-4">
                <div className="card-header">Mapping Progress Summary</div>
                <div className="card-body">
                    <p className="text-muted">Mapping status details will be shown here.</p>
                    {/* Add summary components later, e.g., % mapped, unmapped resources */} 
                    <button 
                        className="btn btn-sm btn-outline-primary"
                        onClick={() => {
                            logActivity('Navigate to Mapping Status view triggered.');
                            setView('mappingStatus'); // Actually change the view
                        }} 
                    >
                        View Full Mapping Status
                    </button>
                </div>
            </div>

            {/* Legacy Patient Loading - Keep for now if needed */}
            <div className="card">
                <div className="card-header">Legacy Patient Data</div>
                <div className="card-body">
                    <p>Load patient list from the legacy system for reference or testing.</p>
                    <button 
                        className="btn btn-primary" 
                        onClick={handleFetchClick} 
                        disabled={loading}
                    >
                        {loading ? (
                            <>
                                <span className="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
                                Loading...
                            </>
                        ) : (
                            'Load All Legacy Patients'
                        )}
                    </button>
                    {/* Display error related to patient fetching */}
                    {error && swaggerDefinition && <div className="alert alert-danger mt-3">Error loading patients: {error}</div>}
                    {/* Display count of loaded patients */}
                    {patients.length > 0 && (
                        <div className="alert alert-success mt-3">
                            Successfully loaded {patients.length} patient records.
                        </div>
                    )}
                </div>
            </div>

        </div>
    );
};

export default Dashboard;
