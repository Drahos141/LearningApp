const express = require('express');
const router = express.Router();
const store = require('../store');

const notFound = res => res.status(404).json({ error: 'Not found' });

router.get('/categories', (req, res) => {
  res.json(store.getCategories());
});

router.get('/categories/:id', (req, res) => {
  const cat = store.getCategory(Number(req.params.id));
  cat ? res.json(cat) : notFound(res);
});

router.get('/subcategories/:subId/lessons', (req, res) => {
  const sub = store.getSubcategory(Number(req.params.subId));
  if (!sub) return notFound(res);
  res.json(sub.lessons.map(l => store.getLesson(l._id)));
});

router.get('/lessons/:id', (req, res) => {
  const l = store.getLesson(Number(req.params.id));
  l ? res.json(l) : notFound(res);
});

router.get('/lessons/:id/quiz', (req, res) => {
  const l = store.getLesson(Number(req.params.id));
  if (!l) return notFound(res);
  res.json({ lessonId: l._id, questions: l.quiz.questions });
});

router.get('/lessons/:id/minigame', (req, res) => {
  const l = store.getLesson(Number(req.params.id));
  if (!l) return notFound(res);
  res.json({ lessonId: l._id, items: l.flashcards.map(c => ({ term: c.front, definition: c.back })) });
});

module.exports = router;
