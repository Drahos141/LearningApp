import { useState, useMemo } from 'react';

const D = { bg: '#001a0d', card: '#002211', border: '#003322', text: '#ccffe8', muted: '#559966', correct: { bg: '#003311', border: '#00cc55', text: '#00ee66' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

const QUESTIONS = [
  { q: 'A train travels 120 km in 2 hours. What is its speed in km/h?', answer: 60, options: [40,50,60,80], exp: 'Speed = Distance ÷ Time = 120 ÷ 2 = 60 km/h.' },
  { q: 'If 5 workers build a wall in 10 days, how many days for 10 workers?', answer: 5, options: [2,5,8,20], exp: '5 workers × 10 days = 50 worker-days. 10 workers → 50 ÷ 10 = 5 days.' },
  { q: 'A shirt costs $80, discounted by 25%. What is the sale price?', answer: 60, options: [55,60,65,70], exp: '25% of $80 = $20. $80 − $20 = $60.' },
  { q: 'What percentage of 200 is 50?', answer: 25, options: [10,20,25,40], exp: '50 ÷ 200 × 100 = 25%.' },
  { q: 'A car depreciates 20% per year. Worth $10,000 now. Value after 2 years?', answer: 6400, options: [6000,6400,7000,8000], exp: 'Year 1: 10000 × 0.8 = 8000. Year 2: 8000 × 0.8 = 6400.' },
  { q: 'A rectangle is 8m × 2m. A square has the same area (16m²). What is the square\'s side length?', answer: 4, options: [3,4,5,8], exp: 'Area = 8×2 = 16m². Side of square = √16 = 4m (exact whole number).' },
  { q: 'If A > B and B > C, and C = 5, which could be a valid set of values?', answer: 'A=9, B=7, C=5', options: ['A=3, B=7, C=5','A=9, B=4, C=5','A=9, B=7, C=5','A=5, B=7, C=9'], exp: 'We need A>B>C=5. A=9>B=7>C=5 is valid.' },
  { q: 'A clock shows 3:00. What is the angle between the hands?', answer: 90, options: [45,60,90,120], exp: 'At 3:00, the minute hand is at 12 and hour hand at 3 — exactly one quarter of 360° = 90°.' },
  { q: 'A shop sells 60 items at $3 profit each. How much total profit to sell 75% of items?', answer: 135, options: [120,135,150,180], exp: '75% of 60 = 45 items. 45 × $3 = $135 profit.' },
  { q: 'What is 15% of 240?', answer: 36, options: [24,30,36,40], exp: '10% of 240 = 24. 5% = 12. 15% = 24 + 12 = 36.' },
];

export default function QuantitativeReasoning() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen !== null) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 16, padding: '2.5rem', textAlign: 'center', color: D.text }}>
      <div style={{ fontSize: '3rem' }}>📊</div>
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem' }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Quantitative reasoning done!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#00cc55', color: '#001a00', border: 'none' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem' }}>
        <span>Q {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span>
      </div>
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 12, padding: '1.75rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '1rem' }}>QUANTITATIVE REASONING</div>
        <p style={{ fontWeight: 600, fontSize: '1.05rem', lineHeight: 1.7 }}>{q.q}</p>
      </div>
      <div style={{ display: 'flex', flexDirection: 'column', gap: '0.6rem' }}>
        {q.options.map(o => {
          let bg = D.card, border = D.border, color = D.text;
          if (chosen !== null) {
            if (o === q.answer) { bg = D.correct.bg; border = D.correct.border; color = D.correct.text; }
            else if (o === chosen) { bg = D.wrong.bg; border = D.wrong.border; color = D.wrong.text; }
          }
          return <button key={String(o)} onClick={() => pick(o)} style={{ padding: '0.9rem 1.25rem', background: bg, border: `1px solid ${border}`, borderRadius: 10, color, fontWeight: 700, fontSize: '1rem', cursor: 'pointer', fontFamily: 'inherit', textAlign: 'left' }}>{o}</button>;
        })}
      </div>
      {chosen !== null && <>
        <div style={{ marginTop: '1rem', padding: '0.9rem 1.1rem', borderRadius: 10, background: chosen === q.answer ? D.correct.bg : D.wrong.bg, borderLeft: `4px solid ${chosen === q.answer ? D.correct.border : D.wrong.border}`, color: chosen === q.answer ? D.correct.text : D.wrong.text, fontSize: '0.9rem', lineHeight: 1.6 }}>
          <strong>{chosen === q.answer ? '✓ Correct!' : `✗ Answer: ${q.answer}.`}</strong> {q.exp}
        </div>
        <button className="next-btn" style={{ marginTop: '1rem' }} onClick={next}>{idx + 1 >= QUESTIONS.length ? 'See Results' : 'Next →'}</button>
      </>}
    </div>
  );
}
