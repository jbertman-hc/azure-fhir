const express = require('express');
const router = express.Router();
const axios = require('axios');

// Helper function to make requests to the API
// This will use the proxy middleware configured in server.js
async function callApi(endpoint, method = 'GET', data = null) {
  try {
    // Use relative URLs that will be handled by the proxy middleware
    const url = endpoint.startsWith('/') ? endpoint : `/${endpoint}`;
    console.log(`Calling API: ${method} ${url}`);
    
    const response = await axios({
      method,
      url,
      baseURL: '/api', // This will be rewritten by the proxy middleware
      data: method !== 'GET' ? data : undefined,
      headers: {
        'Content-Type': 'application/json'
      }
    });
    
    return response.data;
  } catch (error) {
    console.error(`Error calling API (${endpoint}):`, error.message);
    throw error;
  }
}

// Analyze source data for potential mappings
router.post('/analyze', async (req, res) => {
  try {
    const { resourceType, sourceData } = req.body;
    
    if (!sourceData) {
      return res.status(400).json({ error: 'No source data provided' });
    }
    
    // Call the API to analyze the source data
    const result = await callApi('mapping/analyze', 'POST', {
      resourceType,
      sourceData
    });
    
    res.json(result);
  } catch (error) {
    console.error('Error analyzing data:', error);
    res.status(error.response?.status || 500).json({ 
      error: error.message,
      details: error.response?.data
    });
  }
});

// Generate mapping configuration
router.post('/generate-config', async (req, res) => {
  try {
    const { resourceType, mappings } = req.body;
    
    if (!mappings || Object.keys(mappings).length === 0) {
      return res.status(400).json({ error: 'No mappings provided' });
    }
    
    // Call the API to generate the configuration
    const result = await callApi('mapping/generate-config', 'POST', {
      resourceType,
      mappings
    });
    
    res.json(result);
  } catch (error) {
    console.error('Error generating configuration:', error);
    res.status(error.response?.status || 500).json({ 
      error: error.message,
      details: error.response?.data
    });
  }
});

// Preview FHIR resource
router.post('/preview', async (req, res) => {
  try {
    const { resourceType, sourceData, configuration } = req.body;
    
    if (!sourceData) {
      return res.status(400).json({ error: 'No source data provided' });
    }
    
    if (!configuration) {
      return res.status(400).json({ error: 'No configuration provided' });
    }
    
    // Call the API to preview the FHIR resource
    const result = await callApi('mapping/preview', 'POST', {
      resourceType,
      sourceData,
      configuration
    });
    
    res.json(result);
  } catch (error) {
    console.error('Error previewing FHIR resource:', error);
    res.status(error.response?.status || 500).json({ 
      error: error.message,
      details: error.response?.data
    });
  }
});

// Save configuration
router.post('/save-config', async (req, res) => {
  try {
    const { resourceType, configuration } = req.body;
    
    if (!configuration) {
      return res.status(400).json({ error: 'No configuration provided' });
    }
    
    // Call the API to save the configuration
    const result = await callApi('mapping/save-config', 'POST', {
      resourceType,
      configuration
    });
    
    res.json(result);
  } catch (error) {
    console.error('Error saving configuration:', error);
    res.status(error.response?.status || 500).json({ 
      error: error.message,
      details: error.response?.data
    });
  }
});

// Generate mapper class
router.post('/generate-mapper', async (req, res) => {
  try {
    const { resourceType, configuration } = req.body;
    
    if (!configuration) {
      return res.status(400).json({ error: 'No configuration provided' });
    }
    
    // Call the API to generate the mapper class
    const result = await callApi('mapping/generate-mapper', 'POST', {
      resourceType,
      configuration
    });
    
    res.json(result);
  } catch (error) {
    console.error('Error generating mapper class:', error);
    res.status(error.response?.status || 500).json({ 
      error: error.message,
      details: error.response?.data
    });
  }
});

module.exports = router;
