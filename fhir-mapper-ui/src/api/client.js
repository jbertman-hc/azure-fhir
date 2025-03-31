import axios from 'axios';

// Configure axios with base URL (proxied by Vite)
const API_BASE_URL = '/api'; 

const apiClient = axios.create({
    baseURL: API_BASE_URL,
    headers: {
        'Content-Type': 'application/json'
    },
    // Set a more reasonable timeout
    timeout: 10000
});

export default apiClient;
