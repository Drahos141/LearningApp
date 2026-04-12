import { useState, useEffect, useRef } from 'react';

const COLORS = [
  { name: 'RED', hex: '#dc2626' },
  { name: 'BLUE', hex: '#2563eb' },
  { name: 'GREEN', hex: '#16a34a' },
  { name: 'YELLOW', hex: '#ca8a04' },
];

function makeQ() {
  const word = COLORS[Math.floor(Math.random() * COLORS.length)];
  let ink;
  do { ink = COLORS[Math.floor(Math.random() * COLORS.length)]; } while (ink === word);
  return { word, ink };
}

export default function StroopTest() {
  const [started, setStarted] = useState(false);
  const [time, setTime] = useState(30);
  const [q, setQ] = useState(makeQ);
  const [score, setScore] = useState(0);
  const [wrong, setWrong] = useState(0);
  const [flash, setFlash] = useState(null);
  const [done, setDone] = useState(false);

  useEffect(() => {
    if (!started || done) return;
    const t = setInterval(() => setTime(t => { if (t <= 1) { setDone(true); return 0; } return t-1; }), 1000);
    return () => clearInterval(t);
  }, [started, done]);

  const pick = (c) => {
    if (!started || done) return;
    if (c.name === q.ink.name) { setScore(s => s+1); setFlash('correct'); }
    else { setWrong(w => w+1); setFlash('wrong'); }
    setTimeout(() => setFlash(null), 200);
    setQ(makeQ());
  };

  if (!started) return (
    <div style={{ textAlign: 'center' }}>
      <p style={{ marginBottom: '1rem', color: '#6b6b6b' }}>A color name will appear in a different ink color. Click the button matching the <strong>INK COLOR</strong>, not the word!</p>
      <button className="play-btn" onClick={() => setStarted(true)}>Start!</button>
    </div>
  );

  if (done) return (
    <div className="game-result">
      <h2>Score: {score}</h2>
      <p>Correct: {score} | Wrong: {wrong}</p>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setScore(0);setWrong(0);setTime(30);setDone(false);setStarted(false);setQ(makeQ()); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>⏱ {time}s</span><span>✓ {score} ✗ {wrong}</span></div>
      <div style={{ background: flash === 'correct' ? '#f0fdf4' : flash === 'wrong' ? '#fef2f2' : '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '3rem', textAlign: 'center', marginBottom: '1.5rem', transition: 'background 0.15s' }}>
        <div style={{ fontSize: '3rem', fontWeight: 900, color: q.ink.hex, letterSpacing: '0.05em' }}>{q.word.name}</div>
        <div style={{ fontSize: '0.78rem', color: '#6b6b6b', marginTop: '0.75rem' }}>Click the INK COLOR button</div>
      </div>
      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '0.75rem' }}>
        {COLORS.map(c => (
          <button key={c.name} onClick={() => pick(c)} style={{ padding: '1rem', border: '2px solid #e0e0e0', borderRadius: '10px', background: c.hex, color: '#fff', fontWeight: 800, fontSize: '1rem', letterSpacing: '0.05em' }}>{c.name}</button>
        ))}
      </div>
    </div>
  );
}
