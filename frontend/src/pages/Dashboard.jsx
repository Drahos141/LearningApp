import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getCategories } from '../api/api';

export default function Dashboard() {
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    getCategories()
      .then(setCategories)
      .catch(() => setError('Failed to load categories. Make sure the backend is running.'))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <div className="page-center"><div className="spinner" /></div>;
  if (error) return <div className="page-center error-msg">{error}</div>;

  return (
    <div className="dashboard">
      <header className="dashboard-header">
        <h1 className="app-title">LearningApp</h1>
        <p className="app-subtitle">Choose a topic and start learning today</p>
      </header>

      <main className="dashboard-main">
        <h2 className="section-heading">Learning Topics</h2>
        <div className="cards-grid">
          {categories.map(cat => (
            <button
              key={cat._id || cat.id}
              className="category-card"
              onClick={() => navigate(`/category/${cat._id || cat.id}`)}
            >
              <span className="card-icon">{cat.icon}</span>
              <h3 className="card-title">{cat.name}</h3>
              <p className="card-desc">{cat.description}</p>
              <span className="card-meta">{cat.subcategories?.length ?? 0} subcategories</span>
            </button>
          ))}
        </div>

        <div className="games-featured">
          <div>
            <h2>🧩 Logical Games</h2>
            <p>20 brain-training games — memory, logic, math, patterns, and more</p>
          </div>
          <button className="games-btn" onClick={() => navigate('/games')}>
            Play Now →
          </button>
        </div>
      </main>
    </div>
  );
}
