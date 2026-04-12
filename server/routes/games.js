const express = require('express');
const router = express.Router();
const Game = require('../models/Game');

function toId(doc) {
  const o = doc.toObject ? doc.toObject() : doc;
  o.id = o._id;
  return o;
}

router.get('/games', async (req, res) => {
  try {
    const games = await Game.find().sort({ _id: 1 });
    res.json(games.map(toId));
  } catch (e) { res.status(500).json({ error: e.message }); }
});

router.get('/games/:id', async (req, res) => {
  try {
    const p = req.params.id;
    const n = Number(p);
    const game = isNaN(n) ? await Game.findOne({ slug: p }) : await Game.findById(n);
    if (!game) return res.status(404).json({ error: 'Not found' });
    res.json(toId(game));
  } catch (e) { res.status(500).json({ error: e.message }); }
});

module.exports = router;
