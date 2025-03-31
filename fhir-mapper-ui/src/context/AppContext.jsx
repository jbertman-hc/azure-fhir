import React, { createContext, useState, useContext, useCallback, useEffect } from 'react';
import apiClient from '../api/client'; // Use the configured client

// Create Context
const AppContext = createContext();

// Provider Component
export const AppProvider = ({ children }) => {
    const [loading, setLoading] = useState(false); // Start not loading
    const [error, setError] = useState(null);
    const [view, setView] = useState('dashboard'); // Default view
    const [dataSource, setDataSource] = useState('Not Connected'); // Start not connected
    const [apiActivityLog, setApiActivityLog] = useState([]);
    const [swaggerDefinition, setSwaggerDefinition] = useState(null); // Add state for Swagger data

    // --- State likely to be removed/changed for mapper focus ---
    const [patients, setPatients] = useState([]); // Kept for now as Dashboard uses it
    const [selectedPatient, setSelectedPatient] = useState(null); // Kept for now as Header uses it
    const [activeModule, setActiveModule] = useState(null);
    // ----------------------------------------------------------

    // Helper function to add logs
    const logActivity = useCallback((logEntry) => {
        setApiActivityLog(prev => [logEntry, ...prev.slice(0, 199)]); // Keep last 200 entries
    }, []);

    // --- Patient Fetching (Likely to be removed/replaced) ---
    const fetchPatients = useCallback(async () => {
        setLoading(true);
        setError(null);
        setApiActivityLog([]); // Clear log on new fetch
        logActivity({ type: 'info', message: 'Fetching Legacy Patient Data...' });

        try {
            // Simplified for refactor - reusing existing comprehensive endpoint
            logActivity({ type: 'api_call', endpoint: '/list-all-patients', message: 'Scanning for available patient IDs...' });
            const listResponse = await apiClient.get('/list-all-patients', {
                timeout: 30000 // Increase timeout to 30 seconds (30000 ms)
            });
            const patientSamples = listResponse.data.patientSamples || [];
            logActivity({ type: 'api_response', endpoint: '/list-all-patients', message: `Found ${patientSamples.length} potential patients.` });

            const patientsData = [];
            // Fetch details only for those marked as having data, limit concurrency
            const patientsWithData = patientSamples.filter(p => p.hasData);
            const BATCH_SIZE = 5;
            for (let i = 0; i < patientsWithData.length; i += BATCH_SIZE) {
                const batch = patientsWithData.slice(i, i + BATCH_SIZE);
                const batchPromises = batch.map(async (p) => {
                    try {
                        logActivity({ type: 'api_call', endpoint: `/test-patient-demographics/${p.patientId}`, message: `Fetching details for patient ${p.patientId}...` });
                        const detailResponse = await apiClient.get(`/test-patient-demographics/${p.patientId}`);
                        logActivity({ type: 'api_response', endpoint: `/test-patient-demographics/${p.patientId}`, message: `Received details for patient ${p.patientId}.` });
                        // Assume the response IS the patient object
                        return { ...detailResponse.data, ID: p.patientId }; // Add ID back if not present
                    } catch (detailError) {
                        logActivity({ type: 'error', endpoint: `/test-patient-demographics/${p.patientId}`, message: `Failed to fetch details for patient ${p.patientId}: ${detailError.message}` });
                        return null; // Indicate failure for this patient
                    }
                });
                const results = await Promise.all(batchPromises);
                patientsData.push(...results.filter(p => p !== null)); // Add successful fetches
            }

            setPatients(patientsData);
            setDataSource(patientsData.length > 0 ? 'api' : 'Not Connected');
            logActivity({ type: 'info', message: `Successfully loaded data for ${patientsData.length} patients.` });

        } catch (err) {
            console.error('Error fetching patients:', err);
            setError(`Failed to load patients: ${err.message}`);
            logActivity({ type: 'error', message: `Critical error during patient fetch: ${err.message}` });
            setDataSource('Error');
        } finally {
            setLoading(false);
        }
    }, [logActivity]); // Dependency on logActivity

    // Placeholder for fetching mapping status - we'll need this for the new UI
    const fetchMappingStatus = useCallback(async () => {
        // Logic from MappingStatusView's useEffect will go here
        // Uses apiClient.get('/mapping-status');
        console.warn('fetchMappingStatus not implemented yet');
        // Needs state to store mapping details: e.g., const [mappingStatus, setMappingStatus] = useState({});
    }, []);

    // Fetch Swagger data
    const fetchSwaggerData = useCallback(async () => {
        logActivity({ type: 'info', message: 'Fetching Swagger definition...' });
        try {
            // Assumes swagger.json is copied to public folder by the user
            const response = await fetch('/swagger.json'); 
            if (!response.ok) {
                 // Log specific HTTP error
                 logActivity({ type: 'error', message: `Failed to fetch Swagger: HTTP status ${response.status}` });
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const swaggerData = await response.json();
            setSwaggerDefinition(swaggerData); // Update state
            logActivity({ type: 'info', message: 'Swagger definition loaded successfully.' });
            console.log('Fetched Swagger Data:', swaggerData); // Keep console log for debugging
        } catch (error) {
            console.error('Error fetching Swagger data:', error);
            logActivity({ type: 'error', message: `Failed to load Swagger definition: ${error.message}` });
            setError('Failed to load Swagger definition. Check console and ensure swagger.json is in the public folder.'); // Update global error
             setSwaggerDefinition(null); // Explicitly set to null on error
        }
    }, [setError, logActivity]); // Add dependencies

    // Fetch Swagger data when the provider mounts
    useEffect(() => {
        fetchSwaggerData();
        // We might also fetch mapping status here eventually
        // fetchMappingStatus(); 
    }, [fetchSwaggerData]); // Dependency array ensures it runs once

    const contextValue = {
        loading,
        setLoading, // May need direct access in some components
        error,
        setError, // May need direct access
        view,
        setView,
        dataSource,
        apiActivityLog,
        logActivity,
        fetchPatients, // Keep for now
        fetchMappingStatus, // Keep placeholder
        fetchSwaggerData, // Expose fetch function if needed elsewhere
        swaggerDefinition, // Add swagger data to context
        // --- Likely to be removed ---
        patients,
        selectedPatient,
        setSelectedPatient,
        activeModule,
        setActiveModule,
        // --- Other fetch functions removed (details, allergies etc.) ---
    };

    return (
        <AppContext.Provider value={contextValue}>
            {children}
        </AppContext.Provider>
    );
};

// Custom hook to use the context
export const useAppContext = () => useContext(AppContext);
