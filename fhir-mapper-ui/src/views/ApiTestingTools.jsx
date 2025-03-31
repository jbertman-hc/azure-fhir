import React, { useState, useCallback } from 'react';
import apiClient from '../api/client'; // Use the configured client
import { useAppContext } from '../context/AppContext'; // Access logActivity

const ApiTestingTools = () => {
    const [endpoint, setEndpoint] = useState('/list-all-patients');
    const [patientId, setPatientId] = useState('');
    const [loading, setLoading] = useState(false);
    const [response, setResponse] = useState(null);
    const [error, setError] = useState(null);
    const { logActivity } = useAppContext(); // Get log function from context

    const handleSubmit = useCallback(async (e) => {
        e.preventDefault();
        setLoading(true);
        setResponse(null);
        setError(null);

        let url = endpoint;
        if (endpoint.includes(':patientId')) {
            if (!patientId) {
                setError('Patient ID is required for this endpoint.');
                setLoading(false);
                return;
            }
            url = endpoint.replace(':patientId', patientId);
        }

        logActivity({ type: 'api_call', endpoint: url, message: `Testing endpoint: ${url}` });
        try {
            const startTime = performance.now();
            const result = await apiClient.get(url, {
                timeout: 30000 // Add 30 second timeout
            }); 
            const endTime = performance.now();
            const duration = (endTime - startTime).toFixed(2);

            setResponse({
                status: result.status,
                statusText: result.statusText,
                headers: result.headers,
                data: result.data,
                duration: duration
            });
            logActivity({ type: 'api_response', endpoint: url, message: `Success (${result.status}) - ${duration} ms` });
        } catch (err) {
            const endTime = performance.now();
            const duration = (endTime - performance.now()).toFixed(2); // Won't capture start time in catch
            setError({
                message: err.message,
                status: err.response?.status,
                statusText: err.response?.statusText,
                data: err.response?.data,
                duration: 'N/A' // Duration less useful on error
            });
            logActivity({ type: 'error', endpoint: url, message: `Error: ${err.message} (${err.response?.status || 'N/A'})` });
            console.error('API Test Error:', err);
        } finally {
            setLoading(false);
        }
    }, [endpoint, patientId, logActivity]);

    const endpoints = [
        { value: '/list-all-patients', label: 'List All Patient IDs' },
        { value: '/test-patient-demographics/:patientId', label: 'Patient Demographics by ID' },
        { value: '/test-patient-addendum/:patientId', label: 'Patient Addendum by ID' },
        { value: '/test-patient-problems/:patientId', label: 'Patient Problems by ID' },
        { value: '/test-patient-medications/:patientId', label: 'Patient Medications by ID' },
        { value: '/test-patient-allergies/:patientId', label: 'Patient Allergies by ID' },
        { value: '/mapping-status', label: 'Get Mapping Status' },
        // Add other testable legacy endpoints
    ];

    const requiresPatientId = endpoint.includes(':patientId');

    return (
        <div className="container-fluid p-4">
            <h2>Legacy API Testing Tools</h2>
            <p className="text-muted">Directly interact with legacy API endpoints. Responses are shown below.</p>
            <hr/>

            <form onSubmit={handleSubmit}>
                <div className="row mb-3">
                    <div className="col-md-5">
                        <label htmlFor="endpointSelect" className="form-label">Select Endpoint:</label>
                        <select 
                            id="endpointSelect" 
                            className="form-select" 
                            value={endpoint}
                            onChange={e => setEndpoint(e.target.value)}
                        >
                            {endpoints.map(ep => (
                                <option key={ep.value} value={ep.value}>{ep.label}</option>
                            ))}
                        </select>
                    </div>
                    {requiresPatientId && (
                        <div className="col-md-4">
                            <label htmlFor="patientIdInput" className="form-label">Patient ID:</label>
                            <input 
                                type="text" 
                                className="form-control" 
                                id="patientIdInput"
                                value={patientId}
                                onChange={e => setPatientId(e.target.value)}
                                placeholder="Enter Patient ID"
                                required
                            />
                        </div>
                    )}
                    <div className="col-md-3 d-flex align-items-end">
                        <button type="submit" className="btn btn-primary w-100" disabled={loading}>
                            {loading ? (
                                <>
                                    <span className="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                    <span className="visually-hidden">Loading...</span> Testing...
                                </> 
                            ) : (
                                <><i className="bi bi-send me-1"></i> Send Request</>
                            )}
                        </button>
                    </div>
                </div>
            </form>

            {loading && (
                <div className="text-center p-5">
                    <div className="spinner-border text-primary" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                    <p className="mt-2">Sending request...</p>
                </div>
            )}

            {error && (
                <div className="alert alert-danger mt-4">
                    <h4><i className="bi bi-exclamation-triangle-fill me-2"></i> Error</h4>
                    <p><strong>Status:</strong> {error.status || 'N/A'} {error.statusText || 'N/A'}</p>
                    <p><strong>Message:</strong> {error.message}</p>
                    {error.data && (
                        <>
                            <hr />
                            <p><strong>Response Body:</strong></p>
                            <pre className="bg-light p-2 rounded small" style={{ maxHeight: '300px', overflowY: 'auto' }}>
                                {JSON.stringify(error.data, null, 2)}
                            </pre>
                        </>
                    )}
                </div>
            )}

            {response && (
                 <div className="card mt-4">
                     <div className="card-header d-flex justify-content-between align-items-center">
                         <h5 className="mb-0">Response</h5>
                         <div>
                             <span className={`badge me-2 ${response.status >= 200 && response.status < 300 ? 'bg-success' : 'bg-warning'}`}>
                                 {response.status} {response.statusText}
                             </span>
                             <span className="badge bg-info">
                                 <i className="bi bi-clock me-1"></i> {response.duration} ms
                             </span>
                         </div>
                     </div>
                     <div className="card-body">
                         {/* Consider only showing headers if needed */}
                         {/* <p><strong>Headers:</strong></p>
                         <pre className="bg-light p-2 rounded small" style={{ maxHeight: '150px', overflowY: 'auto' }}>
                             {JSON.stringify(response.headers, null, 2)}
                         </pre>
                         <hr /> */}
                         <p><strong>Body:</strong></p>
                         <pre className="bg-light p-2 rounded small" style={{ maxHeight: '500px', overflowY: 'auto' }}>
                             {JSON.stringify(response.data, null, 2)}
                         </pre>
                     </div>
                 </div>
            )}

        </div>
    );
};

export default ApiTestingTools;
