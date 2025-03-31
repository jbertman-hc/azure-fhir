import React from 'react';

const DataSourceBadge = ({ source }) => {
    let badgeClass = "bg-secondary";
    let tooltip = "Data source status unknown";
    let display = source || "Unknown";

    switch(source) {
        case 'api':
        case 'api-demographics':
        case 'api-addendum':
        case 'api-other':
            badgeClass = "bg-success";
            tooltip = `Data fetched from ${source}`;
            display = "API";
            break;
        case 'hybrid':
            badgeClass = "bg-warning";
            tooltip = "Data from mixed API/mock sources";
            display = "Hybrid";
            break;
        case 'mock':
            badgeClass = "bg-info";
            tooltip = "Using mock data";
            display = "Mock";
            break;
        case 'Not Connected':
             badgeClass = "bg-secondary";
             tooltip = "No data source connected";
             display = "Not Connected";
            break;
         case 'Error':
             badgeClass = "bg-danger";
             tooltip = "Error connecting to data source";
             display = "Error";
            break;
        // No default needed as we initialize above
    }

    return (
        <span 
            className={`badge ${badgeClass} ms-2`} 
            title={tooltip}
            data-bs-toggle="tooltip" 
            data-bs-placement="bottom"
        >
            <i className="bi bi-database me-1"></i>
            {display}
        </span>
    );
};

export default DataSourceBadge;
