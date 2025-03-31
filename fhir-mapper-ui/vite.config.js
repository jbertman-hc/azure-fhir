import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173, // Keep the UI running on 5173
    proxy: {
      // Proxy /api requests to the backend server
      '/api': {
        target: 'http://localhost:3000', // Correct backend port
        changeOrigin: true,
        secure: false,
        rewrite: (path) => path.replace(/^\/api/, '') // Remove /api prefix
      },
       // Proxy /swagger.json if needed (though placing in /public is preferred)
       // '/swagger.json': {
       //   target: 'http://localhost:3000', 
       //   changeOrigin: true,
       //   secure: false
       // }
    }
  }
})
