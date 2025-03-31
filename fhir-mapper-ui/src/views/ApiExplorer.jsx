import React, { useState } from 'react';
import { useAppContext } from '../context/AppContext';

// Helper component for displaying details of a single path
const PathDetails = ({ path, details }) => {
    const [isOpen, setIsOpen] = useState(false);
    const methods = Object.keys(details);

    return (
        <div className="accordion-item">
            <h2 className="accordion-header" id={`heading-${path.replace(/[^a-zA-Z0-9]/g, '')}`}>
                <button 
                    className={`accordion-button ${isOpen ? '' : 'collapsed'}`} 
                    type="button" 
                    onClick={() => setIsOpen(!isOpen)}
                    aria-expanded={isOpen}
                    aria-controls={`collapse-${path.replace(/[^a-zA-Z0-9]/g, '')}`}
                >
                    <code>{path}</code>
                    <span className="ms-auto">{methods.map(m => <span key={m} className={`badge bg-${getMethodColor(m)} me-1`}>{m.toUpperCase()}</span>)}</span>
                </button>
            </h2>
            <div 
                id={`collapse-${path.replace(/[^a-zA-Z0-9]/g, '')}`}
                className={`accordion-collapse collapse ${isOpen ? 'show' : ''}`}
                aria-labelledby={`heading-${path.replace(/[^a-zA-Z0-9]/g, '')}`}
            >
                <div className="accordion-body small">
                    {methods.map(method => (
                        <div key={method} className="mb-3 pb-2 border-bottom">
                            <h5 className="fw-bold"><span className={`badge bg-${getMethodColor(method)} me-2`}>{method.toUpperCase()}</span>{details[method].summary || 'No summary'}</h5>
                            {details[method].description && <p>{details[method].description}</p>}
                            {details[method].tags && <p>Tags: {details[method].tags.join(', ')}</p>}
                             {/* Basic Parameter Info */}
                            {details[method].parameters && details[method].parameters.length > 0 && (
                                <>
                                    <h6>Parameters:</h6>
                                    <ul>
                                        {details[method].parameters.map((param, idx) => (
                                            <li key={idx}><code>{param.name}</code> ({param.in}, {param.required ? 'required' : 'optional'}{param.type ? `, type: ${param.type}` : ''}) - {param.description}</li>
                                        ))}
                                    </ul>
                                </> 
                            )}
                           {/* Basic Response Info */} 
                           <h6>Responses:</h6>
                           <ul>
                                {Object.entries(details[method].responses || {}).map(([code, resp]) => (
                                    <li key={code}><strong>{code}:</strong> {resp.description}</li>
                                ))}
                           </ul>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
};

// Helper to assign colors to HTTP methods
const getMethodColor = (method) => {
    switch (method.toLowerCase()) {
        case 'get': return 'success';
        case 'post': return 'primary';
        case 'put': return 'warning';
        case 'delete': return 'danger';
        case 'patch': return 'info';
        case 'options':
        case 'head': return 'secondary';
        default: return 'dark';
    }
};

const ApiExplorer = () => {
    const { swaggerDefinition, error } = useAppContext();
    const [searchTerm, setSearchTerm] = useState('');

    // Handle loading state (swaggerDefinition is null initially and after error)
    if (swaggerDefinition === null && !error) {
        return (
            <div className="text-center p-5">
                <div className="spinner-border" role="status">
                    <span className="visually-hidden">Loading...</span>
                </div>
                <p className="mt-2">Loading API Definition...</p>
            </div>
        );
    }

    // Handle error state (error is set in context if fetch fails)
    if (error && swaggerDefinition === null) {
        return <div className="alert alert-danger m-4">Error loading Swagger definition: {error}</div>;
    }
    
    // Handle case where swagger is loaded but empty or invalid (basic check)
    if (!swaggerDefinition || !swaggerDefinition.info || !swaggerDefinition.paths) {
        return <div className="alert alert-warning m-4">Swagger definition loaded but appears invalid or empty.</div>;
    }

    const { info, paths } = swaggerDefinition;

    const filteredPaths = Object.entries(paths).filter(([path]) => 
        path.toLowerCase().includes(searchTerm.toLowerCase())
    );

    return (
        <div className="container-fluid p-4">
            <h2>{info.title || 'API Definition'} <span className="badge bg-secondary">{info.version || 'N/A'}</span></h2>
            {info.description && <p className="text-muted">{info.description}</p>}
            <hr />

            <div className="mb-3">
                 <input 
                    type="text"
                    className="form-control"
                    placeholder="Filter paths... (e.g., /patient or /mapping)"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                 />
            </div>

            {filteredPaths.length > 0 ? (
                <div className="accordion" id="apiPathsAccordion">
                    {filteredPaths.map(([path, details]) => (
                        <PathDetails key={path} path={path} details={details} />
                    ))}
                </div>
            ) : (
                 <div className="alert alert-info">No API paths match your filter criteria.</div>
            )}
        </div>
    );
};

export default ApiExplorer;
