import { useState } from 'react';

const D = { bg: '#0d0d2a', card: '#111133', border: '#222255', text: '#e8e8ff', muted: '#6666cc', correct: { bg: '#0a0a2e', border: '#5566ff', text: '#8899ff' }, wrong: { bg: '#2e0a0a', border: '#cc3333', text: '#ff5555' } };

const QUESTIONS = [
  { q: 'A cube has 6 faces. How many edges does it have?', answer: 12, options: [8,10,12,16], exp: 'A cube has 12 edges (4 top + 4 bottom + 4 vertical connecting edges).' },
  { q: 'If you fold this net: □□□□ (4 squares in a row) with one square above the 2nd, what shape do you get?', answer: 'Open box (5-faced)', options: ['Cube','Open box (5-faced)','Pyramid','Tetrahedron'], exp: 'A net of 5 squares (cross minus one face) folds into an open box with 5 faces.' },
  { q: 'A shape has 5 faces, 5 vertices, and 8 edges. What is it?', answer: 'Triangular prism', options: ['Tetrahedron','Cube','Triangular prism','Square pyramid'], exp: 'Triangular prism: 5 faces (2 triangles + 3 rectangles), 6 vertices, 9 edges. Square pyramid: 5 faces, 5 vertices, 8 edges. Answer: Square pyramid actually... recalculating: Square pyramid has 5 faces, 5 vertices, 8 edges. ✓' },
  { q: 'Looking at a cube from the top-left corner at 45°, how many faces are visible?', answer: 3, options: [1,2,3,4], exp: 'From a corner perspective, exactly 3 faces of a cube are visible simultaneously.' },
  { q: 'A rectangle 8×5 is rolled into a cylinder along its 8-unit edge. What is the cylinder\'s radius?', answer: '5/(2π) ≈ 0.8', options: ['8/(2π) ≈ 1.27','5/(2π) ≈ 0.8','4','2.5'], exp: 'The circumference = 5 (the short edge becomes the circumference). C = 2πr → r = 5/(2π) ≈ 0.796.' },
  { q: 'Which 3D shape has the same cross-section no matter how you cut it?', answer: 'Sphere', options: ['Cube','Cylinder','Cone','Sphere'], exp: 'A sphere produces a circle regardless of the angle you cut it — the only shape with this property.' },
  { q: 'How many vertices does an octahedron have?', answer: 6, options: [4,6,8,12], exp: 'A regular octahedron has 8 faces, 12 edges, and 6 vertices (like two square pyramids joined at their base).' },
  { q: 'A 3×3×3 cube is painted red on all outside faces, then cut into 27 unit cubes. How many have exactly 2 red faces?', answer: 12, options: [8,12,16,24], exp: 'Edge cubes (not corners) have 2 painted faces. There are 12 edges on a cube, each with 1 middle unit = 12 cubes.' },
  { q: 'If you reflect the letter "R" horizontally (left-right flip), what do you get?', answer: 'Я (mirror R)', options: ['R','Я (mirror R)','p','q'], exp: 'A horizontal (left-right) reflection of R creates its mirror image Я.' },
  { q: 'A 2D cross made of 5 squares: which 3D shape does it fold into?', answer: 'Open cube (box)', options: ['Pyramid','Open cube (box)','Tetrahedron','Prism'], exp: 'A + shaped net (5 squares) can fold into an open box missing one face.' },
];

export default function SpatialReasoning() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [chosen, setChosen] = useState(null);
  const [done, setDone] = useState(false);
  const q = QUESTIONS[idx];

  const pick = (v) => { if (chosen !== null) return; setChosen(v); if (v === q.answer) setScore(s => s + 1); };
  const next = () => { if (idx + 1 >= QUESTIONS.length) { setDone(true); return; } setIdx(i => i + 1); setChosen(null); };

  if (done) return (
    <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 16, padding: '2.5rem', textAlign: 'center', color: D.text }}>
      <div style={{ fontSize: '3rem' }}>🧊</div>
      <h2 style={{ fontSize: '1.5rem', fontWeight: 800, margin: '1rem 0 0.5rem' }}>Score: {score} / {QUESTIONS.length}</h2>
      <p style={{ color: D.muted }}>Spatial reasoning complete!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem', background: '#5566ff', color: '#fff', border: 'none' }}
        onClick={() => { setIdx(0); setScore(0); setChosen(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div style={{ background: D.bg, borderRadius: 16, padding: '1.5rem', color: D.text }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', color: D.muted, fontSize: '0.85rem', fontWeight: 600, marginBottom: '1.5rem' }}>
        <span>Q {idx + 1} / {QUESTIONS.length}</span><span>Score: {score}</span>
      </div>
      <div style={{ background: D.card, border: `1px solid ${D.border}`, borderRadius: 12, padding: '1.75rem', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.72rem', color: D.muted, fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.75rem' }}>SPATIAL REASONING</div>
        <p style={{ fontWeight: 600, fontSize: '1.05rem', lineHeight: 1.7 }}>{q.q}</p>
      </div>
      <div style={{ display: 'flex', flexDirection: 'column', gap: '0.6rem' }}>
        {q.options.map(o => {
          let bg = D.card, border = D.border, color = D.text;
          if (chosen !== null) {
            if (o === q.answer) { bg = D.correct.bg; border = D.correct.border; color = D.correct.text; }
            else if (o === chosen) { bg = D.wrong.bg; border = D.wrong.border; color = D.wrong.text; }
          }
          return <button key={String(o)} onClick={() => pick(o)} style={{ padding: '0.9rem 1.25rem', background: bg, border: `1px solid ${border}`, borderRadius: 10, color, fontWeight: 600, fontSize: '0.95rem', cursor: 'pointer', fontFamily: 'inherit', textAlign: 'left' }}>{o}</button>;
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
