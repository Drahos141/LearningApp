const mongoose = require('mongoose');

const GameSchema = new mongoose.Schema({
  _id: { type: Number, required: true },
  slug: { type: String, required: true, unique: true },
  name: String,
  description: String,
  type: { type: String, enum: ['memory','logic','sequence','word','math','pattern','spatial'] },
  instructions: String,
  difficulty: { type: String, enum: ['easy','medium','hard'] }
});

module.exports = mongoose.model('Game', GameSchema);
