import React from 'react';
import { AppProvider, useAppContext } from './context/AppContext';
import Header from './components/Header';
import Sidebar from './components/Sidebar'; // Keep sidebar for patient context (if needed)
import Dashboard from './views/Dashboard';
import MappingStatusView from './views/MappingStatusView';
import ApiTestingTools from './views/ApiTestingTools';
import LoadingWithLog from './components/LoadingWithLog'; // Import the loader
import ApiExplorer from './views/ApiExplorer'; // Import the new view
import MappingDiscoveryView from './views/MappingDiscoveryView'; // Import the mapping discovery view

const MainContent = () => {
    const { view, loading, error, selectedPatient, activeModule } = useAppContext();

    // Centralized loading check
    // If loading is specifically for mapping status or swagger, handle it within those components
    // This global loading is currently tied to fetchPatients in the context
    if (loading) {
        return <LoadingWithLog message="Loading Legacy Data..." />; // Use the enhanced loader
    }
    
    // Handle global errors (e.g., from initial fetches if any)
    // Note: MappingStatusView and ApiTestingTools handle their own errors
    // if (error && view === 'dashboard') { // Only show global error on dashboard?
    //     return <div className="alert alert-danger m-4">Error: {error}</div>;
    // }

    const renderView = () => {
        switch (view) {
            case 'dashboard':
                return <Dashboard />;
            case 'mappingStatus':
                return <MappingStatusView />;
            case 'apiTesting':
                return <ApiTestingTools />;
            case 'apiExplorer': // Add case for ApiExplorer
                return <ApiExplorer />;
            case 'mappingDiscovery': // Add case for MappingDiscoveryView
                return <MappingDiscoveryView />;
            case 'patient-detail':
                if (!selectedPatient) {
                    return <div className="alert alert-warning m-4">No patient selected. Go back to API Testing.</div>;
                }
                // Render the correct patient module based on activeModule
                // Example: return <PatientSummary />; or <PatientDemographics />;
                // For now, just a placeholder:
                return <div className="p-4"><h2>Patient Detail for {selectedPatient.First} {selectedPatient.Last} ({activeModule})</h2><p>Module content goes here...</p></div>;
            default:
                return <Dashboard />;
        }
    };

    return (
        <div className="container-fluid">
            <div className="row">
                {/* Sidebar only shows when a patient is selected (view === 'patient-detail') */}
                {/* <Sidebar /> */}
                {/* Main content area */} 
                {/* Adjust column width if sidebar is present */}
                <div className={`col ${view === 'patient-detail' ? 'col-md-10' : 'col-12'} p-0`}> 
                   {renderView()}
                </div>
            </div>
        </div>
    );
}

const App = () => {
    return (
        <AppProvider>
            <div className="d-flex flex-column vh-100">
                <Header />
                <main className="flex-grow-1" style={{ overflowY: 'auto' }}>
                   <MainContent />
                </main>
            </div>
        </AppProvider>
    );
};

export default App;
