import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getQuiz } from '../api/api';

const LETTERS = ['A','B','C','D'];

export default function QuizPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [quiz, setQuiz] = useState(null);
  const [loading, setLoading] = useState(true);
  const [qIdx, setQIdx] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [score, setScore] = useState(0);
  const [done, setDone] = useState(false);

  useEffect(() => {
    getQuiz(id).then(setQuiz).catch(() => {}).finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;
  if (!quiz) return <div className="page-center error-msg">Quiz not found.</div>;

  const questions = quiz.questions;
  const q = questions[qIdx];

  if (done) {
    const pct = Math.round((score / questions.length) * 100);
    const emoji = pct >= 80 ? '🎉' : pct >= 50 ? '👍' : '📚';
    return (

      <div className="page quiz-page">
        <div className="nav-row">
          <button className="home-btn" onClick={() => navigate('/')}>🏠 Home</button>
          <button className="back-btn" onClick={() => navigate(`/lesson/${id}`)}>← Back to Lesson</button>
        </div>
        <div className="quiz-result">
          <div className="result-emoji">{emoji}</div>
          <h2>{pct >= 80 ? 'Excellent!' : pct >= 50 ? 'Good job!' : 'Keep studying!'}</h2>
          <p className="result-score">You scored {score} out of {questions.length}</p>
          <div className="result-bar"><div className="result-fill" style={{ width: `${pct}%` }} /></div>
          <div className="result-pct">{pct}%</div>
          <div className="result-actions">
            <button className="action-btn quiz-btn" onClick={() => { setQIdx(0); setChosen(null); setScore(0); setDone(false); }}>Retry Quiz</button>
            <button className="action-btn game-btn" onClick={() => navigate(`/lesson/${id}`)}>Back to Lesson</button>
          </div>
        </div>
      </div>
    );
  }

  const pick = (i) => {
    if (chosen !== null) return;
    setChosen(i);
    if (i === q.correctIndex) setScore(s => s + 1);
  };

  const next = () => {
    if (qIdx + 1 >= questions.length) { setDone(true); return; }
    setQIdx(i => i + 1); setChosen(null);
  };

  return (
    <div className="page quiz-page">
      <div className="quiz-header">
        <div className="nav-row" style={{ marginBottom: 0 }}>
          <button className="home-btn" onClick={() => navigate('/')}>🏠 Home</button>
          <button className="back-btn" onClick={() => navigate(`/lesson/${id}`)}>← Back</button>
        </div>
        <span className="quiz-counter">Question {qIdx + 1} of {questions.length}</span>
      </div>
      <div className="progress-bar">
        <div className="progress-fill" style={{ width: `${((qIdx) / questions.length) * 100}%` }} />
      </div>
      <div className="question-card">
        <p className="question-text">{q.text}</p>
        <div className="options-grid">
          {q.options.map((opt, i) => {
            let cls = 'option-btn';
            if (chosen !== null) {
              if (i === q.correctIndex) cls += ' correct';
              else if (i === chosen) cls += ' wrong';
            }
            return (
              <button key={i} className={cls} onClick={() => pick(i)} disabled={chosen !== null}>
                <span className="option-letter">{LETTERS[i]}</span>
                {opt}
              </button>
            );
          })}
        </div>
        {chosen !== null && (
          <div className={`explanation ${chosen === q.correctIndex ? 'correct-exp' : 'wrong-exp'}`}>
            <strong>{chosen === q.correctIndex ? '✓ Correct!' : '✗ Incorrect'}</strong>
            {q.explanation && <p>{q.explanation}</p>}
          </div>
        )}
        {chosen !== null && (
          <button className="next-btn" onClick={next}>
            {qIdx + 1 >= questions.length ? 'See Results' : 'Next Question →'}
          </button>
        )}
      </div>
    </div>
  );
}
