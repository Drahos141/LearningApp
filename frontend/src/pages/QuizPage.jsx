import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getQuiz } from '../api/api';

export default function QuizPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [quiz, setQuiz] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const [current, setCurrent] = useState(0);
  const [selected, setSelected] = useState(null);
  const [score, setScore] = useState(0);
  const [finished, setFinished] = useState(false);

  useEffect(() => {
    getQuiz(id)
      .then(setQuiz)
      .catch(() => setError('Quiz not found.'))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;
  if (error) return <div className="page-center error-msg">{error}</div>;

  const questions = quiz.questions;
  const question = questions[current];
  const progress = ((current) / questions.length) * 100;

  function handleSelect(idx) {
    if (selected !== null) return;
    setSelected(idx);
    if (idx === question.correctIndex) setScore(s => s + 1);
  }

  function handleNext() {
    if (current + 1 >= questions.length) {
      setFinished(true);
    } else {
      setCurrent(c => c + 1);
      setSelected(null);
    }
  }

  if (finished) {
    const pct = Math.round((score / questions.length) * 100);
    return (
      <div className="page quiz-page">
        <div className="quiz-result">
          <div className="result-emoji">{pct === 100 ? '🏆' : pct >= 60 ? '👍' : '📚'}</div>
          <h2>Quiz Complete!</h2>
          <p className="result-score">
            You got <strong>{score}</strong> out of <strong>{questions.length}</strong> correct
          </p>
          <div className="result-bar">
            <div className="result-fill" style={{ width: `${pct}%` }} />
          </div>
          <p className="result-pct">{pct}%</p>
          <div className="result-actions">
            <button className="action-btn quiz-btn" onClick={() => navigate(`/lesson/${id}`)}>
              ← Back to Lesson
            </button>
            <button className="action-btn game-btn" onClick={() => navigate(`/lesson/${id}/minigame`)}>
              🎴 Play Flashcards
            </button>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="page quiz-page">
      <div className="quiz-header">
        <button className="back-btn" onClick={() => navigate(`/lesson/${id}`)}>← Back</button>
        <span className="quiz-counter">Question {current + 1} / {questions.length}</span>
      </div>

      <div className="progress-bar">
        <div className="progress-fill" style={{ width: `${progress}%` }} />
      </div>

      <div className="question-card">
        <p className="question-text">{question.text}</p>

        <div className="options-grid">
          {question.options.map((opt, idx) => {
            let cls = 'option-btn';
            if (selected !== null) {
              if (idx === question.correctIndex) cls += ' correct';
              else if (idx === selected) cls += ' wrong';
            }
            return (
              <button key={idx} className={cls} onClick={() => handleSelect(idx)}>
                <span className="option-letter">{String.fromCharCode(65 + idx)}</span>
                {opt}
              </button>
            );
          })}
        </div>

        {selected !== null && (
          <div className={`explanation ${selected === question.correctIndex ? 'correct-exp' : 'wrong-exp'}`}>
            <strong>{selected === question.correctIndex ? '✅ Correct!' : '❌ Incorrect'}</strong>
            <p>{question.explanation}</p>
          </div>
        )}

        {selected !== null && (
          <button className="next-btn" onClick={handleNext}>
            {current + 1 >= questions.length ? 'See Results' : 'Next Question →'}
          </button>
        )}
      </div>
    </div>
  );
}
