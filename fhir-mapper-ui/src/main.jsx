import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
// Removed default ./index.css import as Bootstrap is handled in index.html

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
)
