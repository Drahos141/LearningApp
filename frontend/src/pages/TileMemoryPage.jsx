import { useNavigate } from 'react-router-dom';
import TileMemory from '../games/TileMemory';

export default function TileMemoryPage() {
  const navigate = useNavigate();
  return (
    <div className="gameplay">
      <div className="gameplay-header">
        <div className="nav-row">
          <button className="home-btn" onClick={() => navigate('/')}>🏠 Home</button>
          <button className="back-btn" onClick={() => navigate('/brain-games')}>← Brain Games</button>
        </div>
        <h1 className="gameplay-title">🟦 Tile Memory</h1>
        <p className="gameplay-desc">Watch the pattern light up — then reproduce it from memory. More tiles every round!</p>
        <div style={{ display:'flex', gap:'0.5rem', marginTop:'0.5rem', flexWrap:'wrap' }}>
          <span className="badge">memory</span>
          <span className="badge badge-difficulty-medium">1–6 players</span>
        </div>
      </div>
      <div className="gameplay-instructions">
        📖 During the <strong>Memorize</strong> phase, watch which tiles glow. During <strong>Recall</strong>, click those same tiles and submit. Correct tiles earn points; wrong tiles subtract. Each round adds one more tile.
      </div>
      <TileMemory />
    </div>
  );
}
