#!/bin/bash

# Attempt to source nvm to set the correct Node.js version
export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  # This loads nvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"  # This loads nvm bash_completion (optional)

# Use the required Node version (adjust if different)
nvm use 20.17.0

# Navigate to the script's directory (project root)
cd "$(dirname "$0")"

echo "Starting Node.js backend server (serving index.html & API proxy)..."
node server.js &
SERVER_PID=$!
echo "Backend server started with PID $SERVER_PID. Access at http://localhost:3000"
echo "----------------------------------------"

echo "Starting React development server (fhir-mapper-ui)..."
# Run in a subshell to avoid changing the main script's directory
(cd fhir-mapper-ui && npm run dev) &
FRONTEND_PID=$!
echo "React dev server started with PID $FRONTEND_PID. Access typically at http://localhost:5173 (check terminal output for exact port)."
echo "----------------------------------------"

echo "Both servers are starting in the background."
echo "Use 'kill $SERVER_PID $FRONTEND_PID' or Ctrl+C in the terminals where they are running to stop them."

# Keep the script running briefly to allow servers to output initial messages
# Or use 'wait' if you want the script to block until servers are killed
# wait $SERVER_PID
# wait $FRONTEND_PID
