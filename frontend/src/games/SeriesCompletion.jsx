import { useState, useMemo } from 'react';

const D = { bg: '#0d0d1a', card: '#13132a', border: '#2a2a4a', text: '#e8e8ff', muted: '#6666aa', correct: { bg: '#0a1a2e', border: '#4488ff', text: '#66aaff' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

const QUESTIONS = [
  { seq: [2, 6, 12, 20, 30, null], answer: 42, exp: 'Differences: +4,+6,+8,+10,+12. Add 12 to 30.' },
  { seq: [1, 3, 7, 13, 21, null], answer: 31, exp: 'Differences: +2,+4,+6,+8,+10. Add 10 to 21.' },
  { seq: [0, 1, 4, 9, 16, 25, null], answer: 36, exp: 'Perfect squares: 0²,1²,2²,3²,4²,5²,6²=36.' },
  { seq: [1, 2, 6, 24, null], answer: 120, exp: 'Factorials: 1!,2!,3!,4!,5!=120.' },
  { seq: [5, 11, 23, 47, null], answer: 95, exp: 'Each term = previous × 2 + 1. So 47×2+1=95.' },
  { seq: [1, 5, 14, 30, null], answer: 55, exp: 'Tetrahedral numbers: differences are triangular (1,4,9,16,25). 30+25=55.' },
  { seq: [2, 5, 11, 23, 47, null], answer: 95, exp: 'Multiply by 2 then add 1 each step: 47×2+1=95.' },
  { seq: [3, 8, 15, 24, 35, null], answer: 48, exp: 'n²+2n: 1²+2, 2²+4, 3²+6… or differences: +5,+7,+9,+11,+13. 35+13=48.' },
  { seq: [1, 4, 10, 20, 35, null], answer: 56, exp: 'Differences: 3,6,10,15,21 (triangular numbers). 35+21=56.' },
  { seq: [2, 4, 8, 14, 22, null], answer: 32, exp: 'Differences: +2,+4,+6,+8,+10. 22+10=32.' },
];

function makeOpts(answer) {
  const set = new Set([answer]);
  [answer-1,answer+1,answer-2,answer+2,answer-5,answer+5,answer-10,answer+10,answer-12,answer+12]
    .filter(v=>v>0 && !set.has(v)).slice(0,5).forEach(v=>set.size<4&&set.add(v));
  return [...set].sort(()=>Math.random()-0.5);
}

export default function SeriesCompletion() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const opts = useMemo(() => QUESTIONS.map(q => makeOpts(q.answer)), []);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen !== null) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 16, padding: '2.5rem', textAlign: 'center', color: D.text }}>
      <div style={{ fontSize: '3rem' }}>📈</div>
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem' }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Advanced series reasoning!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#4488ff', color: '#fff', border: 'none' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem' }}>
        <span>Q {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span>
      </div>
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 12, padding: '1.75rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '1rem' }}>WHAT COMES NEXT IN THE SERIES?</div>
        <div style={{ display: 'flex', gap: '0.6rem', alignItems: 'center', flexWrap: 'wrap' }}>
          {q.seq.map((n, i) => (
            <span key={i} style={{ minWidth: '3.5rem', textAlign: 'center', padding: '0.6rem', borderRadius: 8, fontSize: '1.4rem', fontWeight: 800,
              background: n === null ? '#1a1a3e' : '#1e1e3a', border: `2px solid ${n === null ? '#4488ff' : D.border}`,
              color: n === null ? '#4488ff' : D.text }}>
              {n === null ? '?' : n}
            </span>
          ))}
        </div>
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {opts[idx].map(v => {
          let bg = D.card, border = D.border, color = D.text;
          if (chosen !== null) {
            if (v === q.answer) { bg = D.correct.bg; border = D.correct.border; color = D.correct.text; }
            else if (v === chosen) { bg = D.wrong.bg; border = D.wrong.border; color = D.wrong.text; }
          }
          return <button key={v} onClick={() => pick(v)} style={{ padding: '1rem', background: bg, border: `1px solid ${border}`, borderRadius: 10, color, fontWeight: 800, fontSize: '1.2rem', cursor: 'pointer', fontFamily: 'inherit' }}>{v}</button>;
        })}
      </div>
      {chosen !== null && <>
        <div style={{ marginTop: '1rem', padding: '0.9rem 1.1rem', borderRadius: 10, background: chosen === q.answer ? D.correct.bg : D.wrong.bg, borderLeft: `4px solid ${chosen === q.answer ? D.correct.border : D.wrong.border}`, color: chosen === q.answer ? D.correct.text : D.wrong.text, fontSize: '0.9rem' }}>
          <strong>{chosen === q.answer ? '✓ Correct!' : `✗ Answer: ${q.answer}.`}</strong> {q.exp}
        </div>
        <button className="next-btn" style={{ marginTop: '1rem' }} onClick={next}>{idx + 1 >= QUESTIONS.length ? 'See Results' : 'Next →'}</button>
      </>}
    </div>
  );
}
