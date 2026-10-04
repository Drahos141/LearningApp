require('dotenv').config();
const express = require('express');
const cors = require('cors');
const fs = require('fs');
const path = require('path');

const store = require('./store');
const categoriesRouter = require('./routes/categories');
const gamesRouter = require('./routes/games');

const app = express();
const PORT = process.env.PORT || 4000;
const STATIC_DIR = process.env.STATIC_DIR || path.join(__dirname, '../frontend/dist');

app.disable('x-powered-by');
app.use(cors());
app.use(express.json());

app.get('/api/health', (req, res) => res.json({ status: 'ok' }));
app.use('/api', categoriesRouter);
app.use('/api', gamesRouter);
app.use('/api', (req, res) => res.status(404).json({ error: 'Not found' }));

// Serve the built frontend (single-container mode). Hashed assets are cached long-term;
// any other non-API path falls back to index.html for client-side routing.
if (fs.existsSync(path.join(STATIC_DIR, 'index.html'))) {
  app.use('/assets', express.static(path.join(STATIC_DIR, 'assets'), { immutable: true, maxAge: '1y' }));
  app.use(express.static(STATIC_DIR));
  app.get('*', (req, res) => res.sendFile(path.join(STATIC_DIR, 'index.html')));
} else {
  console.log(`No frontend build at ${STATIC_DIR} — serving API only`);
}

store.load()
  .then(() => {
    const server = app.listen(PORT, () => console.log(`Server running on http://localhost:${PORT}`));
    const shutdown = () => server.close(() => Promise.resolve(store.close()).then(() => process.exit(0)));
    process.on('SIGTERM', shutdown);
    process.on('SIGINT', shutdown);
  })
  .catch(err => {
    console.error('Failed to load content:', err);
    process.exit(1);
  });
