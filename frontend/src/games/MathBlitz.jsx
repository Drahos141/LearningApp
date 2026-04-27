import { useState, useEffect, useRef, useCallback } from 'react';

const TIME_PER_Q = 8; // seconds per question
const QUESTIONS_PER_PLAYER = 6;

const PLAYER_COLORS = ['#4488ff', '#ff6644', '#44cc88', '#cc44ff'];
const PLAYER_BG    = ['#0a1a3a', '#2e1208', '#0a2a1a', '#1e0a30'];

// Generate a challenging question suitable for adults
function makeQuestion() {
  const type = Math.floor(Math.random() * 6);
  switch (type) {
    case 0: { // 3-digit + 3-digit
      const a = Math.floor(Math.random() * 900) + 100;
      const b = Math.floor(Math.random() * 900) + 100;
      return { text: `${a} + ${b}`, answer: a + b };
    }
    case 1: { // 3-digit - 2-digit (always positive)
      const a = Math.floor(Math.random() * 500) + 200;
      const b = Math.floor(Math.random() * 100) + 50;
      return { text: `${a} − ${b}`, answer: a - b };
    }
    case 2: { // 2-digit × single digit
      const a = Math.floor(Math.random() * 90) + 10;
      const b = Math.floor(Math.random() * 9) + 2;
      return { text: `${a} × ${b}`, answer: a * b };
    }
    case 3: { // percentage: X% of Y
      const pcts = [10, 15, 20, 25, 30, 40, 50];
      const p = pcts[Math.floor(Math.random() * pcts.length)];
      const n = (Math.floor(Math.random() * 18) + 2) * 10; // multiples of 10
      return { text: `${p}% of ${n}`, answer: Math.round((p / 100) * n) };
    }
    case 4: { // order of operations: a + b × c
      const a = Math.floor(Math.random() * 30) + 5;
      const b = Math.floor(Math.random() * 10) + 2;
      const c = Math.floor(Math.random() * 10) + 2;
      return { text: `${a} + ${b} × ${c}`, answer: a + b * c };
    }
    case 5: { // division: a × b ÷ b (clean)
      const b = Math.floor(Math.random() * 9) + 2;
      const a = (Math.floor(Math.random() * 20) + 5) * b;
      return { text: `${a} ÷ ${b}`, answer: a / b };
    }
    default: {
      const a = Math.floor(Math.random() * 200) + 50;
      const b = Math.floor(Math.random() * 200) + 50;
      return { text: `${a} + ${b}`, answer: a + b };
    }
  }
}

function generatePlayerQuestions() {
  return Array.from({ length: QUESTIONS_PER_PLAYER }, makeQuestion);
}

// ─── Setup Screen ─────────────────────────────────────────────────────────────
function Setup({ onStart }) {
  const [count, setCount] = useState(2);
  const [names, setNames] = useState(['Player 1', 'Player 2', 'Player 3', 'Player 4']);

  const updateName = (i, v) => setNames(n => { const c = [...n]; c[i] = v; return c; });

  return (
    <div style={{ textAlign: 'center', maxWidth: 440, margin: '0 auto', padding: '0.5rem' }}>
      <div style={{ fontSize: '3rem', marginBottom: '0.5rem' }}>⚡</div>
      <h2 style={{ fontWeight: 900, fontSize: '1.6rem', marginBottom: '0.3rem' }}>Math Blitz</h2>
      <p style={{ color: '#888', marginBottom: '1.5rem', lineHeight: 1.5 }}>
        Quick mental arithmetic for {'{1–4}'} players.<br />
        Each player answers {QUESTIONS_PER_PLAYER} questions — {TIME_PER_Q}s per question.<br />
        Score points for correct answers. Faster = same score, so be accurate!
      </p>

      <div style={{ marginBottom: '1.5rem' }}>
        <div style={{ fontWeight: 700, marginBottom: '0.75rem', color: '#aaa', fontSize: '0.85rem', letterSpacing: '0.08em' }}>NUMBER OF PLAYERS</div>
        <div style={{ display: 'flex', justifyContent: 'center', gap: '0.5rem' }}>
          {[1, 2, 3, 4].map(n => (
            <button key={n} onClick={() => setCount(n)} style={{
              width: 52, height: 52, borderRadius: 12, fontWeight: 900, fontSize: '1.4rem',
              background: count === n ? PLAYER_COLORS[n - 1] : '#1a1a2e',
              border: `2px solid ${count === n ? PLAYER_COLORS[n - 1] : '#2a2a4a'}`,
              color: count === n ? '#fff' : '#666', cursor: 'pointer', transition: 'all 0.15s',
            }}>{n}</button>
          ))}
        </div>
      </div>

      <div style={{ marginBottom: '2rem' }}>
        <div style={{ fontWeight: 700, marginBottom: '0.75rem', color: '#aaa', fontSize: '0.85rem', letterSpacing: '0.08em' }}>PLAYER NAMES (optional)</div>
        <div style={{ display: 'flex', flexDirection: 'column', gap: '0.5rem' }}>
          {Array.from({ length: count }, (_, i) => (
            <div key={i} style={{ display: 'flex', alignItems: 'center', gap: '0.75rem' }}>
              <div style={{ width: 12, height: 12, borderRadius: '50%', background: PLAYER_COLORS[i], flexShrink: 0 }} />
              <input
                value={names[i]}
                onChange={e => updateName(i, e.target.value)}
                placeholder={`Player ${i + 1}`}
                style={{
                  flex: 1, padding: '0.6rem 0.9rem', borderRadius: 8, border: '1px solid #2a2a4a',
                  background: '#0e0e1e', color: '#e0e0ff', fontFamily: 'inherit', fontSize: '0.95rem', outline: 'none',
                }}
              />
            </div>
          ))}
        </div>
      </div>

      <button onClick={() => onStart(count, names.slice(0, count))} style={{
        padding: '0.85rem 2.5rem', borderRadius: 12, background: '#4488ff', border: 'none',
        color: '#fff', fontWeight: 900, fontSize: '1.1rem', cursor: 'pointer', letterSpacing: '0.05em',
      }}>
        ▶ START GAME
      </button>
    </div>
  );
}

// ─── Player Turn Screen ────────────────────────────────────────────────────────
function PlayerTurn({ player, playerIdx, questions, onDone }) {
  const [qIdx, setQIdx] = useState(0);
  const [input, setInput] = useState('');
  const [timeLeft, setTimeLeft] = useState(TIME_PER_Q);
  const [results, setResults] = useState([]); // { correct, answer, given, q }
  const [phase, setPhase] = useState('ready'); // 'ready' | 'playing' | 'done'
  const inputRef = useRef();
  const timerRef = useRef();

  const q = questions[qIdx];

  const submitAnswer = useCallback((givenRaw) => {
    clearInterval(timerRef.current);
    const given = parseInt(givenRaw, 10);
    const correct = !isNaN(given) && given === q.answer;
    const newResults = [...results, { correct, answer: q.answer, given, text: q.text }];
    setResults(newResults);
    if (qIdx + 1 >= questions.length) {
      setPhase('done');
      const score = newResults.filter(r => r.correct).length;
      setTimeout(() => onDone(score, newResults), 1200);
    } else {
      setQIdx(i => i + 1);
      setInput('');
      setTimeLeft(TIME_PER_Q);
    }
  }, [q, qIdx, questions, results, onDone]);

  // Timer
  useEffect(() => {
    if (phase !== 'playing') return;
    timerRef.current = setInterval(() => {
      setTimeLeft(t => {
        if (t <= 1) {
          submitAnswer('');
          return TIME_PER_Q;
        }
        return t - 1;
      });
    }, 1000);
    return () => clearInterval(timerRef.current);
  }, [phase, qIdx, submitAnswer]);

  useEffect(() => {
    if (phase === 'playing') {
      setTimeLeft(TIME_PER_Q);
      inputRef.current?.focus();
    }
  }, [qIdx, phase]);

  if (phase === 'ready') return (
    <div style={{ textAlign: 'center', padding: '2rem 1rem', maxWidth: 400, margin: '0 auto' }}>
      <div style={{ width: 60, height: 60, borderRadius: '50%', background: PLAYER_COLORS[playerIdx], display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: '1.8rem', fontWeight: 900, color: '#fff', margin: '0 auto 1rem' }}>
        {(player[0] || 'P').toUpperCase()}
      </div>
      <h2 style={{ fontWeight: 800, fontSize: '1.5rem', marginBottom: '0.5rem', color: PLAYER_COLORS[playerIdx] }}>{player}</h2>
      <p style={{ color: '#888', marginBottom: '1.5rem', lineHeight: 1.6 }}>
        Your turn! Answer {questions.length} math problems.<br />
        You have {TIME_PER_Q} seconds per question.<br />
        Press Enter or → to submit.
      </p>
      <button onClick={() => setPhase('playing')} style={{
        padding: '0.85rem 2.5rem', borderRadius: 12, background: PLAYER_COLORS[playerIdx], border: 'none',
        color: '#fff', fontWeight: 900, fontSize: '1.1rem', cursor: 'pointer',
      }}>Ready!</button>
    </div>
  );

  if (phase === 'done') {
    const score = results.filter(r => r.correct).length;
    return (
      <div style={{ textAlign: 'center', padding: '2rem 1rem', maxWidth: 400, margin: '0 auto' }}>
        <div style={{ fontSize: '3rem', marginBottom: '0.5rem' }}>✅</div>
        <h2 style={{ color: PLAYER_COLORS[playerIdx], fontWeight: 900 }}>{player}: {score}/{questions.length}</h2>
        <p style={{ color: '#888' }}>Calculating…</p>
      </div>
    );
  }

  const timerPct = (timeLeft / TIME_PER_Q) * 100;
  const timerColor = timeLeft > 4 ? '#44cc88' : timeLeft > 2 ? '#ffaa22' : '#ff4444';

  return (
    <div style={{ maxWidth: 480, margin: '0 auto' }}>
      {/* Header */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1rem' }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: '0.6rem' }}>
          <div style={{ width: 32, height: 32, borderRadius: '50%', background: PLAYER_COLORS[playerIdx], display: 'flex', alignItems: 'center', justifyContent: 'center', fontWeight: 900, color: '#fff', fontSize: '0.9rem' }}>
            {(player[0] || 'P').toUpperCase()}
          </div>
          <span style={{ fontWeight: 700, color: PLAYER_COLORS[playerIdx] }}>{player}</span>
        </div>
        <span style={{ color: '#888', fontWeight: 700, fontSize: '0.85rem' }}>Q {qIdx + 1} / {questions.length}</span>
      </div>

      {/* Timer bar */}
      <div style={{ height: 6, background: '#1a1a2e', borderRadius: 3, marginBottom: '1.5rem', overflow: 'hidden' }}>
        <div style={{ height: '100%', width: `${timerPct}%`, background: timerColor, transition: 'width 0.9s linear, background 0.3s', borderRadius: 3 }} />
      </div>

      {/* Question */}
      <div style={{
        background: PLAYER_BG[playerIdx], border: `2px solid ${PLAYER_COLORS[playerIdx]}33`,
        borderRadius: 16, padding: '2rem', textAlign: 'center', marginBottom: '1.5rem',
      }}>
        <div style={{ fontSize: '0.72rem', color: '#888', fontWeight: 700, letterSpacing: '0.1em', marginBottom: '0.75rem' }}>CALCULATE</div>
        <div style={{ fontSize: '2.4rem', fontWeight: 900, color: '#fff', letterSpacing: '0.05em' }}>{q.text}</div>
        <div style={{ fontSize: '0.75rem', color: timerColor, fontWeight: 700, marginTop: '0.75rem' }}>⏱ {timeLeft}s</div>
      </div>

      {/* Previous result flash */}
      {results.length > 0 && (() => {
        const last = results[results.length - 1];
        return (
          <div style={{
            padding: '0.5rem 1rem', borderRadius: 8, marginBottom: '1rem',
            background: last.correct ? '#0a2a1a' : '#2a0a0a',
            border: `1px solid ${last.correct ? '#44cc88' : '#ff4444'}`,
            color: last.correct ? '#44cc88' : '#ff6666', fontSize: '0.85rem', textAlign: 'center',
          }}>
            {last.correct ? `✓ Correct! ${last.text} = ${last.answer}` : `✗ Wrong! ${last.text} = ${last.answer}`}
          </div>
        );
      })()}

      {/* Input */}
      <div style={{ display: 'flex', gap: '0.75rem' }}>
        <input
          ref={inputRef}
          type="number"
          value={input}
          onChange={e => setInput(e.target.value)}
          onKeyDown={e => e.key === 'Enter' && input.trim() !== '' && submitAnswer(input)}
          placeholder="Your answer…"
          style={{
            flex: 1, padding: '0.9rem 1rem', borderRadius: 10, border: `2px solid ${PLAYER_COLORS[playerIdx]}55`,
            background: '#0e0e1e', color: '#e0e0ff', fontSize: '1.3rem', fontFamily: 'inherit',
            outline: 'none', fontWeight: 700, textAlign: 'center',
          }}
        />
        <button
          disabled={input.trim() === ''}
          onClick={() => submitAnswer(input)}
          style={{
            padding: '0 1.5rem', borderRadius: 10, border: 'none',
            background: input.trim() ? PLAYER_COLORS[playerIdx] : '#1a1a2e',
            color: '#fff', fontWeight: 900, fontSize: '1.3rem', cursor: input.trim() ? 'pointer' : 'default',
          }}>→</button>
      </div>

      {/* Progress dots */}
      <div style={{ display: 'flex', justifyContent: 'center', gap: '0.4rem', marginTop: '1.25rem' }}>
        {questions.map((_, i) => {
          const r = results[i];
          return (
            <div key={i} style={{
              width: 10, height: 10, borderRadius: '50%',
              background: r ? (r.correct ? '#44cc88' : '#ff4444') : (i === qIdx ? PLAYER_COLORS[playerIdx] : '#2a2a4a'),
              transition: 'background 0.3s',
            }} />
          );
        })}
      </div>
    </div>
  );
}

// ─── Scoreboard ───────────────────────────────────────────────────────────────
function Scoreboard({ players, scores, allResults, onRestart }) {
  const ranked = players
    .map((p, i) => ({ name: p, score: scores[i], idx: i, results: allResults[i] }))
    .sort((a, b) => b.score - a.score);

  const medals = ['🥇', '🥈', '🥉', '🏅'];

  return (
    <div style={{ maxWidth: 520, margin: '0 auto', textAlign: 'center' }}>
      <div style={{ fontSize: '3rem', marginBottom: '0.5rem' }}>🏆</div>
      <h2 style={{ fontWeight: 900, fontSize: '1.8rem', marginBottom: '0.25rem' }}>Game Over!</h2>
      <p style={{ color: '#888', marginBottom: '1.5rem' }}>Final Scores</p>

      <div style={{ display: 'flex', flexDirection: 'column', gap: '0.6rem', marginBottom: '1.5rem' }}>
        {ranked.map((p, rank) => (
          <div key={p.idx} style={{
            display: 'flex', alignItems: 'center', gap: '1rem',
            background: rank === 0 ? PLAYER_BG[p.idx] : '#0e0e1e',
            border: `2px solid ${rank === 0 ? PLAYER_COLORS[p.idx] : '#1a1a2e'}`,
            borderRadius: 12, padding: '0.85rem 1.25rem',
          }}>
            <span style={{ fontSize: '1.5rem', width: 30, textAlign: 'left' }}>{medals[rank]}</span>
            <div style={{ width: 36, height: 36, borderRadius: '50%', background: PLAYER_COLORS[p.idx], display: 'flex', alignItems: 'center', justifyContent: 'center', fontWeight: 900, color: '#fff', fontSize: '1rem', flexShrink: 0 }}>
              {(p.name[0] || 'P').toUpperCase()}
            </div>
            <div style={{ flex: 1, textAlign: 'left' }}>
              <div style={{ fontWeight: 700, color: PLAYER_COLORS[p.idx] }}>{p.name}</div>
              <div style={{ fontSize: '0.75rem', color: '#666' }}>
                {p.results?.filter(r => r.correct).length ?? p.score} correct, {p.results?.filter(r => !r.correct).length ?? 0} wrong
              </div>
            </div>
            <div style={{ fontWeight: 900, fontSize: '1.6rem', color: PLAYER_COLORS[p.idx] }}>
              {p.score}<span style={{ fontSize: '0.8rem', color: '#666', fontWeight: 600 }}>/{QUESTIONS_PER_PLAYER}</span>
            </div>
          </div>
        ))}
      </div>

      {/* Per-player breakdown */}
      {ranked.map(p => p.results && (
        <details key={p.idx} style={{ background: '#0e0e1e', border: '1px solid #1a1a2e', borderRadius: 10, padding: '0.75rem 1rem', marginBottom: '0.5rem', textAlign: 'left' }}>
          <summary style={{ fontWeight: 700, color: PLAYER_COLORS[p.idx], cursor: 'pointer', listStyle: 'none', display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
            {p.name}'s answers
          </summary>
          <div style={{ marginTop: '0.75rem', display: 'flex', flexDirection: 'column', gap: '0.3rem' }}>
            {p.results.map((r, i) => (
              <div key={i} style={{ display: 'flex', alignItems: 'center', gap: '0.75rem', fontSize: '0.85rem' }}>
                <span>{r.correct ? '✓' : '✗'}</span>
                <span style={{ flex: 1, color: '#ccc' }}>{r.text} = <strong>{r.answer}</strong></span>
                {!r.correct && r.given != null && !isNaN(r.given) && (
                  <span style={{ color: '#ff6666' }}>you: {r.given}</span>
                )}
                {!r.correct && (isNaN(r.given) || r.given == null) && (
                  <span style={{ color: '#888' }}>timeout</span>
                )}
              </div>
            ))}
          </div>
        </details>
      ))}

      <button onClick={onRestart} style={{
        marginTop: '1rem', padding: '0.85rem 2.5rem', borderRadius: 12, background: '#4488ff', border: 'none',
        color: '#fff', fontWeight: 900, fontSize: '1.1rem', cursor: 'pointer',
      }}>▶ Play Again</button>
    </div>
  );
}

// ─── Main Component ────────────────────────────────────────────────────────────
export default function MathBlitz() {
  const [phase, setPhase]         = useState('setup'); // 'setup' | 'playing' | 'board'
  const [players, setPlayers]     = useState([]);
  const [allQuestions, setAll]    = useState([]); // questions per player
  const [currentPlayer, setCurrent] = useState(0);
  const [scores, setScores]       = useState([]);
  const [allResults, setAllResults] = useState([]);

  const startGame = (count, names) => {
    setPlayers(names);
    setAll(Array.from({ length: count }, generatePlayerQuestions));
    setScores(Array(count).fill(0));
    setAllResults(Array(count).fill(null));
    setCurrent(0);
    setPhase('playing');
  };

  const handleDone = (score, results) => {
    const newScores = [...scores]; newScores[currentPlayer] = score;
    const newResults = [...allResults]; newResults[currentPlayer] = results;
    setScores(newScores);
    setAllResults(newResults);
    if (currentPlayer + 1 >= players.length) {
      setPhase('board');
    } else {
      setCurrent(p => p + 1);
    }
  };

  const restart = () => {
    setPhase('setup');
    setPlayers([]);
    setCurrent(0);
    setScores([]);
    setAllResults([]);
  };

  if (phase === 'setup') return (
    <div style={{ background: '#080818', borderRadius: 16, padding: '1.5rem', color: '#e0e0ff' }}>
      <Setup onStart={startGame} />
    </div>
  );

  if (phase === 'playing') return (
    <div style={{ background: '#080818', borderRadius: 16, padding: '1.5rem', color: '#e0e0ff' }}>
      {/* Progress bar: which player's turn */}
      <div style={{ display: 'flex', gap: '0.4rem', marginBottom: '1.5rem' }}>
        {players.map((p, i) => (
          <div key={i} style={{ flex: 1, height: 5, borderRadius: 3, background: i < currentPlayer ? PLAYER_COLORS[i] : i === currentPlayer ? PLAYER_COLORS[i] : '#1a1a2e', opacity: i < currentPlayer ? 0.5 : 1 }} />
        ))}
      </div>
      <PlayerTurn
        key={currentPlayer}
        player={players[currentPlayer]}
        playerIdx={currentPlayer}
        questions={allQuestions[currentPlayer]}
        onDone={handleDone}
      />
    </div>
  );

  if (phase === 'board') return (
    <div style={{ background: '#080818', borderRadius: 16, padding: '1.5rem', color: '#e0e0ff' }}>
      <Scoreboard players={players} scores={scores} allResults={allResults} onRestart={restart} />
    </div>
  );

  return null;
}
