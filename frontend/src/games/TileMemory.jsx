import { useState, useEffect, useCallback, useRef } from 'react';

const GRID_SIZE = 4;
const TOTAL_TILES = GRID_SIZE * GRID_SIZE;
const MEMORIZE_SECONDS = 3;
const TILE_COUNT_START = 3;
const MAX_ROUNDS = 8;

function getTilesForRound(round) {
  return Math.min(TILE_COUNT_START + (round - 1), TOTAL_TILES - 1);
}

function randomActiveTiles(count) {
  const indices = [];
  while (indices.length < count) {
    const idx = Math.floor(Math.random() * TOTAL_TILES);
    if (!indices.includes(idx)) indices.push(idx);
  }
  return indices;
}

export default function TileMemory() {
  const [phase, setPhase] = useState('setup');
  const [players, setPlayers] = useState([]);
  const [numPlayers, setNumPlayers] = useState(2);
  const [playerNames, setPlayerNames] = useState(['Player 1', 'Player 2', 'Player 3', 'Player 4', 'Player 5', 'Player 6']);
  const [currentPlayerIdx, setCurrentPlayerIdx] = useState(0);
  const [round, setRound] = useState(1);
  const [activeTiles, setActiveTiles] = useState([]);
  const [selectedTiles, setSelectedTiles] = useState([]);
  const [timer, setTimer] = useState(MEMORIZE_SECONDS);
  const [roundResult, setRoundResult] = useState(null);
  const timerRef = useRef(null);

  const beginMemorizePhase = useCallback((roundNum) => {
    const count = getTilesForRound(roundNum);
    setActiveTiles(randomActiveTiles(count));
    setSelectedTiles([]);
    setTimer(MEMORIZE_SECONDS);
    setPhase('memorize');
    setRoundResult(null);
  }, []);

  const startGame = () => {
    const names = playerNames.slice(0, numPlayers);
    const initialPlayers = names.map(n => ({ name: n.trim() || 'Player', score: 0 }));
    setPlayers(initialPlayers);
    setCurrentPlayerIdx(0);
    setRound(1);
    setActiveTiles([]);
    setSelectedTiles([]);
    setRoundResult(null);
    const count = getTilesForRound(1);
    setActiveTiles(randomActiveTiles(count));
    setTimer(MEMORIZE_SECONDS);
    setPhase('memorize');
  };

  useEffect(() => {
    if (phase !== 'memorize') return;
    timerRef.current = setInterval(() => {
      setTimer(t => {
        if (t <= 1) {
          clearInterval(timerRef.current);
          setPhase('recall');
          return 0;
        }
        return t - 1;
      });
    }, 1000);
    return () => clearInterval(timerRef.current);
  }, [phase, activeTiles]);

  const handleTileClick = (idx) => {
    if (phase !== 'recall') return;
    setSelectedTiles(prev =>
      prev.includes(idx) ? prev.filter(i => i !== idx) : [...prev, idx]
    );
  };

  const submitRecall = () => {
    const correct = activeTiles.filter(i => selectedTiles.includes(i));
    const missed = activeTiles.filter(i => !selectedTiles.includes(i));
    const wrong = selectedTiles.filter(i => !activeTiles.includes(i));
    const points = Math.max(0, correct.length - wrong.length);
    setPlayers(prev => prev.map((p, i) =>
      i === currentPlayerIdx ? { ...p, score: p.score + points } : p
    ));
    setRoundResult({ correct: correct.length, missed: missed.length, wrong: wrong.length, points });
    setPhase('result');
  };

  const nextTurn = () => {
    const nextIdx = currentPlayerIdx + 1;
    if (nextIdx >= players.length) {
      const nextRound = round + 1;
      if (nextRound > MAX_ROUNDS) { setPhase('gameover'); return; }
      setRound(nextRound);
      setCurrentPlayerIdx(0);
      beginMemorizePhase(nextRound);
    } else {
      setCurrentPlayerIdx(nextIdx);
      beginMemorizePhase(round);
    }
  };

  const resetGame = () => {
    setPhase('setup');
    setRound(1);
    setCurrentPlayerIdx(0);
    setActiveTiles([]);
    setSelectedTiles([]);
    setRoundResult(null);
    setPlayers([]);
  };

  if (phase === 'setup') {
    return (
      <div style={S.setupWrap}>
        <div style={S.card}>
          <div style={S.bigIcon}>🧠</div>
          <h2 style={S.cardTitle}>Tile Memory</h2>
          <p style={S.cardSub}>Watch the glowing tiles, then reproduce the pattern!</p>

          <label style={S.label}>Number of Players</label>
          <div style={S.countRow}>
            {[1,2,3,4,5,6].map(n => (
              <button key={n}
                style={n === numPlayers ? {...S.countBtn, ...S.countBtnOn} : S.countBtn}
                onClick={() => setNumPlayers(n)}>{n}</button>
            ))}
          </div>

          <label style={{...S.label, marginTop:'1.25rem'}}>Player Names</label>
          {Array.from({length: numPlayers}, (_, i) => (
            <input key={i} style={S.input}
              placeholder={`Player ${i+1}`}
              value={playerNames[i] || ''}
              onChange={e => setPlayerNames(prev => { const a=[...prev]; a[i]=e.target.value; return a; })}
            />
          ))}

          <button style={S.startBtn} onClick={startGame}>Start Game 🚀</button>
          <p style={S.hint}>{MAX_ROUNDS} rounds · tiles increase each round</p>
        </div>
      </div>
    );
  }

  if (phase === 'gameover') {
    const sorted = [...players].sort((a,b) => b.score - a.score);
    return (
      <div style={S.setupWrap}>
        <div style={S.card}>
          <div style={S.bigIcon}>🏆</div>
          <h2 style={S.cardTitle}>Game Over!</h2>
          <p style={{...S.cardSub, color:'#a78bfa'}}>🎉 {sorted[0].name} wins with {sorted[0].score} pts!</p>
          <div style={S.board}>
            {sorted.map((p, i) => (
              <div key={i} style={i===0 ? {...S.boardRow, ...S.boardWinner} : S.boardRow}>
                <span style={S.rank}>{i===0?'🥇':i===1?'🥈':i===2?'🥉':`${i+1}.`}</span>
                <span style={{flex:1, textAlign:'left', fontWeight:600}}>{p.name}</span>
                <span style={{fontWeight:700, color:'#a78bfa'}}>{p.score} pts</span>
              </div>
            ))}
          </div>
          <button style={S.startBtn} onClick={resetGame}>Play Again</button>
        </div>
      </div>
    );
  }

  const tileCount = getTilesForRound(round);

  return (
    <div style={S.gameWrap}>
      {/* Header */}
      <div style={S.hdr}>
        <span style={S.roundBadge}>Round {round}/{MAX_ROUNDS}</span>
        <span style={S.tileBadge}>{tileCount} tiles</span>
      </div>
      <div style={S.playerRow}>
        {players.map((p,i) => (
          <div key={i} style={i===currentPlayerIdx ? {...S.chip, ...S.chipOn} : S.chip}>
            <span>{p.name}</span>
            <span style={S.chipPts}>{p.score}pts</span>
          </div>
        ))}
      </div>

      {/* Phase bar */}
      {phase === 'memorize' && (
        <div style={{...S.phaseBar, background:'linear-gradient(135deg,#1a2e1a,#162e13)', color:'#86efac'}}>
          <span style={S.phaseIcon}>👁️</span>
          <span><strong>Memorize!</strong> Watch the glowing tiles</span>
          <div style={S.timerCircle}>{timer}</div>
        </div>
      )}
      {phase === 'recall' && (
        <div style={{...S.phaseBar, background:'linear-gradient(135deg,#1a1a2e,#16213e)', color:'#93c5fd'}}>
          <span style={S.phaseIcon}>🎯</span>
          <span><strong>{players[currentPlayerIdx]?.name}</strong>, click the tiles you saw!</span>
          <span style={S.selCount}>{selectedTiles.length}/{tileCount}</span>
        </div>
      )}

      {/* Grid */}
      <div style={S.grid}>
        {Array.from({length: TOTAL_TILES}, (_,idx) => {
          const isActive   = phase === 'memorize' && activeTiles.includes(idx);
          const isSelected = phase === 'recall'   && selectedTiles.includes(idx);
          const isCorrect  = phase === 'result'   && activeTiles.includes(idx) && selectedTiles.includes(idx);
          const isWrong    = phase === 'result'   && selectedTiles.includes(idx) && !activeTiles.includes(idx);
          const isMissed   = phase === 'result'   && activeTiles.includes(idx) && !selectedTiles.includes(idx);
          return (
            <button key={idx}
              style={{
                ...S.tile,
                ...(isActive   ? S.tileActive   : {}),
                ...(isSelected ? S.tileSel       : {}),
                ...(isCorrect  ? S.tileOk        : {}),
                ...(isWrong    ? S.tileBad       : {}),
                ...(isMissed   ? S.tileMissed    : {}),
              }}
              onClick={() => handleTileClick(idx)}
              disabled={phase !== 'recall'}
            />
          );
        })}
      </div>

      {phase === 'recall' && (
        <div style={S.subRow}>
          <button style={S.subBtn} onClick={submitRecall}>Submit Answer ✓</button>
        </div>
      )}

      {phase === 'result' && roundResult && (
        <div style={S.overlay}>
          <div style={S.resBox}>
            <div style={{fontSize:'2.5rem'}}>
              {roundResult.wrong===0 && roundResult.missed===0 ? '🌟' : roundResult.correct>0 ? '✅' : '❌'}
            </div>
            <h3 style={{margin:'0.4rem 0', fontSize:'1.15rem'}}>{players[currentPlayerIdx]?.name}</h3>
            <div style={S.stats}>
              <span style={{color:'#22c55e', fontWeight:600}}>✓ {roundResult.correct}</span>
              <span style={{color:'#ef4444', fontWeight:600}}>✗ {roundResult.wrong}</span>
              <span style={{color:'#f97316', fontWeight:600}}>○ {roundResult.missed}</span>
            </div>
            <div style={S.pts}>+{roundResult.points} pts</div>
            <button style={S.nextBtn} onClick={nextTurn}>
              {currentPlayerIdx+1 < players.length
                ? `Next: ${players[currentPlayerIdx+1]?.name} →`
                : round >= MAX_ROUNDS ? 'Final Scores 🏆'
                : `Round ${round+1} →`}
            </button>
          </div>
        </div>
      )}
    </div>
  );
}

const S = {
  setupWrap: { display:'flex', justifyContent:'center', padding:'0.5rem' },
  card: {
    background:'#111', borderRadius:'16px', padding:'2rem', maxWidth:'440px',
    width:'100%', boxShadow:'0 8px 32px rgba(0,0,0,0.5)', color:'#fff', textAlign:'center',
  },
  bigIcon: { fontSize:'3rem', marginBottom:'0.25rem' },
  cardTitle: { fontSize:'1.7rem', fontWeight:800, margin:'0 0 0.25rem' },
  cardSub: { color:'#aaa', marginBottom:'1.5rem', fontSize:'0.9rem' },
  label: { display:'block', color:'#bbb', fontWeight:600, fontSize:'0.85rem', textAlign:'left', marginBottom:'0.4rem' },
  countRow: { display:'flex', gap:'0.4rem', flexWrap:'wrap' },
  countBtn: {
    width:'44px', height:'44px', border:'2px solid #333', background:'#1a1a1a',
    color:'#fff', borderRadius:'10px', fontWeight:700, fontSize:'1rem', cursor:'pointer',
  },
  countBtnOn: { border:'2px solid #7c5cbf', background:'#2d1f5e', color:'#c4b5fd' },
  input: {
    width:'100%', padding:'0.6rem 0.9rem', marginBottom:'0.4rem',
    background:'#1a1a1a', border:'1px solid #333', borderRadius:'8px',
    color:'#fff', fontSize:'0.9rem', outline:'none', fontFamily:'inherit',
    boxSizing:'border-box',
  },
  startBtn: {
    width:'100%', padding:'0.8rem', background:'linear-gradient(135deg,#7c5cbf,#4f46e5)',
    color:'#fff', border:'none', borderRadius:'10px', fontWeight:700,
    fontSize:'1rem', cursor:'pointer', marginTop:'0.75rem',
  },
  hint: { color:'#555', fontSize:'0.78rem', marginTop:'0.75rem' },

  gameWrap: { position:'relative' },
  hdr: { display:'flex', gap:'0.6rem', alignItems:'center', marginBottom:'0.5rem', flexWrap:'wrap' },
  roundBadge: {
    background:'#1a1a1a', color:'#fff', border:'1px solid #333',
    borderRadius:'8px', padding:'0.25rem 0.7rem', fontWeight:700, fontSize:'0.85rem',
  },
  tileBadge: {
    background:'linear-gradient(135deg,#7c5cbf,#4f46e5)', color:'#fff',
    borderRadius:'8px', padding:'0.25rem 0.7rem', fontWeight:600, fontSize:'0.82rem',
  },
  playerRow: { display:'flex', gap:'0.35rem', flexWrap:'wrap', marginBottom:'0.6rem' },
  chip: {
    display:'flex', gap:'0.35rem', alignItems:'center',
    padding:'0.25rem 0.6rem', borderRadius:'20px',
    background:'#1a1a1a', border:'1px solid #2a2a2a', color:'#777', fontSize:'0.8rem',
  },
  chipOn: { background:'linear-gradient(135deg,#2d1f5e,#1a1a40)', border:'1px solid #7c5cbf', color:'#c4b5fd', fontWeight:700 },
  chipPts: { color:'#555', fontSize:'0.72rem' },

  phaseBar: {
    display:'flex', alignItems:'center', gap:'0.6rem',
    padding:'0.65rem 0.9rem', borderRadius:'12px', marginBottom:'0.6rem',
    fontWeight:600, fontSize:'0.9rem',
  },
  phaseIcon: { fontSize:'1.2rem' },
  timerCircle: {
    marginLeft:'auto', width:'32px', height:'32px', borderRadius:'50%',
    background:'#22c55e', color:'#000', display:'flex', alignItems:'center',
    justifyContent:'center', fontWeight:800, fontSize:'0.95rem',
  },
  selCount: {
    marginLeft:'auto', background:'#4f46e5', color:'#fff',
    padding:'0.15rem 0.5rem', borderRadius:'12px', fontWeight:700, fontSize:'0.85rem',
  },

  grid: {
    display:'grid', gridTemplateColumns:`repeat(${GRID_SIZE},1fr)`,
    gap:'0.45rem', maxWidth:'360px', margin:'0 auto',
  },
  tile: {
    aspectRatio:'1', borderRadius:'10px', border:'2px solid #2a2a2a',
    background:'#1a1a1a', cursor:'pointer', transition:'all 0.15s', outline:'none',
  },
  tileActive: {
    background:'linear-gradient(135deg,#7c5cbf,#4f46e5)', border:'2px solid #7c5cbf',
    boxShadow:'0 0 16px 4px rgba(124,92,191,0.65)', transform:'scale(1.05)',
  },
  tileSel: {
    background:'linear-gradient(135deg,#1e3a5f,#2d4a7a)', border:'2px solid #3b82f6',
    boxShadow:'0 0 10px 2px rgba(59,130,246,0.4)', transform:'scale(1.03)',
  },
  tileOk: {
    background:'linear-gradient(135deg,#14532d,#166534)', border:'2px solid #22c55e',
    boxShadow:'0 0 10px 2px rgba(34,197,94,0.4)',
  },
  tileBad: {
    background:'linear-gradient(135deg,#450a0a,#7f1d1d)', border:'2px solid #ef4444',
  },
  tileMissed: {
    background:'linear-gradient(135deg,#431407,#7c2d12)', border:'2px solid #f97316', opacity:0.75,
  },

  subRow: { display:'flex', justifyContent:'center', marginTop:'0.9rem' },
  subBtn: {
    padding:'0.7rem 2rem', background:'linear-gradient(135deg,#7c5cbf,#4f46e5)',
    color:'#fff', border:'none', borderRadius:'10px', fontWeight:700, fontSize:'0.95rem', cursor:'pointer',
  },

  overlay: {
    position:'absolute', inset:0, background:'rgba(0,0,0,0.87)',
    display:'flex', alignItems:'center', justifyContent:'center',
    borderRadius:'16px', zIndex:10,
  },
  resBox: {
    background:'#111', borderRadius:'16px', padding:'1.75rem', textAlign:'center',
    color:'#fff', minWidth:'240px', boxShadow:'0 8px 32px rgba(0,0,0,0.7)',
  },
  stats: { display:'flex', gap:'1rem', justifyContent:'center', margin:'0.5rem 0', flexWrap:'wrap' },
  pts: { fontSize:'1.4rem', fontWeight:800, color:'#a78bfa', margin:'0.4rem 0 0.9rem' },
  nextBtn: {
    padding:'0.65rem 1.25rem', background:'linear-gradient(135deg,#7c5cbf,#4f46e5)',
    color:'#fff', border:'none', borderRadius:'10px', fontWeight:700, fontSize:'0.9rem',
    cursor:'pointer', width:'100%',
  },

  board: { marginBottom:'1.5rem' },
  boardRow: {
    display:'flex', alignItems:'center', gap:'0.6rem',
    padding:'0.5rem 0.7rem', borderRadius:'8px', marginBottom:'0.35rem',
    background:'#1a1a1a', color:'#ccc',
  },
  boardWinner: { background:'linear-gradient(135deg,#2d1f5e,#1a1a40)', color:'#c4b5fd', fontWeight:700 },
  rank: { width:'26px', textAlign:'center', fontSize:'1rem' },
};
