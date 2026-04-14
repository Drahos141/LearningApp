import { useState } from 'react';

const D = { bg: '#1a0d1a', card: '#24112a', border: '#3a1a4a', text: '#f0e8ff', muted: '#9966cc', correct: { bg: '#1a0a2e', border: '#aa44ff', text: '#cc77ff' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

// Each puzzle: 3x3 grid (9 cells, last is null). Row and column each follow a numeric rule.
const QUESTIONS = [
  { grid: [1,2,3, 2,4,6, 3,6,null], answer: 9, options: [7,8,9,12],
    rule: 'Each row/column: row×col (row 3, col 3 = 9).' },
  { grid: [1,2,3, 4,5,6, 7,8,null], answer: 9, options: [9,10,11,12],
    rule: 'Sequential numbers 1–9 in order.' },
  { grid: [2,4,8, 3,6,12, 4,8,null], answer: 16, options: [14,16,18,20],
    rule: 'Each row doubles: col1, col1×2, col1×4. Row 3: 4,8,16.' },
  { grid: [10,9,8, 7,6,5, 4,3,null], answer: 2, options: [1,2,3,4],
    rule: 'Numbers count down: 10,9,8,7,6,5,4,3,2.' },
  { grid: [1,1,2, 1,2,3, 2,3,null], answer: 5, options: [4,5,6,7],
    rule: 'Each cell = sum of cell above + cell to left (Pascal-like). 2+3=5.' },
  { grid: [4,2,2, 9,3,3, 16,4,null], answer: 4, options: [3,4,5,6],
    rule: 'Col1 is perfect squares. Col2 is their square roots. Col3 = Col2. √16=4.' },
  { grid: [3,6,9, 12,15,18, 21,24,null], answer: 27, options: [25,26,27,28],
    rule: 'Multiples of 3: 3,6,9,12,15,18,21,24,27.' },
  { grid: [1,2,4, 3,6,12, 5,10,null], answer: 20, options: [15,18,20,22],
    rule: 'Each row: start, ×2, ×4. Row 3: 5,10,20.' },
];

const cellStyle = (v, isNull) => ({
  width: 68, height: 68, display: 'flex', alignItems: 'center', justifyContent: 'center',
  borderRadius: 8, fontWeight: 800, fontSize: isNull ? '1.6rem' : '1.3rem',
  background: isNull ? '#2a0a3a' : '#2a1a3a',
  border: `2px solid ${isNull ? '#aa44ff' : D.border}`,
  color: isNull ? '#aa44ff' : D.text,
});

export default function MissingPiece() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen !== null) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 16, padding: '2.5rem', textAlign: 'center', color: D.text }}>
      <div style={{ fontSize: '3rem' }}>🔮</div>
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem' }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Pattern matrix solved!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#aa44ff', color: '#fff', border: 'none' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem' }}>
        <span>Q {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span>
      </div>
      <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '1rem' }}>FIND THE MISSING NUMBER IN THE MATRIX</div>
      <div style={{ display: 'inline-grid', gridTemplateColumns: 'repeat(3,1fr)', gap: 6, background: D.border, padding: 6, borderRadius: 12, marginBottom: '1.5rem' }}>
        {q.grid.map((v, i) => (
          <div key={i} style={cellStyle(v, v === null)}>{v === null ? '?' : v}</div>
        ))}
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {q.options.map(o => {
          let bg = D.card, border = D.border, color = D.text;
          if (chosen !== null) {
            if (o === q.answer) { bg = D.correct.bg; border = D.correct.border; color = D.correct.text; }
            else if (o === chosen) { bg = D.wrong.bg; border = D.wrong.border; color = D.wrong.text; }
          }
          return <button key={o} onClick={() => pick(o)} style={{ padding: '1rem', background: bg, border: `1px solid ${border}`, borderRadius: 10, color, fontWeight: 800, fontSize: '1.3rem', cursor: 'pointer', fontFamily: 'inherit' }}>{o}</button>;
        })}
      </div>
      {chosen !== null && <>
        <div style={{ marginTop: '1rem', padding: '0.9rem 1.1rem', borderRadius: 10, background: chosen === q.answer ? D.correct.bg : D.wrong.bg, borderLeft: `4px solid ${chosen === q.answer ? D.correct.border : D.wrong.border}`, color: chosen === q.answer ? D.correct.text : D.wrong.text, fontSize: '0.9rem' }}>
          <strong>{chosen === q.answer ? '✓ Correct!' : `✗ Answer: ${q.answer}.`}</strong> {q.rule}
        </div>
        <button className="next-btn" style={{ marginTop: '1rem' }} onClick={next}>{idx + 1 >= QUESTIONS.length ? 'See Results' : 'Next →'}</button>
      </>}
    </div>
  );
}
