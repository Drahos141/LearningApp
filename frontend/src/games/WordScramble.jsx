import { useState } from 'react';

const WORDS = ['python','network','biology','history','algebra','science','grammar','culture','climate','quantum'];

function scramble(w) {
  let s = w.split('');
  do { s.sort(() => Math.random() - 0.5); } while (s.join('') === w);
  return s.join('').toUpperCase();
}

export default function WordScramble() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [input, setInput] = useState('');
  const [result, setResult] = useState(null);
  const [scrambled] = useState(() => WORDS.map(w => scramble(w)));
  const [done, setDone] = useState(false);

  const check = () => {
    const correct = input.toLowerCase().trim() === WORDS[idx];
    setResult(correct);
    if (correct) setScore(s => s + 1);
  };

  const next = () => {
    if (idx + 1 >= WORDS.length) { setDone(true); return; }
    setIdx(i => i + 1); setInput(''); setResult(null);
  };

  if (done) return (
    <div className="game-result">
      <div style={{ fontSize: '3rem' }}>🔤</div>
      <h2>Score: {score} / {WORDS.length}</h2>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setIdx(0); setScore(0); setInput(''); setResult(null); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>Word {idx + 1} / {WORDS.length}</span><span>Score: {score}</span></div>
      <div style={{ background: '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '2rem', textAlign: 'center', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '0.78rem', color: '#6b6b6b', marginBottom: '0.75rem', fontWeight: 600 }}>UNSCRAMBLE THIS WORD</div>
        <div style={{ fontSize: '2.2rem', fontWeight: 800, letterSpacing: '0.15em' }}>{scrambled[idx]}</div>
      </div>
      {result === null ? (
        <div style={{ display: 'flex', gap: '0.75rem' }}>
          <input value={input} onChange={e => setInput(e.target.value)} onKeyDown={e => e.key === 'Enter' && input && check()} placeholder="Type your answer..." style={{ flex: 1, padding: '0.85rem 1rem', border: '1px solid #e0e0e0', borderRadius: '10px', fontSize: '1rem', fontFamily: 'inherit', outline: 'none' }} autoFocus />
          <button className="play-btn" disabled={!input} onClick={check}>Check</button>
        </div>
      ) : (
        <div>
          <div style={{ padding: '1rem 1.25rem', borderRadius: '10px', background: result ? '#f0fdf4' : '#fef2f2', borderLeft: `4px solid ${result ? '#16a34a' : '#dc2626'}`, color: result ? '#166534' : '#991b1b', marginBottom: '1rem' }}>
            {result ? '✓ Correct!' : `✗ The answer was: ${WORDS[idx].toUpperCase()}`}
          </div>
          <button className="next-btn" onClick={next}>{idx + 1 >= WORDS.length ? 'See Results' : 'Next →'}</button>
        </div>
      )}
    </div>
  );
}
