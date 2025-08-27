import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      '/graphql': {
        target: 'https://localhost:7097',
        changeOrigin: true,
        secure: false, // For self-signed certificates
      },
      '/api': {
        target: 'https://localhost:7097',
        changeOrigin: true,
        secure: false, // For self-signed certificates
      }
    }
  }
})
