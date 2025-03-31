import React, { useState } from 'react';
import { useAppContext } from '../context/AppContext';

const Sidebar = () => {
    const { view, activeModule, setActiveModule, selectedPatient } = useAppContext();
    const [isExpanded, setIsExpanded] = useState(true);

    // Only show sidebar when viewing patient details (might remove later)
    if (view !== 'patient-detail' || !selectedPatient) {
        return null;
    }

    const modules = [
        { key: 'summary', label: 'Summary', icon: 'bi-file-earmark-text' },
        { key: 'demographics', label: 'Demographics', icon: 'bi-person-badge' },
        { key: 'problems', label: 'Problems', icon: 'bi-exclamation-octagon' },
        { key: 'medications', label: 'Medications', icon: 'bi-capsule' },
        { key: 'allergies', label: 'Allergies', icon: 'bi-bandaid' },
        // Add other patient modules if needed
    ];

    return (
        <div className={`col-md-${isExpanded ? '2' : '1'} p-0`} style={{ transition: 'width 0.3s ease' }}>
            <div className="d-flex flex-column vh-100 bg-light border-end position-sticky top-0">
                <div className="p-2 text-center border-bottom">
                    <button className="btn btn-sm btn-outline-secondary" onClick={() => setIsExpanded(!isExpanded)}>
                        <i className={`bi ${isExpanded ? 'bi-chevron-double-left' : 'bi-chevron-double-right'}`}></i>
                    </button>
                </div>
                <ul className="nav nav-pills flex-column mb-auto p-2">
                    {modules.map(module => (
                        <li className="nav-item" key={module.key}>
                            <a 
                                href="#"
                                className={`nav-link text-dark ${activeModule === module.key ? 'active' : ''}`}
                                onClick={() => setActiveModule(module.key)}
                                title={module.label}
                            >
                                <i className={`bi ${module.icon} me-2`}></i>
                                {isExpanded && module.label}
                            </a>
                        </li> 
                    ))}
                </ul>
                <div className="border-top p-2 text-center">
                    {/* Optional footer content */}
                    {isExpanded && <small className="text-muted">Patient View</small>}
                </div>
            </div>
        </div>
    );
};

export default Sidebar;
