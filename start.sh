#!/bin/bash
# Run the app without Docker: builds the frontend and serves everything on http://localhost:4000
set -e
cd "$(dirname "$0")"

echo "[1/3] Installing server dependencies..."
(cd server && npm ci)

echo "[2/3] Installing frontend dependencies..."
(cd frontend && npm ci)

echo "[3/3] Building frontend..."
(cd frontend && npm run build)

echo ""
echo "=== Starting server on http://localhost:${PORT:-4000} ==="
exec node server/index.js
