const express = require('express');
const cors = require('cors');
const { createProxyMiddleware } = require('http-proxy-middleware');
const axios = require('axios');

const app = express();
const PORT = process.env.PORT || 3000;

// Enable CORS for all routes
app.use(cors());

// Serve static files from the current directory
app.use(express.static('.'));

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

// Test specific endpoint - Addendum with ID 1
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

// Test available endpoints
app.get('/test-available-endpoints', async (req, res) => {
  try {
    console.log('Testing various endpoints to find available ones...');
    
    const endpoints = [
      'Addendum/1',
      'Demographics',
      'Demographics/1',
      'PatientIndex/1',
      'ListAllergies/Patient/1',
      'ListProblem/Patient/1',
      'ListMEDS/Patient/1',
      'ProviderIndex'
    ];
    
    const results = {};
    
    for (const endpoint of endpoints) {
      try {
        console.log(`Testing endpoint: ${endpoint}`);
        const response = await axios.get(`https://apiserviceswin20250318.azurewebsites.net/api/${endpoint}`);
        results[endpoint] = {
          success: true,
          status: response.status,
          dataAvailable: response.data && (Array.isArray(response.data) ? 
                                          response.data.length > 0 : 
                                          Object.keys(response.data).length > 0)
        };
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

// Start the server
app.listen(PORT, () => {
  console.log(`CORS proxy server running on port ${PORT}`);
  console.log(`Visit http://localhost:${PORT} to access the EHR application`);
  console.log(`API requests will be proxied to https://apiserviceswin20250318.azurewebsites.net/api`);
});