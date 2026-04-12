#!/bin/bash
set -e
cd "$(dirname "$0")"

echo "=== LearningApp Setup ==="

# Check for MongoDB
if ! command -v mongod &> /dev/null; then
  echo "MongoDB is required. Install from https://www.mongodb.com/"
  exit 1
fi

# Install backend deps
echo "[1/4] Installing backend dependencies..."
cd server && npm install && cd ..

# Install + build frontend
echo "[2/4] Installing frontend dependencies..."
cd frontend && npm install

echo "[3/4] Building frontend..."
npm run build && cd ..

# Seed database
echo "[4/4] Seeding database..."
cd server && node seed.js && cd ..

echo ""
echo "=== Starting server on http://localhost:4000 ==="
node server/index.js
