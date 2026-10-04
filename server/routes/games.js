const express = require('express');
const router = express.Router();
const store = require('../store');

router.get('/games', (req, res) => {
  res.json(store.getGames());
});

router.get('/games/:id', (req, res) => {
  const game = store.getGame(req.params.id);
  game ? res.json(game) : res.status(404).json({ error: 'Not found' });
});

module.exports = router;
