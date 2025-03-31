import React from 'react';
import { useAppContext } from '../context/AppContext';

const LoadingWithLog = ({ message = "Loading..." }) => {
    const { apiActivityLog } = useAppContext();

    return (
        <div className="p-4" style={{ maxHeight: '80vh', overflowY: 'auto' }}>
            <div className="text-center mb-4">
                <div className="spinner-border text-primary" role="status">
                    <span className="visually-hidden">{message}</span>
                </div>
                <p className="mt-2">{message}</p>
            </div>
            
            {apiActivityLog.length > 0 && (
                <div className="card">
                    <div className="card-header">
                        <h5 className="mb-0">Activity Log</h5>
                    </div>
                    <ul className="list-group list-group-flush" style={{ fontSize: '0.8rem' }}>
                        {apiActivityLog.map((log, index) => {
                            let icon = 'bi-info-circle';
                            let color = 'text-muted';
                            if (log.type === 'api_call') { icon = 'bi-arrow-right-circle'; color = 'text-primary'; }
                            if (log.type === 'api_response') { icon = 'bi-arrow-left-circle'; color = 'text-success'; }
                            if (log.type === 'error') { icon = 'bi-exclamation-triangle'; color = 'text-danger'; }

                            return (
                                <li key={index} className="list-group-item d-flex align-items-center">
                                    <i className={`bi ${icon} ${color} me-2`}></i>
                                    <span className={color}>{log.message}</span>
                                    {log.endpoint && <small className="text-muted ms-2">({log.endpoint})</small>}
                                </li>
                            );
                        })}
                    </ul>
                </div>
            )}
        </div>
    );
};

export default LoadingWithLog;
