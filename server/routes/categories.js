const express = require('express');
const router = express.Router();
const Category = require('../models/Category');

function addId(obj) {
  if (!obj) return obj;
  obj.id = obj._id;
  return obj;
}

function toId(doc) {
  const obj = doc.toObject ? doc.toObject() : doc;
  obj.id = obj._id;
  if (obj.subcategories) obj.subcategories = obj.subcategories.map(s => {
    s.id = s._id;
    if (s.lessons) s.lessons = s.lessons.map(l => { l.id = l._id; return l; });
    return s;
  });
  return obj;
}

router.get('/categories', async (req, res) => {
  try {
    const cats = await Category.find().sort({ _id: 1 });
    res.json(cats.map(toId));
  } catch (e) { res.status(500).json({ error: e.message }); }
});

router.get('/categories/:id', async (req, res) => {
  try {
    const cat = await Category.findById(Number(req.params.id));
    if (!cat) return res.status(404).json({ error: 'Not found' });
    res.json(toId(cat));
  } catch (e) { res.status(500).json({ error: e.message }); }
});

router.get('/subcategories/:subId/lessons', async (req, res) => {
  try {
    const subId = Number(req.params.subId);
    const cat = await Category.findOne({ 'subcategories._id': subId });
    if (!cat) return res.status(404).json({ error: 'Not found' });
    const sub = cat.subcategories.find(s => s._id === subId);
    res.json(sub.lessons.map(l => { const o = l.toObject(); o.id = o._id; return o; }));
  } catch (e) { res.status(500).json({ error: e.message }); }
});

async function findLesson(id) {
  const cat = await Category.findOne({ 'subcategories.lessons._id': id });
  if (!cat) return null;
  for (const sub of cat.subcategories) {
    const l = sub.lessons.find(x => x._id === id);
    if (l) return l;
  }
  return null;
}

router.get('/lessons/:id', async (req, res) => {
  try {
    const l = await findLesson(Number(req.params.id));
    if (!l) return res.status(404).json({ error: 'Not found' });
    const o = l.toObject(); o.id = o._id;
    res.json(o);
  } catch (e) { res.status(500).json({ error: e.message }); }
});

router.get('/lessons/:id/quiz', async (req, res) => {
  try {
    const l = await findLesson(Number(req.params.id));
    if (!l) return res.status(404).json({ error: 'Not found' });
    res.json({ lessonId: l._id, questions: l.quiz.questions });
  } catch (e) { res.status(500).json({ error: e.message }); }
});

router.get('/lessons/:id/minigame', async (req, res) => {
  try {
    const l = await findLesson(Number(req.params.id));
    if (!l) return res.status(404).json({ error: 'Not found' });
    res.json({ lessonId: l._id, items: l.flashcards.map(c => ({ term: c.front, definition: c.back })) });
  } catch (e) { res.status(500).json({ error: e.message }); }
});

module.exports = router;
