const express = require('express');
const cors = require('cors');
const { createProxyMiddleware } = require('http-proxy-middleware');
const axios = require('axios');
const path = require('path');
const fs = require('fs').promises; // Use promises for async file operations
const os = require('os');

const app = express();
const PORT = process.env.PORT || 3000;

// Enable CORS for all routes
app.use(cors());
app.use(express.json());

// Basic health check endpoint
app.get('/health', (req, res) => {
  res.status(200).json({ status: 'ok', message: 'Server is running' });
});

// Define API routes first (before static file serving)
// This ensures our API endpoints get priority

// Debug endpoint to test direct connection to Azure API
app.get('/test-api-connection', async (req, res) => {
  try {
    console.log('Testing direct connection to API...');
    const response = await axios.get('https://apiserviceswin20250318.azurewebsites.net/api/Demographics');
    console.log('API Response:', response.status);
    res.json({
      success: true,
      status: response.status,
      data: response.data
    });
  } catch (error) {
    console.error('Error testing API connection:', error.message);
    res.status(500).json({
      success: false,
      error: error.message,
      response: error.response ? {
        status: error.response.status,
        data: error.response.data
      } : null
    });
  }
});

// Test any endpoint with optional ID parameter
app.get('/test-endpoint/:endpoint/:id?', async (req, res) => {
  const endpoint = req.params.endpoint;
  const id = req.params.id || '';
  const path = id ? `${endpoint}/${id}` : endpoint;
  
  try {
    console.log(`Testing endpoint: ${path}`);
    const response = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/${path}`);
    console.log(`API Response for ${path}:`, response.status);
    res.json({
      success: true,
      status: response.status,
      data: response.data
    });
  } catch (error) {
    console.error(`Error testing ${path} endpoint:`, error.message);
    res.status(500).json({
      success: false,
      error: error.message,
      response: error.response ? {
        status: error.response.status,
        data: error.response.data
      } : null
    });
  }
});

// Special endpoint to get all patient demographics
app.get('/get-all-patients', async (req, res) => {
  try {
    console.log('Attempting to fetch all patients demographics...');
    
    // We know these patient IDs exist based on previous info 
    const patientIds = [1001, 1033]; // William Saffire and Bobo the Clown
    
    // Try all possible patient IDs from 1000-1050
    for (let i = 1000; i <= 1050; i++) {
      if (!patientIds.includes(i)) {
        patientIds.push(i);
      }
    }
    
    // Create a function to fetch a single patient
    const fetchPatient = async (id) => {
      try {
        // Try multiple endpoints for each patient
        const endpoints = [
          `Demographics/${id}`, 
          `PatientIndex/${id}`,
          `Addendum/${id}`
        ];
        
        for (const endpoint of endpoints) {
          try {
            const response = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/${endpoint}`);
            
            // Handle 204 No Content properly 
            if (response.status === 204) {
              console.log(`Endpoint ${endpoint} exists but returned No Content`);
              continue;
            }
            
            if (response.status === 200 && response.data) {
              console.log(`Found data for patient ${id} in endpoint ${endpoint}`);
              return {
                id,
                endpoint,
                data: response.data,
                source: endpoint.split('/')[0]
              };
            }
          } catch (err) {
            // Ignore the error and try next endpoint
          }
        }
        
        return null;
      } catch (err) {
        console.warn(`Error fetching patient ${id}:`, err.message);
        return null;
      }
    };
    
    // Fetch all patients in parallel
    const fetchPromises = patientIds.map(id => fetchPatient(id));
    const results = await Promise.all(fetchPromises);
    
    // Filter out nulls and collect patient data
    const patients = results
      .filter(result => result !== null)
      .map(result => {
        // Format patient data based on the source
        const patientData = result.data;
        
        // Format the data based on whether it's from Demographics, PatientIndex or Addendum
        if (result.source === 'Demographics') {
          return {
            patientId: result.id,
            firstName: patientData.First,
            lastName: patientData.Last,
            birthDate: patientData.BirthDate,
            gender: patientData.Gender,
            chartId: patientData.ChartID,
            source: 'Demographics'
          };
        } else if (result.source === 'PatientIndex') {
          return {
            patientId: result.id,
            acPatientId: patientData.AcPatientId,
            externalPatientId: patientData.ExternalPatientId,
            source: 'PatientIndex'
          };
        } else if (result.source === 'Addendum') {
          const patientName = patientData.patientName || '';
          const nameParts = patientName.split(', ');
          const lastName = nameParts[0] || '';
          const firstName = nameParts.length > 1 ? nameParts[1].split(' ')[0] : '';
          
          // Extract DOB if present in the string
          const dobMatch = patientName.match(/DOB: (\d+\/\d+\/\d+)/);
          const birthDate = dobMatch ? new Date(dobMatch[1]).toISOString() : null;
          
          return {
            patientId: result.id,
            firstName,
            lastName,
            birthDate,
            addendumNote: patientData.noteBody,
            addendumDate: patientData.date,
            source: 'Addendum'
          };
        } else {
          return {
            patientId: result.id,
            rawData: patientData,
            source: result.source
          };
        }
      });
    
    res.json({
      success: true,
      total: patients.length,
      patients
    });
  } catch (error) {
    console.error('Error fetching all patients:', error);
    res.status(500).json({
      success: false,
      error: error.message
    });
  }
});

// Test specific endpoint - Addendum with ID (for backward compatibility)
app.get('/test-addendum/:id', async (req, res) => {
  const id = req.params.id;
  try {
    console.log(`Testing Addendum endpoint with ID ${id}...`);
    const response = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/Addendum/${id}`);
    console.log('Addendum API Response:', response.status);
    res.json({
      success: true,
      status: response.status,
      data: response.data
    });
  } catch (error) {
    console.error(`Error testing Addendum/${id} endpoint:`, error.message);
    res.status(500).json({
      success: false,
      error: error.message,
      response: error.response ? {
        status: error.response.status,
        data: error.response.data
      } : null
    });
  }
});

// Try accessing SQL repositories directly
app.get('/sql-repositories', async (req, res) => {
  try {
    console.log('Trying to access SQL repositories directly...');
    
    // Check various repository endpoints
    const repositories = [
      'DemographicsRepository',
      'PatientIndexRepository',
      'ListPmAccountsRepository',
      'ListPatientRegistryInfoRepository',
      'PatientChargesRepository',
      'PatientOptionsRepository',
      'PatientMessagesRepository'
    ];
    
    const results = {};
    
    // Try both bare endpoints and get methods
    for (const repo of repositories) {
      try {
        // Try bare repository
        const bareResponse = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/${repo}`);
        results[repo] = {
          success: true,
          status: bareResponse.status,
          hasData: bareResponse.data && 
                  (Array.isArray(bareResponse.data) ? 
                   bareResponse.data.length > 0 : 
                   Object.keys(bareResponse.data).length > 0)
        };
      } catch (bareError) {
        results[repo] = {
          success: false,
          status: bareError.response?.status || 'Error',
          error: bareError.message
        };
      }
      
      // Try with GetAll method
      try {
        const getAllResponse = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/${repo}/GetAll`);
        results[`${repo}/GetAll`] = {
          success: true,
          status: getAllResponse.status,
          hasData: getAllResponse.data && 
                  (Array.isArray(getAllResponse.data) ? 
                   getAllResponse.data.length > 0 : 
                   Object.keys(getAllResponse.data).length > 0)
        };
      } catch (getAllError) {
        results[`${repo}/GetAll`] = {
          success: false,
          status: getAllError.response?.status || 'Error',
          error: getAllError.message
        };
      }
    }
    
    res.json({
      success: true,
      message: 'Completed scan of SQL repositories',
      results
    });
  } catch (error) {
    console.error('Error in sql-repositories:', error.message);
    res.status(500).json({
      success: false,
      error: error.message
    });
  }
});

// Try to get a list of all patients from various potential endpoints
app.get('/list-all-patients', async (req, res) => {
  try {
    console.log(`[${new Date().toISOString()}] Received request for /list-all-patients`); 
    console.log('  Request Headers:', req.headers); 
    
    console.log('Attempting to list all patients from various endpoint sources...');
    
    // Endpoints that might return collections of patients
    const listEndpoints = [
      'PatientIndex',
      'Demographics',
      'ListPmAccounts',
      'DemographicsRepository',
      'PatientIndexRepository',
      'DemographicsRepository/GetDemographics',
      'PatientIndexRepository/GetPatientIndexs',
      'PatientIndex/GetCount',
      'Demographics/GetCount'
    ];
    
    const results = {};
    
    // Try each potential endpoint
    for (const endpoint of listEndpoints) {
      try {
        console.log(`Trying to list all patients from: ${endpoint}`);
        const response = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/${endpoint}`);
        
        if (response.status === 200) {
          // If we get data, analyze it
          let data = null;
          if (Array.isArray(response.data)) {
            data = {
              count: response.data.length,
              isArray: true,
              sampleData: response.data.length > 0 ? response.data.slice(0, 2) : []
            };
          } else if (typeof response.data === 'object' && response.data !== null) {
            data = {
              count: Object.keys(response.data).length,
              isArray: false,
              sampleData: response.data
            };
          }
          
          results[endpoint] = {
            success: true,
            status: response.status,
            data
          };
          
          console.log(`Found data at ${endpoint}: ${data ? (data.isArray ? `${data.count} items` : 'Object') : 'No data'}`);
        } else {
          results[endpoint] = {
            success: false,
            status: response.status,
            message: 'No data returned'
          };
        }
      } catch (error) {
        results[endpoint] = {
          success: false,
          status: error.response?.status || 'Error',
          error: error.message
        };
        console.log(`Error accessing ${endpoint}: ${error.message}`);
      }
    }
    
    // Try PatientIDs to see which ones have data
    const patientSamples = [];
    
    // First, try the known patients
    const knownPatientIDs = [1001, 1033]; // William Saffire and Bobo the Clown
    
    // Then try a wider range for discovery
    const testPatientIDs = [];
    // Try specific IDs
    [1, 2, 3, 4, 5, 10, 100, 1000, 1001, 1002, 1033, 2000, 3000, 4000, 5000, 10000].forEach(id => {
      if (!knownPatientIDs.includes(id)) {
        testPatientIDs.push(id);
      }
    });
    
    // Then try ranges
    for (let i = 1000; i <= 1050; i++) {
      if (!knownPatientIDs.includes(i) && !testPatientIDs.includes(i)) {
        testPatientIDs.push(i);
      }
    }
    
    // First test the known patients
    for (const patientId of knownPatientIDs) {
      try {
        // First check Demographics
        const demoResponse = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/Demographics/${patientId}`);
        
        // Even if we get 204 No Content, the patient ID might still be valid
        if (demoResponse.status === 200 || demoResponse.status === 204) {
          patientSamples.push({
            patientId: patientId,
            exists: true,
            hasData: demoResponse.status === 200,
            source: 'Demographics',
            known: true
          });
          console.log(`Patient ID ${patientId} exists in Demographics (status: ${demoResponse.status})`);
          continue; // Skip to next patient ID
        }
      } catch (error) {
        // Patient might not exist in Demographics, try PatientIndex
        try {
          const indexResponse = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/PatientIndex/${patientId}`);
          
          if (indexResponse.status === 200 || indexResponse.status === 204) {
            patientSamples.push({
              patientId: patientId,
              exists: true,
              hasData: indexResponse.status === 200,
              source: 'PatientIndex',
              known: true
            });
            console.log(`Patient ID ${patientId} exists in PatientIndex (status: ${indexResponse.status})`);
            continue; // Skip to next patient ID
          }
        } catch (indexError) {
          // Patient not found in PatientIndex either
          console.log(`Known patient ID ${patientId} not found in either Demographics or PatientIndex`);
        }
      }
    }
    
    // Now check the test patient IDs
    for (const patientId of testPatientIDs) {
      try {
        // First check Demographics
        const demoResponse = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/Demographics/${patientId}`);
        
        // Even if we get 204 No Content, the patient ID might still be valid
        if (demoResponse.status === 200 || demoResponse.status === 204) {
          patientSamples.push({
            patientId: patientId,
            exists: true,
            hasData: demoResponse.status === 200,
            source: 'Demographics'
          });
          console.log(`Patient ID ${patientId} exists in Demographics (status: ${demoResponse.status})`);
          continue; // Skip to next patient ID
        }
      } catch (error) {
        // Patient might not exist in Demographics, try PatientIndex
        try {
          const indexResponse = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/PatientIndex/${patientId}`);
          
          if (indexResponse.status === 200 || indexResponse.status === 204) {
            patientSamples.push({
              patientId: patientId,
              exists: true,
              hasData: indexResponse.status === 200,
              source: 'PatientIndex'
            });
            console.log(`Patient ID ${patientId} exists in PatientIndex (status: ${indexResponse.status})`);
            continue; // Skip to next patient ID
          }
        } catch (indexError) {
          // Patient not found in PatientIndex either
        }
      }
    }
    
    res.json({
      success: true,
      message: 'Completed scan of potential patient list endpoints',
      listResults: results,
      patientSamples
    });
  } catch (error) {
    console.error('Error in list-all-patients:', error.message);
    res.status(500).json({
      success: false,
      error: error.message
    });
  }
});

// Special endpoint to test demographics with improved handling
app.get('/test-patient-demographics/:id', async (req, res) => {
  const id = req.params.id;
  try {
    console.log(`Testing Demographics endpoint with ID ${id} and handling 204...`);
    
    // Create a result object
    let result = {
      patientId: id,
      attempts: [],
      data: null
    };
    
    // Try multiple endpoints to get patient data
    const endpoints = [
      `Demographics/${id}`,
      `PatientIndex/${id}`,
      `ListProblem/Patient/${id}`,
      `Addendum/${id}`,
      `DemographicsRepository/GetDemographics/${id}`
    ];
    
    for (const endpoint of endpoints) {
      try {
        console.log(`Trying endpoint: ${endpoint}`);
        const response = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/${endpoint}`);
        
        result.attempts.push({
          endpoint,
          status: response.status,
          success: true,
          hasData: response.status === 200 && !!response.data
        });
        
        // If we got a 204 No Content, continue to try other endpoints
        if (response.status === 204) {
          console.log(`${endpoint} returned 204 No Content`);
          continue;
        }
        
        // If we have data, save it
        if (response.status === 200 && response.data) {
          const source = endpoint.split('/')[0];
          console.log(`<<< Raw data received from ${endpoint} (Status ${response.status}):`, JSON.stringify(response.data));
          
          // Process the data based on source
          if (source === 'Demographics') {
            result.data = {
              source: 'Demographics',
              firstName: response.data.First,
              lastName: response.data.Last,
              gender: response.data.Gender,
              birthDate: response.data.BirthDate,
              chartId: response.data.ChartID,
              rawData: response.data
            };
            break; // Exit the loop if we found Demographics data
          } else if (source === 'PatientIndex') {
            result.data = {
              source: 'PatientIndex',
              acPatientId: response.data.AcPatientId,
              externalPatientId: response.data.ExternalPatientId,
              firstName: response.data.First || response.data.firstName || response.data.GivenName,
              lastName: response.data.Last || response.data.lastName || response.data.FamilyName,
              rawData: response.data
            };
            // Continue looking for better data sources
          } else if (source === 'Addendum') {
            const patientName = response.data.patientName || '';
            const nameParts = patientName.split(', ');
            const lastName = nameParts[0] || '';
            const firstName = nameParts.length > 1 ? nameParts[1].split(' ')[0] : '';
            
            // Extract DOB if present in the string
            const dobMatch = patientName.match(/DOB: (\d+\/\d+\/\d+)/);
            const birthDate = dobMatch ? new Date(dobMatch[1]).toISOString() : null;
            
            result.data = {
              source: 'Addendum',
              firstName,
              lastName,
              birthDate,
              addendumNote: response.data.noteBody,
              addendumDate: response.data.date,
              rawData: response.data
            };
            // Continue looking for better data sources
          } else {
            // For other endpoints, just save the raw data
            if (!result.data) {
              result.data = {
                source,
                firstName: response.data.First || response.data.firstName || response.data.GivenName,
                lastName: response.data.Last || response.data.lastName || response.data.FamilyName,
                rawData: response.data
              };
            }
          }
          console.log(`>>> Processed data assigned from ${source}:`, JSON.stringify(result.data));
        }
      } catch (error) {
        result.attempts.push({
          endpoint,
          status: error.response?.status || 'No response',
          success: false,
          error: error.message
        });
      }
    }
    
    // Return the collected data
    res.json({
      success: true,
      result
    });
  } catch (error) {
    console.error(`Error in test-patient-demographics:`, error.message);
    res.status(500).json({
      success: false,
      error: error.message
    });
  }
});

// Test available endpoints
app.get('/test-available-endpoints', async (req, res) => {
  try {
    console.log('Testing various endpoints to find available ones...');
    
    const endpoints = [
      'Addendum/1',
      'Demographics',
      'Demographics/1',
      'Demographics/1001',  // William Saffire
      'Demographics/1033',  // Bobo the Clown
      'PatientIndex',
      'PatientIndex/1',
      'PatientIndex/1001',
      'ListAllergies/Patient/1001',
      'ListProblem/Patient/1001',
      'ListMEDS/Patient/1001',
      'SOAP/1001',
      'ProviderIndex',
      'DemographicsRepository/1001', // Try the repository directly
      'DemographicsRepository/GetDemographics/1001',  // Try with method name
      'ListPmAccounts', // Might list patient accounts
      'ListPatientRegistryInfo', // Might contain patient registry info
      'PatientIndex' // Bare endpoint might list all patients
    ];
    
    const results = {};
    
    for (const endpoint of endpoints) {
      try {
        console.log(`Testing endpoint: ${endpoint}`);
        const response = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/${endpoint}`);
        
        // Check for 204 No Content - valid but empty
        if (response.status === 204) {
          results[endpoint] = {
            success: true,
            status: 204,
            dataAvailable: false,
            note: 'Endpoint exists but returned No Content'
          };
        } else {
          results[endpoint] = {
            success: true,
            status: response.status,
            dataAvailable: response.data && (Array.isArray(response.data) ? 
                                            response.data.length > 0 : 
                                            Object.keys(response.data).length > 0),
            dataPreview: response.data ? JSON.stringify(response.data).substring(0, 100) + '...' : 'No data'
          };
        }
      } catch (error) {
        results[endpoint] = {
          success: false,
          status: error.response?.status || 'No response',
          error: error.message
        };
      }
    }
    
    res.json({
      summary: 'Endpoint testing complete',
      results
    });
  } catch (error) {
    console.error('Error in endpoint testing:', error.message);
    res.status(500).json({
      success: false,
      error: error.message
    });
  }
});

// New Mapping Status Endpoint
const mappersDir = path.join(__dirname, 'FHIRMappers');
const flagRegex = /<<<\s*FLAG:(.*?)\s*>>>/g; // Regex to find flags

app.get('/mapping-status', async (req, res) => {
  console.log('[server.js] Received request for /mapping-status'); 
  const mappingDetails = {};
  let hasErrors = false;
  let files = [];

  try {
    console.log(`[server.js] Attempting to read directory: ${mappersDir}`); 
    
    // Try reading the directory. If it fails with ENOENT, the directory doesn't exist.
    try {
        files = await fs.readdir(mappersDir);
        console.log(`[server.js] Found ${files.length} items in ${mappersDir}`);
    } catch (dirReadError) {
        if (dirReadError.code === 'ENOENT') {
            console.warn(`[server.js] Warning: FHIRMappers directory not found at ${mappersDir}`); 
            return res.status(404).json({ 
                message: 'FHIRMappers directory not found',
                error: `Directory not found: ${mappersDir}`,
                mappingDetails: {} 
            });
        } else {
            // Re-throw other errors to be caught by the outer try...catch
            throw dirReadError;
        }
    }

    for (const file of files) {
      const filePath = path.join(mappersDir, file);
      const fileStats = await fs.stat(filePath);

      if (fileStats.isFile() && path.extname(file).toLowerCase() === '.cs') {
        const mapperName = path.basename(file, '.cs');
        // Basic attempt to guess the resource from the name
        const potentialResource = mapperName.endsWith('Mapper') ? mapperName.slice(0, -6) : mapperName;
        const details = { 
          mapperName: mapperName,
          potentialResource: potentialResource,
          flags: [],
          error: null
        };
        
        try {
          console.log(`[server.js] Reading file: ${filePath}`); 
          const content = await fs.readFile(filePath, 'utf8');
          const lines = content.split(os.EOL); // Split by OS-specific newline
          
          const flagRegex = /<<<\s*FLAG:(.*?)(?:>>>|$)/gmi; // Global, multiline, case-insensitive
          let match;
          lines.forEach((line, index) => {
            flagRegex.lastIndex = 0; // Reset regex index for each line
            while ((match = flagRegex.exec(line)) !== null) {
              details.flags.push({
                line: index + 1,
                flagContent: match[1].trim(),
                fullLine: line.trim() // Store the whole line for context
              });
            }
          });
          console.log(`[server.js] Found ${details.flags.length} flags in ${file}`); 
        } catch (fileReadError) {
          console.error(`[server.js] Error reading file ${file}:`, fileReadError); 
          details.error = `Error reading file: ${fileReadError.message}`;
          hasErrors = true;
        }
        mappingDetails[file] = details; // Use filename as key
      }
    }
    
    console.log(`[server.js] Finished processing mappers. Sending response.`); 
    res.status(hasErrors ? 207 : 200).json({ 
      message: hasErrors ? 'Processed mappers with some errors' : 'Successfully processed mappers', 
      mappingDetails: mappingDetails
    });

  } catch (err) {
    console.error('[server.js] Critical error in /mapping-status:', err); 
    res.status(500).json({
      message: 'Failed to get mapping status due to server error',
      error: err.message,
      mappingDetails: {}
    });
  }
});

// Proxy middleware configuration with debug logging
const apiProxy = createProxyMiddleware('/api', {
  target: 'https://apiserviceswin20250318.azurewebsites.net',
  changeOrigin: true,
  logLevel: 'debug',
  pathRewrite: {
    '^/api': '/api', // no path rewrite needed in this case
  },
  onProxyReq: function(proxyReq, req, res) {
    console.log(`Proxying request to: ${proxyReq.path}`);
    // You can modify headers here if needed
    // proxyReq.setHeader('Authorization', 'Bearer YOUR_TOKEN');
  },
  onProxyRes: function(proxyRes, req, res) {
    // Log response status
    console.log(`Received response: ${proxyRes.statusCode} for ${req.url}`);
    
    // Add CORS headers to the proxy response
    proxyRes.headers['Access-Control-Allow-Origin'] = '*';
    proxyRes.headers['Access-Control-Allow-Methods'] = 'GET, POST, PUT, DELETE, OPTIONS';
    proxyRes.headers['Access-Control-Allow-Headers'] = 'Content-Type, Authorization';
  },
  onError: function(err, req, res) {
    console.error('Proxy error:', err);
    res.status(500).json({ error: 'Proxy error occurred', message: err.message });
  }
});

// Use the proxy middleware
app.use('/api', apiProxy);

// Serve static files from the current directory
// This comes AFTER defining all API routes so they get priority
app.use(express.static('.'));

// Start the server
app.listen(PORT, () => {
  console.log(`CORS proxy server running on port ${PORT}`);
  console.log(`Visit http://localhost:${PORT} to access the EHR application`);
  console.log(`API requests will be proxied to https://apiserviceswin20250318.azurewebsites.net/api`);
  
  // List available API endpoints for testing
  console.log('\nAvailable API endpoints for testing:');
  console.log('- /test-api-connection');
  console.log('- /test-endpoint/:endpoint/:id?');
  console.log('- /get-all-patients');
  console.log('- /test-addendum/:id');
  console.log('- /sql-repositories');
  console.log('- /list-all-patients');
  console.log('- /test-patient-demographics/:id');
  console.log('- /test-available-endpoints');
  console.log('- /mapping-status');
});