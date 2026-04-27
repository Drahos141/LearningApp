import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getGames } from '../api/api';

const FILTERS = ['all','memory','logic','math','word','pattern','spatial','sequence'];

export default function GamesHub() {
  const [games, setGames] = useState([]);
  const [filter, setFilter] = useState('all');
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    getGames()
      .then(setGames)
      .catch(() => {})
      .finally(() => setLoading(false));
  }, []);

  const filtered = filter === 'all' ? games : games.filter(g => g.type === filter);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;

  return (
    <div className="games-hub">
      <header className="games-hub-header">
        <h1>🧩 Logical Games</h1>
        <p>Train your brain with 33 interactive games</p>
      </header>
      <main className="games-hub-main">
        <div className="nav-row">
          <button className="home-btn" onClick={() => navigate('/')}>🏠 Home</button>
          <button className="back-btn" onClick={() => navigate('/')}>← Back</button>
        </div>

        <div className="filter-bar">
          {FILTERS.map(f => (
            <button key={f} className={`filter-btn${filter === f ? ' active' : ''}`} onClick={() => setFilter(f)}>
              {f}
            </button>
          ))}
        </div>

        <div className="games-grid">
          {filtered.map(game => (
            <button key={game._id} className="game-card" onClick={() => navigate(`/games/${game.slug}`)}>
              <div className="game-card-name">{game.name}</div>
              <div className="game-card-desc">{game.description}</div>
              <div className="game-badges">
                <span className="badge">{game.type}</span>
                <span className={`badge badge-difficulty-${game.difficulty}`}>{game.difficulty}</span>
              </div>
            </button>
          ))}
        </div>
      </main>
    </div>
  );
}
