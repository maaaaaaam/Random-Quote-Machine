import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  test: {globals: true},
  plugins: [react({jsxRuntime: 'classic'})],
})
