import { useState, useEffect, useRef } from 'react';

const N = 2;
const CELLS = 9;
const INTERVAL = 2000;

export default function NBack() {
  const [started, setStarted] = useState(false);
  const [history, setHistory] = useState([]);
  const [current, setCurrent] = useState(null);
  const [hits, setHits] = useState(0);
  const [misses, setMisses] = useState(0);
  const [falseAlarms, setFalseAlarms] = useState(0);
  const [rounds, setRounds] = useState(0);
  const [done, setDone] = useState(false);
  const [matched, setMatched] = useState(false);
  const total = 20;
  const timerRef = useRef();
  const histRef = useRef([]);
  const roundRef = useRef(0);

  const step = () => {
    const pos = Math.floor(Math.random() * CELLS);
    setCurrent(pos);
    setMatched(false);
    histRef.current = [...histRef.current, pos];
    setHistory(h => [...h, pos]);
    roundRef.current++;
    setRounds(r => r + 1);
    if (roundRef.current >= total) {
      setTimeout(() => setDone(true), INTERVAL);
    }
  };

  const start = () => {
    setStarted(true); setHits(0); setMisses(0); setFalseAlarms(0); setRounds(0); setDone(false);
    histRef.current = []; roundRef.current = 0;
    step();
    timerRef.current = setInterval(() => {
      if (roundRef.current >= total) { clearInterval(timerRef.current); return; }
      step();
    }, INTERVAL);
  };

  const pressMatch = () => {
    const h = histRef.current;
    if (h.length >= N + 1 && h[h.length - 1] === h[h.length - 1 - N]) {
      setHits(x => x + 1); setMatched(true);
    } else {
      setFalseAlarms(x => x + 1);
    }
  };

  useEffect(() => () => clearInterval(timerRef.current), []);

  if (!started) return (
    <div style={{ textAlign: 'center' }}>
      <p style={{ marginBottom: '1.5rem', color: '#6b6b6b' }}>A cell lights up every 2 seconds. Press MATCH if it's the same position as {N} steps ago.</p>
      <button className="play-btn" onClick={start}>Start N-Back (N={N})</button>
    </div>
  );

  if (done) return (
    <div className="game-result">
      <h2>N-Back Complete</h2>
      <p>Hits: {hits} | Misses: {misses} | False Alarms: {falseAlarms}</p>
      <button className="play-btn" style={{ marginTop: '1.5rem' }} onClick={() => { setStarted(false); setDone(false); }}>Play Again</button>
    </div>
  );

  return (
    <div>
      <div className="game-score-bar"><span>Round {rounds}/{total}</span><span>Hits: {hits} | FA: {falseAlarms}</span></div>
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3,1fr)', gap: '0.5rem', maxWidth: '240px', margin: '0 auto 2rem' }}>
        {Array.from({ length: CELLS }, (_, i) => (
          <div key={i} style={{ height: '70px', borderRadius: '10px', border: '1px solid #e0e0e0', background: current === i ? '#0a0a0a' : '#f8f8f8', transition: 'background 0.15s' }} />
        ))}
      </div>
      <button className="play-btn" style={{ width: '100%', background: matched ? '#16a34a' : undefined }} onClick={pressMatch}>MATCH!</button>
    </div>
  );
}
