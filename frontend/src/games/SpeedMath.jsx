import { useState, useEffect, useRef } from 'react';

function makeQ() {
  const a = Math.floor(Math.random()*20)+1;
  const b = Math.floor(Math.random()*20)+1;
  const op = Math.random() > 0.5 ? '+' : '-';
  const ans = op === '+' ? a+b : a-b;
  return { text: `${a} ${op} ${b} = ?`, answer: ans };
}

export default function SpeedMath() {
  const [started, setStarted] = useState(false);
  const [time, setTime] = useState(30);
  const [q, setQ] = useState(makeQ);
  const [input, setInput] = useState('');
  const [score, setScore] = useState(0);
  const [done, setDone] = useState(false);
  const ref = useRef();

  useEffect(() => {
    if (!started || done) return;
    const t = setInterval(() => setTime(t => { if (t<=1){setDone(true);return 0;} return t-1; }), 1000);
    return () => clearInterval(t);
  }, [started, done]);

  const submit = () => {
    if (parseInt(input) === q.answer) setScore(s => s+1);
    setQ(makeQ()); setInput('');
    ref.current?.focus();
  };

  if (!started) return (
    <div style={{ textAlign: 'center' }}>
      <div style={{ fontSize: '3rem', marginBottom: '1rem' }}>🏃</div>
      <p style={{ marginBottom: '1.5rem', color: '#6b6b6b' }}>Answer simple addition and subtraction problems as fast as possible! 30 seconds on the clock.</p>
      <button className="play-btn" onClick={() => setStarted(true)}>Start!</button>
    </div>
  );

  if (done) return (
    <div className="game-result">
      <div style={{ fontSize: '3rem' }}>🏃</div>
      <h2>Score: {score}</h2>
      <p>You solved {score} problems in 30 seconds!</p>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setScore(0);setTime(30);setDone(false);setStarted(false);setQ(makeQ());setInput(''); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>⏱ {time}s</span><span>Score: {score}</span></div>
      <div style={{ background: '#f8f8f8', border: '1px solid #e0e0e0', borderRadius: '12px', padding: '2.5rem', textAlign: 'center', marginBottom: '1.5rem' }}>
        <div style={{ fontSize: '2rem', fontWeight: 800 }}>{q.text}</div>
      </div>
      <div style={{ display: 'flex', gap: '0.75rem' }}>
        <input ref={ref} autoFocus value={input} onChange={e => setInput(e.target.value)} onKeyDown={e => e.key === 'Enter' && input && submit()} type="number" placeholder="Answer..." style={{ flex: 1, padding: '0.85rem 1rem', border: '1px solid #e0e0e0', borderRadius: '10px', fontSize: '1.2rem', fontFamily: 'inherit', outline: 'none' }} />
        <button className="play-btn" disabled={!input} onClick={submit}>→</button>
      </div>
    </div>
  );
}
