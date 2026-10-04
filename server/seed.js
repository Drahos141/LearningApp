// Copies the bundled content into MongoDB (replacing what is there).
// Only needed when running with MONGO_URI; the default in-memory mode needs no seeding.
require('dotenv').config();
const mongoose = require('mongoose');
const Category = require('./models/Category');
const Game = require('./models/Game');
const { categories, games } = require('./content');

async function seed() {
  await Category.deleteMany({});
  await Game.deleteMany({});
  await Category.insertMany(categories);
  console.log('Seeded', categories.length, 'categories');
  await Game.insertMany(games);
  console.log('Seeded', games.length, 'games');
}

if (require.main === module) {
  const uri = process.env.MONGO_URI || 'mongodb://127.0.0.1:27017/learningapp';
  mongoose.connect(uri)
    .then(seed)
    .then(() => mongoose.disconnect())
    .then(() => console.log('Done.'))
    .catch(err => { console.error(err); process.exit(1); });
}

module.exports = { seed };
