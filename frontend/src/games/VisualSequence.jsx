import { useState } from 'react';

const D = { bg: '#1a001a', card: '#220022', border: '#3a0044', text: '#ffe8ff', muted: '#cc66cc', correct: { bg: '#1a002e', border: '#cc44ff', text: '#ee66ff' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

// Shape sequences with CSS symbols
const QUESTIONS = [
  { desc: 'Shapes: ▲ □ ○ ▲ □ ?', answer: '○', options: ['▲','□','○','◇'], exp: 'The sequence ▲ □ ○ repeats. After □ comes ○.' },
  { desc: 'Sizes: S M L S M ?', answer: 'L', options: ['S','M','L','XL'], exp: 'The cycle S→M→L repeats. After M comes L.' },
  { desc: 'Colors: ⚫ ⚪ ⚫ ⚪ ⚫ ?', answer: '⚪', options: ['⚫','⚪','🔴','🔵'], exp: 'Alternating black-white pattern. After ⚫ comes ⚪.' },
  { desc: 'Stars: ★ ★★ ★★★ ★ ★★ ?', answer: '★★★', options: ['★','★★','★★★','★★★★'], exp: 'Cycle of 1,2,3 stars repeats. After ★★ comes ★★★.' },
  { desc: 'Arrows: ↑ → ↓ ← ↑ → ?', answer: '↓', options: ['↑','→','↓','←'], exp: 'Rotating clockwise: up→right→down→left. After → comes ↓.' },
  { desc: 'Numbers in shape: □₂ □₄ □₈ □₁₆ ?', answer: '□₃₂', options: ['□₂₀','□₂₄','□₃₂','□₆₄'], exp: 'Numbers double: 2→4→8→16→32.' },
  { desc: 'Pentagon sides: △(3) □(4) ⬠(5) ⬡(6) ?', answer: '7-gon (heptagon)', options: ['○','6-gon','7-gon (heptagon)','8-gon'], exp: 'Shapes gain one side: 3→4→5→6→7 sides (heptagon).' },
  { desc: 'Dots pattern: · :: ::: ? (1,4,9,16)', answer: '25 dots', options: ['20 dots','25 dots','36 dots','16 dots'], exp: 'Perfect squares: 1,4,9,16,25.' },
  { desc: 'Shading: top ▣ → right ▦ → bottom ▩ → ?', answer: 'Left shaded ▧', options: ['Top shaded ▣','Right shaded ▦','Bottom shaded ▩','Left shaded ▧'], exp: 'Shading rotates clockwise around the square: top→right→bottom→left.' },
  { desc: 'Number of shapes: ◆ ◆◆◆ ◆◆◆◆◆◆ ?', answer: '◆◆◆◆◆◆◆◆◆◆ (10)', options: ['◆◆◆◆◆◆◆ (7)','◆◆◆◆◆◆◆◆◆◆ (10)','◆◆◆◆◆◆◆◆ (8)','◆◆◆◆◆◆◆◆◆ (9)'], exp: 'Odd numbers: 1, 3, 6... Actually differences: +2,+3,+4. So 6+4=10.' },
];

export default function VisualSequence() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 16, padding: '2.5rem', textAlign: 'center', color: D.text }}>
      <div style={{ fontSize: '3rem' }}>🔮</div>
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem' }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Visual sequence mastery!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#cc44ff', color: '#1a001a', border: 'none' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem' }}>
        <span>Q {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span>
      </div>
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 12, padding: '1.75rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.75rem' }}>WHAT COMES NEXT?</div>
        <p style={{ fontSize: '1.4rem', fontWeight: 700, letterSpacing: '0.05em', lineHeight: 1.8 }}>{q.desc}</p>
      </div>
      <div style={{ display: 'flex', flexDirection: 'column', gap: '0.6rem' }}>
        {q.options.map(o => {
          let bg = D.card, border = D.border, color = D.text;
          if (chosen) {
            if (o === q.answer) { bg = D.correct.bg; border = D.correct.border; color = D.correct.text; }
            else if (o === chosen) { bg = D.wrong.bg; border = D.wrong.border; color = D.wrong.text; }
          }
          return <button key={o} onClick={() => pick(o)} style={{ padding: '0.9rem 1.25rem', background: bg, border: `1px solid ${border}`, borderRadius: 10, color, fontWeight: 700, fontSize: '1.05rem', cursor: 'pointer', fontFamily: 'inherit', textAlign: 'left' }}>{o}</button>;
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
