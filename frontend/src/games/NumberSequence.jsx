import { useState, useMemo } from 'react';

const QUESTIONS = [
  { seq: [2, 5, 8, 11, null, 17], answer: 14,
    explanation: 'Add 3 each time: 2, 5, 8, 11, 14, 17 (+3 every step).' },
  { seq: [3, 6, 12, 24, null, 96], answer: 48,
    explanation: 'Multiply by 2 each time: 3×2=6, 6×2=12, 12×2=24, 24×2=48.' },
  { seq: [1, 4, 9, 16, 25, null], answer: 36,
    explanation: 'Perfect squares: 1²=1, 2²=4, 3²=9, 4²=16, 5²=25, 6²=36.' },
  { seq: [1, 1, 2, 3, 5, 8, null], answer: 13,
    explanation: 'Fibonacci: each number = sum of two before it. 5+8=13.' },
  { seq: [100, 95, 85, 70, null], answer: 50,
    explanation: 'Subtract 5, 10, 15, 20… (differences increase by 5). 70−20=50.' },
  { seq: [1, 8, 27, 64, null], answer: 125,
    explanation: 'Perfect cubes: 1³=1, 2³=8, 3³=27, 4³=64, 5³=125.' },
  { seq: [2, 3, 5, 7, 11, null], answer: 13,
    explanation: 'Prime numbers in order: 2, 3, 5, 7, 11, 13.' },
  { seq: [1, 2, 4, 7, 11, null], answer: 16,
    explanation: 'Differences increase by 1: +1, +2, +3, +4, +5. So 11+5=16.' },
  { seq: [3, 5, 9, 15, 23, null], answer: 33,
    explanation: 'Differences increase by 2: +2, +4, +6, +8, +10. So 23+10=33.' },
  { seq: [2, 6, 18, 54, null], answer: 162,
    explanation: 'Multiply by 3 each time: 2×3=6, 6×3=18, 18×3=54, 54×3=162.' },
];

function makeOptions(answer) {
  const set = new Set([answer]);
  const candidates = [
    answer + 1, answer - 1, answer + 2, answer - 2,
    answer + 3, answer - 3, answer + 5, answer - 5,
    answer + 7, answer - 7, answer + 10, answer - 10,
    Math.round(answer * 1.5), Math.round(answer * 0.5),
  ].filter(v => v > 0 && !set.has(v));
  const shuffled = candidates.sort(() => Math.random() - 0.5);
  for (const c of shuffled) {
    if (set.size >= 4) break;
    set.add(c);
  }
  return [...set].sort(() => Math.random() - 0.5);
}

export default function NumberSequence() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const opts = useMemo(() => QUESTIONS.map(q => makeOptions(q.answer)), []);

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
      <button className="play-btn" style={{ marginTop: '1.5rem' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>
        Play Again
      </button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar">
        <span>Question {idx + 1} / {QUESTIONS.length}</span>
        <span>Score: {score}</span>
      </div>
      <div style={{ background: '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '2rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.8rem', color: '#6b6b6b', marginBottom: '0.75rem', fontWeight: 600 }}>
          FIND THE MISSING NUMBER
        </div>
        <div style={{ display: 'flex', gap: '0.75rem', alignItems: 'center', flexWrap: 'wrap', fontSize: '1.5rem', fontWeight: 700 }}>
          {q.seq.map((n, i) => (
            <span key={i} style={{
              minWidth: '3rem', textAlign: 'center', padding: '0.5rem 0.75rem',
              border: '2px solid', borderRadius: '8px',
              borderColor: n === null ? '#0a0a0a' : '#e0e0e0',
              background: n === null ? '#0a0a0a' : '#fff',
              color: n === null ? '#fff' : '#0a0a0a'
            }}>
              {n === null ? '?' : n}
            </span>
          ))}
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
            <button key={v} onClick={() => pick(v)} disabled={chosen !== null}
              style={{ padding: '1rem', border: `1px solid ${borderColor}`, borderRadius: '10px', background: bg, color, fontSize: '1.2rem', fontWeight: 700 }}>
              {v}
            </button>
          );
        })}
      </div>
      {chosen !== null && (
        <div style={{ marginTop: '1rem', padding: '1rem', borderRadius: '10px',
          background: chosen === q.answer ? '#f0fdf4' : '#fef2f2',
          borderLeft: `4px solid ${chosen === q.answer ? '#16a34a' : '#dc2626'}`,
          color: chosen === q.answer ? '#166534' : '#991b1b', fontSize: '0.9rem' }}>
          <strong>{chosen === q.answer ? '✓ Correct!' : `✗ The answer is ${q.answer}.`}</strong>
          {' '}{q.explanation}
        </div>
      )}
      {chosen !== null && (
        <button className="next-btn" style={{ marginTop: '1rem' }} onClick={next}>
          {idx + 1 >= QUESTIONS.length ? 'See Results' : 'Next →'}
        </button>
      )}
    </div>
  );
}
