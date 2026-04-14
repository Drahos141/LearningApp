import { useState } from 'react';

const D = { bg: '#1a1000', card: '#231800', border: '#3a2a00', text: '#fff8e0', muted: '#aa8833', correct: { bg: '#1a1400', border: '#ffcc00', text: '#ffd700' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

// Each question: a 2x3 transformation rule shown via text description + pick the next
const QUESTIONS = [
  { rule: 'Shapes increase by one element each step', seq: ['●', '●●', '●●●'], q: 'What comes next?', answer: '●●●●', options: ['●●●', '●●●●', '●●●●●', '●●'], exp: 'Each step adds one ●. Three → Four ●s.' },
  { rule: 'The number inside doubles each step', seq: ['□₁', '□₂', '□₄'], q: 'What comes next?', answer: '□₈', options: ['□₆', '□₈', '□₁₀', '□₁₂'], exp: '1→2→4→8 (×2 each time).' },
  { rule: 'Shape rotates 90° clockwise each step', seq: ['→', '↓', '←'], q: 'What comes next?', answer: '↑', options: ['→', '↓', '←', '↑'], exp: 'Rotating 90° clockwise: right→down→left→up.' },
  { rule: 'Alternating between filled and empty', seq: ['■', '□', '■', '□'], q: 'What comes next?', answer: '■', options: ['□', '■', '▲', '○'], exp: 'Pattern alternates ■ □ ■ □ → ■.' },
  { rule: 'Each step adds a surrounding layer', seq: ['○', '◎', '⊙'], q: 'Which rule best describes the next step?', answer: 'Add another outer ring', options: ['Remove a ring','Add another outer ring','Double the size','Change shape'], exp: 'Each step adds an outer concentric ring.' },
  { rule: 'The shape gains one side per step', seq: ['▲ (3)', '■ (4)', '⬠ (5)'], q: 'What has 6 sides?', answer: 'Hexagon', options: ['Triangle','Square','Pentagon','Hexagon'], exp: '3 sides→4→5→6 (hexagon).' },
  { rule: 'Shading alternates: top, right, bottom, left', seq: ['top shaded', 'right shaded', 'bottom shaded'], q: 'What is shaded next?', answer: 'Left', options: ['Top','Right','Bottom','Left'], exp: 'Clockwise rotation of shading: top→right→bottom→left.' },
  { rule: 'Numbers follow: square, then cube', seq: ['4 (2²)', '8 (2³)', '9 (3²)', '27 (3³)'], q: 'What comes next?', answer: '16 (4²)', options: ['12','16 (4²)','64 (4³)','25'], exp: 'Pattern: n², n³ for each n. n=2: 4,8. n=3: 9,27. n=4: 16 next.' },
  { rule: 'Each step: dark becomes light, light becomes dark', seq: ['◼◻', '◻◼', '◼◻'], q: 'What comes next?', answer: '◻◼', options: ['◼◻','◻◼','◼◼','◻◻'], exp: 'Colors invert each step: dark-light→light-dark→dark-light→light-dark.' },
  { rule: 'The pattern reflects then shifts', seq: ['123', '321', '234'], q: 'What comes next?', answer: '432', options: ['234','342','432','243'], exp: 'Reflect: 123→321. Shift +1 each pair: 234→432 (reflected).' },
];

export default function AbstractReasoning() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 16, padding: '2.5rem', textAlign: 'center', color: D.text }}>
      <div style={{ fontSize: '3rem' }}>🌟</div>
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem' }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Abstract reasoning complete!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#ffd700', color: '#1a1000', border: 'none' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem' }}>
        <span>Q {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span>
      </div>
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 12, padding: '1.75rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.5rem' }}>RULE</div>
        <p style={{ color: '#ffcc55', fontStyle: 'italic', marginBottom: '1rem' }}>{q.rule}</p>
        <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.75rem' }}>SEQUENCE</div>
        <div style={{ display: 'flex', gap: '1rem', alignItems: 'center', flexWrap: 'wrap', fontSize: '1.4rem', fontWeight: 800 }}>
          {q.seq.map((s, i) => (
            <span key={i} style={{ padding: '0.5rem 0.9rem', background: '#2a1a00', border: `1px solid ${D.border}`, borderRadius: 8 }}>{s}</span>
          ))}
          <span style={{ padding: '0.5rem 0.9rem', background: '#2a1a00', border: '2px dashed #ffcc00', borderRadius: 8, color: '#ffcc00' }}>?</span>
        </div>
        <p style={{ marginTop: '1rem', fontWeight: 700, color: D.text }}>{q.q}</p>
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {q.options.map(o => {
          let bg = D.card, border = D.border, color = D.text;
          if (chosen) {
            if (o === q.answer) { bg = D.correct.bg; border = D.correct.border; color = D.correct.text; }
            else if (o === chosen) { bg = D.wrong.bg; border = D.wrong.border; color = D.wrong.text; }
          }
          return <button key={o} onClick={() => pick(o)} style={{ padding: '0.9rem', background: bg, border: `1px solid ${border}`, borderRadius: 10, color, fontWeight: 700, fontSize: '1rem', cursor: 'pointer', fontFamily: 'inherit' }}>{o}</button>;
        })}
      </div>
      {chosen && <>
        <div style={{ marginTop: '1rem', padding: '0.9rem 1.1rem', borderRadius: 10, background: chosen === q.answer ? D.correct.bg : D.wrong.bg, borderLeft: `4px solid ${chosen === q.answer ? D.correct.border : D.wrong.border}`, color: chosen === q.answer ? D.correct.text : D.wrong.text, fontSize: '0.9rem' }}>
          <strong>{chosen === q.answer ? '✓ Correct!' : `✗ Answer: ${q.answer}.`}</strong> {q.exp}
        </div>
        <button className="next-btn" style={{ marginTop: '1rem' }} onClick={next}>{idx + 1 >= QUESTIONS.length ? 'See Results' : 'Next →'}</button>
      </>}
    </div>
  );
}
