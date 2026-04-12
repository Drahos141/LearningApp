import { useState } from 'react';

// Shapes as 4x4 grids of 0/1
const SHAPES = [
  [[1,1,0,0],[1,0,0,0],[1,0,0,0],[0,0,0,0]], // L
  [[1,1,1,0],[0,1,0,0],[0,1,0,0],[0,0,0,0]], // T
  [[1,1,0,0],[0,1,0,0],[0,1,1,0],[0,0,0,0]], // S
  [[0,1,1,0],[0,1,0,0],[1,1,0,0],[0,0,0,0]], // Z
];

function rotate90(grid) {
  const n = grid.length;
  return Array.from({length:n},(_,r)=>Array.from({length:n},(_,c)=>grid[n-1-c][r]));
}

function Grid({grid,size=40}) {
  return (
    <div style={{ display: 'grid', gridTemplateColumns: `repeat(4,${size}px)`, gap: '2px' }}>
      {grid.map((row,r)=>row.map((v,c)=>(
        <div key={`${r}-${c}`} style={{ width:size, height:size, background: v ? '#0a0a0a' : '#f0f0f0', borderRadius:'4px' }} />
      )))}
    </div>
  );
}

function makeQuestion() {
  const shape = SHAPES[Math.floor(Math.random()*SHAPES.length)];
  const rotations = [0,1,2,3];
  let grid = shape;
  const targetRot = Math.floor(Math.random()*4);
  let target = shape;
  for (let i=0;i<targetRot;i++) target = rotate90(target);
  const options = rotations.map(r => { let g = shape; for(let i=0;i<r;i++) g=rotate90(g); return {g,r}; });
  options.sort(()=>Math.random()-0.5);
  return { original: grid, target, correctIdx: options.findIndex(o=>o.r===targetRot), options };
}

export default function VisualRotation() {
  const [questions] = useState(()=>Array.from({length:8},makeQuestion));
  const [idx,setIdx]=useState(0);
  const [score,setScore]=useState(0);
  const [chosen,setChosen]=useState(null);
  const [done,setDone]=useState(false);
  const q=questions[idx];

  const pick=(i)=>{
    if(chosen!==null)return;
    setChosen(i);
    if(i===q.correctIdx)setScore(s=>s+1);
  };
  const next=()=>{
    if(idx+1>=questions.length){setDone(true);return;}
    setIdx(i=>i+1);setChosen(null);
  };

  if(done)return(
    <div className="game-result">
      <div style={{fontSize:'3rem'}}>🔄</div>
      <h2>Score: {score}/{questions.length}</h2>
      <button className="play-btn" style={{marginTop:'1.5rem'}} onClick={()=>{setIdx(0);setScore(0);setChosen(null);setDone(false);}}>Play Again</button>
    </div>
  );

  return(
    <div>
      <div className="game-score-bar"><span>Q {idx+1}/{questions.length}</span><span>Score: {score}</span></div>
      <div style={{display:'flex',gap:'2rem',marginBottom:'1.5rem',flexWrap:'wrap',alignItems:'flex-start'}}>
        <div><div style={{fontSize:'0.75rem',color:'#6b6b6b',marginBottom:'0.5rem',fontWeight:600}}>ORIGINAL</div><Grid grid={q.original}/></div>
        <div><div style={{fontSize:'0.75rem',color:'#6b6b6b',marginBottom:'0.5rem',fontWeight:600}}>MATCH THIS ROTATION</div><Grid grid={q.target}/></div>
      </div>
      <div style={{display:'grid',gridTemplateColumns:'1fr 1fr',gap:'0.75rem'}}>
        {q.options.map((o,i)=>{
          let border='1px solid #e0e0e0',bg='#fff';
          if(chosen!==null){
            if(i===q.correctIdx){border='2px solid #16a34a';bg='#f0fdf4';}
            else if(i===chosen){border='2px solid #dc2626';bg='#fef2f2';}
          }
          return(
            <button key={i} onClick={()=>pick(i)} style={{padding:'1rem',border,borderRadius:'10px',background:bg,display:'flex',justifyContent:'center'}}>
              <Grid grid={o.g} size={32}/>
            </button>
          );
        })}
      </div>
      {chosen!==null&&<button className="next-btn" style={{marginTop:'1.5rem'}} onClick={next}>{idx+1>=questions.length?'See Results':'Next →'}</button>}
    </div>
  );
}
