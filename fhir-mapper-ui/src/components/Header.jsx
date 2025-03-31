import React from 'react';
import { useAppContext } from '../context/AppContext';
import DataSourceBadge from './DataSourceBadge'; // Import the badge

const Header = () => {
    const { view, setView, dataSource, selectedPatient } = useAppContext();

    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark sticky-top">
            <div className="container-fluid">
                {/* Changed title and icon for mapper focus */}
                <a className="navbar-brand" href="#" onClick={() => setView('dashboard')}> 
                    <i className="bi bi-bezier me-1"></i> FHIR Mapper Workbench
                </a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav me-auto">
                        <li className="nav-item">
                            <a className={`nav-link ${view === 'dashboard' ? 'active' : ''}`} href="#" onClick={() => setView('dashboard')}>
                                <i className="bi bi-grid-1x2 me-1"></i> Dashboard
                            </a>
                        </li>
                         {/* Simplified navigation for mapper focus - Add more as needed */}
                         <li className="nav-item">
                             <a className={`nav-link ${view === 'apiExplorer' ? 'active' : ''}`} href="#" onClick={() => setView('apiExplorer')}>
                                 <i className="bi bi-hdd-stack me-1"></i> API Explorer
                             </a>
                         </li>
                         <li className="nav-item">
                            <a className={`nav-link ${view === 'mappingStatus' ? 'active' : ''}`} href="#" onClick={() => setView('mappingStatus')}>
                                <i className="bi bi-table me-1"></i> Mapping Status
                            </a>
                        </li>
                        <li className="nav-item">
                            <a className={`nav-link ${view === 'mappingDiscovery' ? 'active' : ''}`} href="#" onClick={() => setView('mappingDiscovery')}>
                                <i className="bi bi-magic me-1"></i> Mapping Discovery
                            </a>
                        </li>
                        {/* Removed Patient links, kept API testing for now */}
                        {selectedPatient && (
                            <li className="nav-item">
                                <a className={`nav-link ${view === 'patient-detail' ? 'active' : ''}`} href="#" /* onClick={() => setView('patient-detail')} */>
                                    <i className="bi bi-person me-1"></i> 
                                    {/* Displaying patient name might be irrelevant now */}
                                    {selectedPatient.First} {selectedPatient.Last}
                                </a>
                            </li>
                        )}
                        <li className="nav-item">
                            <a className={`nav-link ${view === 'apiTesting' ? 'active' : ''}`} href="#" onClick={() => setView('apiTesting')}>
                                <i className="bi bi-code-slash me-1"></i> Legacy API Testing
                            </a>
                        </li>
                       
                    </ul>
                    {/* Keep data source badge for context */}
                    <div className="d-flex align-items-center">
                        <DataSourceBadge source={dataSource} />
                    </div>
                    {/* Removed Search bar */}
                </div>
            </div>
        </nav>
    );
};

export default Header;
