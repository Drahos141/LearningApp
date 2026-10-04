// Read-only content store. Content is loaded once at startup — from the bundled
// content.js by default, or from MongoDB when MONGO_URI is set — and then served
// from memory with O(1) lookups. Restart the server to pick up content changes.
const mongoose = require('mongoose');
const Category = require('./models/Category');
const Game = require('./models/Game');

const withId = obj => ({ ...obj, id: obj._id });

let categories = [];
let games = [];
const lessonsById = new Map();
const subcategoriesById = new Map();
const gamesById = new Map();
const gamesBySlug = new Map();

function index(rawCategories, rawGames) {
  lessonsById.clear(); subcategoriesById.clear(); gamesById.clear(); gamesBySlug.clear();

  // Category listings carry lesson outlines only; full lessons come from /lessons/:id.
  categories = rawCategories.map(cat => withId({
    ...cat,
    subcategories: cat.subcategories.map(sub => {
      for (const lesson of sub.lessons) lessonsById.set(lesson._id, withId(lesson));
      const outline = withId({
        ...sub,
        lessons: sub.lessons.map(({ _id, order, title }) => withId({ _id, order, title })),
      });
      subcategoriesById.set(sub._id, outline);
      return outline;
    }),
  }));

  games = rawGames.map(withId);
  for (const g of games) { gamesById.set(g._id, g); gamesBySlug.set(g.slug, g); }
}

async function loadFromMongo(uri) {
  await mongoose.connect(uri);
  console.log('Connected to MongoDB');
  if (process.env.SEED_DB === 'true') {
    const { seed } = require('./seed');
    await seed();
  }
  const [cats, gms] = await Promise.all([
    Category.find().sort({ _id: 1 }).lean(),
    Game.find().sort({ _id: 1 }).lean(),
  ]);
  index(cats, gms);
}

async function load() {
  const uri = process.env.MONGO_URI;
  if (uri) {
    await loadFromMongo(uri);
  } else {
    const content = require('./content');
    index(content.categories, content.games);
  }
  console.log(`Loaded ${categories.length} categories, ${lessonsById.size} lessons, ${games.length} games` +
    (uri ? ' from MongoDB' : ' from bundled content'));
}

module.exports = {
  load,
  close: () => mongoose.connection.readyState ? mongoose.disconnect() : undefined,
  getCategories: () => categories,
  getCategory: id => categories.find(c => c._id === id),
  getSubcategory: id => subcategoriesById.get(id),
  getLesson: id => lessonsById.get(id),
  getGames: () => games,
  getGame: idOrSlug => gamesById.get(Number(idOrSlug)) ?? gamesBySlug.get(idOrSlug),
};
