import { useState } from 'react';

const QUESTIONS = [
  { seq: [2, 4, null, 8, 10], answer: 6 },
  { seq: [1, 3, 6, 10, null], answer: 15 },
  { seq: [5, 10, 15, null, 25], answer: 20 },
  { seq: [1, 4, 9, 16, null], answer: 25 },
  { seq: [2, 6, 18, null, 162], answer: 54 },
  { seq: [100, 90, 80, null, 60], answer: 70 },
  { seq: [3, 6, 12, null, 48], answer: 24 },
  { seq: [1, 1, 2, 3, 5, null], answer: 8 },
  { seq: [64, 32, 16, null, 4], answer: 8 },
  { seq: [7, 14, 21, null, 35], answer: 28 },
];

function makeOptions(answer) {
  const opts = [answer];
  while (opts.length < 4) {
    const v = answer + (Math.floor(Math.random() * 20) - 10);
    if (!opts.includes(v) && v > 0) opts.push(v);
  }
  return opts.sort(() => Math.random() - 0.5);
}

export default function NumberSequence() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const [opts] = useState(() => QUESTIONS.map(q => makeOptions(q.answer)));

  const q = QUESTIONS[idx];

  const pick = (v) => {
    if (chosen !== null) return;
    setChosen(v);
    if (v === q.answer) setScore(s => s + 1);
  };

  const next = () => {
    if (idx + 1 >= QUESTIONS.length) { setDone(true); return; }
    setIdx(i => i + 1);
    setChosen(null);
  };

  if (done) return (
    <div className="game-result">
      <div style={{ fontSize: '3rem' }}>🔢</div>
      <h2>Score: {score} / {QUESTIONS.length}</h2>
      <p>You found {score} missing numbers correctly!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>Question {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span></div>
      <div style={{ background: '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '2rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.8rem', color: '#6b6b6b', marginBottom: '0.75rem', fontWeight: 600 }}>FIND THE MISSING NUMBER</div>
        <div style={{ display: 'flex', gap: '1rem', alignItems: 'center', flexWrap: 'wrap', fontSize: '1.5rem', fontWeight: 700 }}>
          {q.seq.map((n, i) => <span key={i} style={{ minWidth: '2.5rem', textAlign: 'center', padding: '0.5rem', border: '2px solid', borderColor: n === null ? '#0a0a0a' : '#e0e0e0', borderRadius: '8px', background: n === null ? '#0a0a0a' : '#fff', color: n === null ? '#fff' : '#0a0a0a' }}>{n === null ? '?' : n}</span>)}
        </div>
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {opts[idx].map(v => {
          let bg = '#fff', borderColor = '#e0e0e0', color = '#0a0a0a';
          if (chosen !== null) {
            if (v === q.answer) { bg = '#f0fdf4'; borderColor = '#16a34a'; color = '#166534'; }
            else if (v === chosen) { bg = '#fef2f2'; borderColor = '#dc2626'; color = '#991b1b'; }
          }
          return (
            <button key={v} onClick={() => pick(v)} style={{ padding: '1rem', border: `1px solid ${borderColor}`, borderRadius: '10px', background: bg, color, fontSize: '1.2rem', fontWeight: 700 }}>{v}</button>
          );
        })}
      </div>
      {chosen !== null && <button className="next-btn" style={{ marginTop: '1.5rem' }} onClick={next}>{idx + 1 >= QUESTIONS.length ? 'See Results' : 'Next →'}</button>}
    </div>
  );
}
