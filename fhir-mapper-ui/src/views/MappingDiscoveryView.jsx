import React, { useState, useEffect } from 'react';
import apiClient from '../api/client';
import { useAppContext } from '../context/AppContext';

const MappingDiscoveryView = () => {
  const [loading, setLoading] = useState(false);
  const [analyzing, setAnalyzing] = useState(false);
  const [error, setError] = useState(null);
  const [sourceData, setSourceData] = useState(null);
  const [manualSourceData, setManualSourceData] = useState('');
  const [resourceType, setResourceType] = useState('Patient');
  const [discoveredMappings, setDiscoveredMappings] = useState([]);
  const [selectedMappings, setSelectedMappings] = useState({});
  const [generatedConfig, setGeneratedConfig] = useState(null);
  const [previewFhir, setPreviewFhir] = useState(null);
  const [validationResults, setValidationResults] = useState(null);
  const { patients } = useAppContext();

  // Resource type options
  const resourceTypes = [
    'Patient', 
    'Condition', 
    'AllergyIntolerance', 
    'Encounter', 
    'Observation',
    'MedicationRequest',
    'Procedure',
    'DiagnosticReport',
    'DocumentReference'
  ];

  // Load sample data for the selected resource type
  useEffect(() => {
    if (resourceType === 'Patient' && patients && patients.length > 0) {
      setSourceData(patients[0]);
    } else {
      // Don't clear sourceData if we have manual data
      if (!manualSourceData) {
        setSourceData(null);
      }
    }
  }, [resourceType, patients, manualSourceData]);

  // Parse manual source data
  const parseManualData = () => {
    try {
      if (!manualSourceData.trim()) {
        setError('Please enter some JSON data');
        return;
      }
      
      const parsedData = JSON.parse(manualSourceData);
      setSourceData(parsedData);
      setError(null);
    } catch (err) {
      setError(`Invalid JSON: ${err.message}`);
    }
  };

  // Generate sample data based on resource type
  const generateSampleData = () => {
    let sampleData = {};
    
    switch(resourceType) {
      case 'Patient':
        sampleData = {
          First: "John",
          Last: "Doe",
          BirthDate: "1970-01-01",
          Gender: "M",
          SS: "123-45-6789",
          ChartID: "MRN12345",
          PatientAddress: "123 Main St",
          City: "Anytown",
          State: "CA",
          Zip: "12345",
          Phone: "555-123-4567",
          Email: "john.doe@example.com"
        };
        break;
      case 'Condition':
        sampleData = {
          PatientID: 1001,
          Code: "J45.909",
          CodeSystem: "ICD-10",
          Description: "Asthma, unspecified",
          OnsetDate: "2020-01-15",
          Status: "active"
        };
        break;
      // Add more sample data for other resource types
      default:
        sampleData = { note: "Please enter sample data for this resource type" };
    }
    
    setManualSourceData(JSON.stringify(sampleData, null, 2));
  };

  // Discover potential mappings
  const discoverMappings = async () => {
    if (!sourceData) {
      setError('No source data available for analysis');
      return;
    }

    setAnalyzing(true);
    setError(null);

    try {
      // In a real implementation, this would call the backend API
      // For demo purposes, we'll simulate the response
      const response = await apiClient.post('/api/mapping-discovery/analyze', {
        resourceType,
        sourceData
      });

      setDiscoveredMappings(response.data.mappings || []);
      
      // Initialize selected mappings with highest confidence options
      const initialSelected = {};
      const grouped = {};
      
      // Group by source property
      response.data.mappings.forEach(mapping => {
        if (!grouped[mapping.sourceProperty]) {
          grouped[mapping.sourceProperty] = [];
        }
        grouped[mapping.sourceProperty].push(mapping);
      });
      
      // Select highest confidence mapping for each source property
      Object.keys(grouped).forEach(prop => {
        const sortedMappings = grouped[prop].sort((a, b) => b.confidence - a.confidence);
        if (sortedMappings.length > 0 && sortedMappings[0].confidence > 0.5) {
          initialSelected[prop] = sortedMappings[0].targetFhirPath;
        }
      });
      
      setSelectedMappings(initialSelected);
    } catch (err) {
      console.error('Error discovering mappings:', err);
      setError(`Failed to discover mappings: ${err.message}`);
    } finally {
      setAnalyzing(false);
    }
  };

  // Generate mapping configuration
  const generateConfiguration = async () => {
    if (Object.keys(selectedMappings).length === 0) {
      setError('No mappings selected');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      // In a real implementation, this would call the backend API
      const response = await apiClient.post('/api/mapping-discovery/generate-config', {
        resourceType,
        mappings: selectedMappings
      });

      setGeneratedConfig(response.data.configuration);
    } catch (err) {
      console.error('Error generating configuration:', err);
      setError(`Failed to generate configuration: ${err.message}`);
    } finally {
      setLoading(false);
    }
  };

  // Preview FHIR resource
  const previewFhirResource = async () => {
    if (!generatedConfig) {
      setError('No configuration generated');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      // In a real implementation, this would call the backend API
      const response = await apiClient.post('/api/mapping-discovery/preview', {
        resourceType,
        sourceData,
        configuration: generatedConfig
      });

      setPreviewFhir(response.data.fhirResource);
      setValidationResults(response.data.validation);
    } catch (err) {
      console.error('Error previewing FHIR resource:', err);
      setError(`Failed to preview FHIR resource: ${err.message}`);
    } finally {
      setLoading(false);
    }
  };

  // Save configuration
  const saveConfiguration = async () => {
    if (!generatedConfig) {
      setError('No configuration generated');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      // In a real implementation, this would call the backend API
      await apiClient.post('/api/mapping-discovery/save-config', {
        resourceType,
        configuration: generatedConfig
      });

      alert('Configuration saved successfully!');
    } catch (err) {
      console.error('Error saving configuration:', err);
      setError(`Failed to save configuration: ${err.message}`);
    } finally {
      setLoading(false);
    }
  };

  // Generate mapper class
  const generateMapperClass = async () => {
    if (!generatedConfig) {
      setError('No configuration generated');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      // In a real implementation, this would call the backend API
      const response = await apiClient.post('/api/mapping-discovery/generate-mapper', {
        resourceType,
        configuration: generatedConfig
      });

      // Download the mapper class
      const blob = new Blob([response.data.mapperClass], { type: 'text/plain' });
      const url = URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = `${resourceType}Mapper.cs`;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
    } catch (err) {
      console.error('Error generating mapper class:', err);
      setError(`Failed to generate mapper class: ${err.message}`);
    } finally {
      setLoading(false);
    }
  };

  // Handle mapping selection change
  const handleMappingChange = (sourceProperty, targetFhirPath) => {
    setSelectedMappings(prev => ({
      ...prev,
      [sourceProperty]: targetFhirPath
    }));
  };

  return (
    <div className="container-fluid mt-3 p-4">
      <h2>FHIR Mapping Discovery</h2>
      <p className="text-muted">
        Automatically discover and configure mappings from legacy data to FHIR resources.
      </p>
      <hr />

      {/* Resource Type Selection */}
      <div className="card mb-4">
        <div className="card-header bg-light">
          <h5 className="mb-0">Step 1: Select Resource Type & Provide Sample Data</h5>
        </div>
        <div className="card-body">
          <div className="row mb-3">
            <div className="col-md-6">
              <label className="form-label">Resource Type</label>
              <select 
                className="form-select" 
                value={resourceType} 
                onChange={(e) => setResourceType(e.target.value)}
              >
                {resourceTypes.map(type => (
                  <option key={type} value={type}>{type}</option>
                ))}
              </select>
            </div>
            <div className="col-md-6">
              <label className="form-label">Sample Data Source</label>
              <div className="d-flex gap-2">
                <button 
                  className="btn btn-outline-secondary" 
                  onClick={generateSampleData}
                >
                  Generate Sample Data
                </button>
                {sourceData && (
                  <button 
                    className="btn btn-primary" 
                    onClick={discoverMappings}
                    disabled={analyzing}
                  >
                    {analyzing ? 'Analyzing...' : 'Discover Potential Mappings'}
                  </button>
                )}
              </div>
            </div>
          </div>

          {!sourceData && (
            <div className="alert alert-info mt-3">
              <h6>No sample data available from the system</h6>
              <p>You can manually enter or paste sample JSON data below:</p>
              <div className="mb-3">
                <textarea 
                  className="form-control font-monospace" 
                  rows="10" 
                  value={manualSourceData}
                  onChange={(e) => setManualSourceData(e.target.value)}
                  placeholder='{"First": "John", "Last": "Doe", ...}'
                ></textarea>
              </div>
              <div className="d-flex gap-2">
                <button 
                  className="btn btn-primary" 
                  onClick={parseManualData}
                >
                  Use This Data
                </button>
              </div>
            </div>
          )}

          {sourceData && (
            <div className="mt-3">
              <h6>Sample Data</h6>
              <pre className="bg-light p-3 border rounded" style={{ maxHeight: '200px', overflow: 'auto' }}>
                {JSON.stringify(sourceData, null, 2)}
              </pre>
            </div>
          )}
        </div>
      </div>

      {/* Discovered Mappings */}
      {discoveredMappings.length > 0 && (
        <div className="card mb-4">
          <div className="card-header bg-light">
            <h5 className="mb-0">Step 2: Review and Select Mappings</h5>
          </div>
          <div className="card-body">
            <div className="table-responsive">
              <table className="table table-hover">
                <thead>
                  <tr>
                    <th>Source Property</th>
                    <th>Source Value</th>
                    <th>Target FHIR Path</th>
                    <th>Confidence</th>
                    <th>Action</th>
                  </tr>
                </thead>
                <tbody>
                  {/* Group by source property */}
                  {Object.entries(
                    discoveredMappings.reduce((acc, mapping) => {
                      if (!acc[mapping.sourceProperty]) {
                        acc[mapping.sourceProperty] = [];
                      }
                      acc[mapping.sourceProperty].push(mapping);
                      return acc;
                    }, {})
                  ).map(([sourceProperty, mappings]) => (
                    <tr key={sourceProperty}>
                      <td>{sourceProperty}</td>
                      <td>{mappings[0].sourceValue}</td>
                      <td>
                        <select 
                          className="form-select form-select-sm"
                          value={selectedMappings[sourceProperty] || ''}
                          onChange={(e) => handleMappingChange(sourceProperty, e.target.value)}
                        >
                          <option value="">-- Select Mapping --</option>
                          {mappings.map((mapping, idx) => (
                            <option 
                              key={idx} 
                              value={mapping.targetFhirPath}
                            >
                              {mapping.targetFhirPath} ({(mapping.confidence * 100).toFixed(0)}%)
                            </option>
                          ))}
                        </select>
                      </td>
                      <td>
                        {mappings.find(m => m.targetFhirPath === selectedMappings[sourceProperty])?.confidence 
                          ? `${(mappings.find(m => m.targetFhirPath === selectedMappings[sourceProperty]).confidence * 100).toFixed(0)}%` 
                          : '-'}
                      </td>
                      <td>
                        <button 
                          className="btn btn-sm btn-outline-danger"
                          onClick={() => {
                            setSelectedMappings(prev => {
                              const updated = {...prev};
                              delete updated[sourceProperty];
                              return updated;
                            });
                          }}
                        >
                          Remove
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            <div className="d-flex justify-content-end mt-3">
              <button 
                className="btn btn-primary" 
                onClick={generateConfiguration}
                disabled={loading || Object.keys(selectedMappings).length === 0}
              >
                {loading ? 'Generating...' : 'Generate Configuration'}
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Generated Configuration */}
      {generatedConfig && (
        <div className="card mb-4">
          <div className="card-header bg-light">
            <h5 className="mb-0">Step 3: Review Generated Configuration</h5>
          </div>
          <div className="card-body">
            <div className="mb-3">
              <pre className="bg-light p-3 border rounded" style={{ maxHeight: '300px', overflow: 'auto' }}>
                {JSON.stringify(generatedConfig, null, 2)}
              </pre>
            </div>

            <div className="d-flex justify-content-end gap-2">
              <button 
                className="btn btn-secondary" 
                onClick={previewFhirResource}
                disabled={loading}
              >
                Preview FHIR Resource
              </button>
              <button 
                className="btn btn-primary" 
                onClick={saveConfiguration}
                disabled={loading}
              >
                Save Configuration
              </button>
              <button 
                className="btn btn-success" 
                onClick={generateMapperClass}
                disabled={loading}
              >
                Generate Mapper Class
              </button>
            </div>
          </div>
        </div>
      )}

      {/* FHIR Resource Preview */}
      {previewFhir && (
        <div className="card mb-4">
          <div className="card-header bg-light">
            <h5 className="mb-0">FHIR Resource Preview</h5>
          </div>
          <div className="card-body">
            {validationResults && !validationResults.isValid && (
              <div className="alert alert-warning mb-3">
                <h6>Validation Issues:</h6>
                <ul>
                  {validationResults.issues.map((issue, idx) => (
                    <li key={idx} className={issue.severity === 'error' ? 'text-danger' : 'text-warning'}>
                      {issue.message}
                    </li>
                  ))}
                </ul>
              </div>
            )}

            <pre className="bg-light p-3 border rounded" style={{ maxHeight: '400px', overflow: 'auto' }}>
              {JSON.stringify(previewFhir, null, 2)}
            </pre>
          </div>
        </div>
      )}

      {/* Error Display */}
      {error && (
        <div className="alert alert-danger mt-3">
          {error}
        </div>
      )}
    </div>
  );
};

export default MappingDiscoveryView;
