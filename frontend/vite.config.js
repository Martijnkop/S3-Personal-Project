import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 443,
    https: {
      key: './cert/pos-martijn-privateKey.key',
      cert: './cert/pos-martijn.crt' ,
    },
  }
})
