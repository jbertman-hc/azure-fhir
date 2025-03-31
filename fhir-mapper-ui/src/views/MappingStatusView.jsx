import React, { useState, useEffect } from 'react';
import apiClient from '../api/client'; // Use the configured client

const MappingStatusView = () => {
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [mappingDetails, setMappingDetails] = useState({}); // Store the full details object

    useEffect(() => {
        console.log('MappingStatusView mounted, fetching status...')
        setLoading(true);
        setError(null);
        apiClient.get('/mapping-status')
            .then(response => {
                console.log('Mapping status fetched successfully:', response.data);
                // Sort mappers by number of flags (descending)
                const sortedDetails = Object.entries(response.data.mappingDetails || {})
                    .sort(([, a], [, b]) => (b.flags?.length || 0) - (a.flags?.length || 0))
                    .reduce((obj, [key, value]) => {
                        obj[key] = value;
                        return obj;
                    }, {});
                setMappingDetails(sortedDetails);
                if (response.status === 207) {
                    console.warn('Mapping status endpoint reported partial errors.');
                    // Optionally set a non-critical error/warning message
                }
            })
            .catch(err => {
                console.error('Raw error object fetching mapping status:', err);
                let errorMsg = `Failed to load mapping status: ${err.message}. Check console for details.`;
                if (err.response) {
                    // Error came from server response (4xx, 5xx)
                    console.error(`Mapping status fetch failed - Status: ${err.response.status}, Data:`, err.response.data);
                    errorMsg = `Failed to load mapping status. Server responded with ${err.response.status} (${err.response.data?.message || 'No details'}).`;
                } else if (err.request) {
                    // Request was made but no response received
                     console.error('Mapping status fetch failed - No response received:', err.request);
                     errorMsg = 'Failed to load mapping status. No response from server. Is it running?';
                } else {
                    // Setup error or other issue
                    console.error('Mapping status fetch failed - Request setup error:', err.message);
                }
                setError(errorMsg);
            })
            .finally(() => {
                setLoading(false);
                console.log('Finished fetching mapping status.');
            });
    }, []); // Empty dependency array ensures this runs only once on mount

    if (loading) {
        return <div className="text-center p-5"><div className="spinner-border" role="status"><span className="visually-hidden">Loading...</span></div><p className="mt-2">Loading Mapping Status...</p></div>;
    }

    if (error) {
        return <div className="alert alert-danger m-4">Error loading mapping status: {error}</div>;
    }

    const mapperFiles = Object.keys(mappingDetails);

    if (mapperFiles.length === 0) {
         return <div className="alert alert-warning m-4">No C# mapper files found or processed in the 'FHIRMappers' directory.</div>;
    }

    return (
        <div className="container-fluid mt-3 p-4">
            <h2>FHIR Mapping Status</h2>
            <p className="text-muted">Tracking progress of C# mappers found in the <code>FHIRMappers/</code> directory. Sorted by flag count (most flags first).</p>
            <hr />

            {mapperFiles.map(fileName => {
                const details = mappingDetails[fileName];
                const hasFlags = details.flags && details.flags.length > 0;
                const hasError = !!details.error;
                const cardBorder = hasError ? 'border-danger' : (hasFlags ? 'border-warning' : 'border-success');
                const cardHeaderBg = hasError ? 'bg-danger text-white' : (hasFlags ? 'bg-warning text-dark' : 'bg-light');

                return (
                    <div key={fileName} className={`card mb-3 ${cardBorder}`}>
                        <div className={`card-header ${cardHeaderBg} d-flex justify-content-between align-items-center`}>
                            <h5 className="mb-0">
                                <i className={`bi ${hasError ? 'bi-x-octagon-fill' : (hasFlags ? 'bi-flag-fill' : 'bi-check-circle-fill')} me-2`}></i>
                                {details.mapperName || fileName} 
                                {details.potentialResource && <small className="text-muted ms-2">({details.potentialResource} Resource)</small>}
                            </h5>
                             <span className={`badge rounded-pill ${hasError ? 'bg-light text-danger' : (hasFlags ? 'bg-danger' : 'bg-success')}`}>
                                 {hasError ? 'Error' : (hasFlags ? `${details.flags.length} Flag(s)` : 'OK')}
                             </span>
                        </div>
                        <div className="card-body">
                            {hasError && (
                                <div className="alert alert-danger small p-2">Error processing file: {details.error}</div>
                            )}
                            {!hasError && !hasFlags && (
                                 <p className="text-success mb-0"><i className="bi bi-check-lg"></i> No flags found in this mapper.</p>
                            )}
                            {hasFlags && (
                                <div className="table-responsive">
                                    <table className="table table-sm table-hover small">
                                        <thead>
                                            <tr>
                                                <th style={{width: '80px'}}>Line</th>
                                                <th>Flag Content & Context</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {details.flags.map((flag, flagIndex) => (
                                                <tr key={flagIndex}>
                                                    <td>{flag.line > 0 ? flag.line : 'N/A'}</td>
                                                    <td>
                                                        <strong className="text-danger">{flag.flagContent}</strong>
                                                        {flag.line > 0 && <code className="d-block text-muted mt-1 small">{flag.fullLine}</code>}
                                                    </td>
                                                </tr>
                                            ))}
                                        </tbody>
                                    </table>
                                </div>
                            )}
                        </div>
                    </div>
                );
            })}
        </div>
    );
};

export default MappingStatusView;
