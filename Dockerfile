FROM node:20-slim

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

WORKDIR /app

# Install backend
COPY server/package*.json ./server/
RUN cd server && npm install --production

# Install & build frontend
COPY frontend/package*.json ./frontend/
RUN cd frontend && npm install

COPY frontend/ ./frontend/
RUN cd frontend && npm run build

COPY server/ ./server/

EXPOSE 4000

CMD ["node", "server/index.js"]
