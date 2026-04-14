const mongoose = require('mongoose');

const QuestionSchema = new mongoose.Schema({
  text: String,
  options: [String],
  correctIndex: Number,
  explanation: String
}, { _id: false });

const FlashcardSchema = new mongoose.Schema({
  front: String,
  back: String
}, { _id: false });

const LessonSchema = new mongoose.Schema({
  _id: { type: Number, required: true },
  title: String,
  content: String,
  additionalInfo: String,
  deepDive: String,
  order: Number,
  quiz: { questions: [QuestionSchema] },
  flashcards: [FlashcardSchema]
});

const SubcategorySchema = new mongoose.Schema({
  _id: { type: Number, required: true },
  name: String,
  description: String,
  lessons: [LessonSchema]
});

const CategorySchema = new mongoose.Schema({
  _id: { type: Number, required: true },
  name: String,
  icon: String,
  color: String,
  description: String,
  subcategories: [SubcategorySchema]
});

module.exports = mongoose.model('Category', CategorySchema);
