import { useState } from 'react';

const WORDS = [
  { word: 'LISTEN', hint: 'To hear attentively' },
  { word: 'SILENT', hint: 'Making no sound (same letters as LISTEN!)' },
  { word: 'PLANET', hint: 'Orbits a star' },
  { word: 'PLATES', hint: 'You eat from these' },
  { word: 'ENLIST', hint: 'To join the military' },
  { word: 'MASTER', hint: 'An expert' },
  { word: 'STREAM', hint: 'A small river' },
  { word: 'TAMERS', hint: 'Animal trainers (plural)' },
];

function scramble(w) {
  let s = w.split('');
  do { s.sort(() => Math.random() - 0.5); } while (s.join('') === w);
  return s.join('');
}

export default function Anagram() {
  const [idx, setIdx] = useState(0);
  const [score, setScore] = useState(0);
  const [input, setInput] = useState('');
  const [result, setResult] = useState(null);
  const [scrambled] = useState(() => WORDS.map(w => scramble(w.word)));
  const [done, setDone] = useState(false);

  const check = () => {
    const correct = input.toUpperCase().trim() === WORDS[idx].word;
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
      <div className="game-score-bar"><span>Word {idx+1}/{WORDS.length}</span><span>Score: {score}</span></div>
      <div style={{ background: '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '2rem', textAlign: 'center', marginBottom: '1rem' }}>
        <div style={{ fontSize: '0.78rem', color: '#6b6b6b', marginBottom: '0.5rem', fontWeight: 600 }}>UNSCRAMBLE</div>
        <div style={{ display: 'flex', gap: '0.5rem', justifyContent: 'center', flexWrap: 'wrap' }}>
          {scrambled[idx].split('').map((l, i) => <span key={i} style={{ display: 'inline-flex', alignItems: 'center', justifyContent: 'center', width: '2.5rem', height: '2.5rem', border: '2px solid #0a0a0a', borderRadius: '8px', fontWeight: 800, fontSize: '1.2rem' }}>{l}</span>)}
        </div>
        <div style={{ marginTop: '1rem', fontSize: '0.85rem', color: '#6b6b6b' }}>Hint: {WORDS[idx].hint}</div>
      </div>
      {result === null ? (
        <div style={{ display: 'flex', gap: '0.75rem' }}>
          <input value={input} onChange={e => setInput(e.target.value)} onKeyDown={e => e.key === 'Enter' && input && check()} placeholder="Type the word..." style={{ flex: 1, padding: '0.85rem 1rem', border: '1px solid #e0e0e0', borderRadius: '10px', fontSize: '1rem', fontFamily: 'inherit', outline: 'none', textTransform: 'uppercase' }} autoFocus />
          <button className="play-btn" disabled={!input} onClick={check}>Check</button>
        </div>
      ) : (
        <div>
          <div style={{ padding: '1rem 1.25rem', borderRadius: '10px', background: result ? '#f0fdf4' : '#fef2f2', borderLeft: `4px solid ${result ? '#16a34a' : '#dc2626'}`, color: result ? '#166534' : '#991b1b', marginBottom: '1rem' }}>
            {result ? '✓ Correct!' : `✗ The answer was: ${WORDS[idx].word}`}
          </div>
          <button className="next-btn" onClick={next}>{idx+1>=WORDS.length?'See Results':'Next →'}</button>
        </div>
      )}
    </div>
  );
}
