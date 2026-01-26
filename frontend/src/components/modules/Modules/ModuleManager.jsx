import { useState, useEffect } from 'react';
import { moduleService } from '../../../services/moduleService';
import './ModuleManager.css';

const ModuleManager = () => {
  const [modules, setModules] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadModules();
  }, []);

  const loadModules = async () => {
    try {
      setLoading(true);
      const data = await moduleService.getAll();
      setModules(data);
    } catch (err) {
      setError('Failed to load modules');
    } finally {
      setLoading(false);
    }
  };

  const handleToggle = async (id, currentStatus) => {
    try {
      await moduleService.toggle(id, !currentStatus);
      loadModules();
    } catch (err) {
      alert('Failed to toggle module');
    }
  };

  if (loading) return <div>Loading modules...</div>;
  if (error) return <div className="error">{error}</div>;

  return (
    <div className="module-manager">
      <div className="page-header">
        <h1>Module Manager</h1>
      </div>

      <div className="modules-grid">
        {modules.map(module => (
          <div key={module.id} className="module-card">
            <div className="module-info">
              <h3>{module.name}</h3>
              <p className="module-version">Version: {module.version}</p>
              <p className="module-order">Load Order: {module.loadOrder}</p>
            </div>
            <div className="module-status">
              <span className={`status ${module.isEnabled ? 'enabled' : 'disabled'}`}>
                {module.isEnabled ? 'Enabled' : 'Disabled'}
              </span>
              {module.name !== 'Core' && (
                <button
                  onClick={() => handleToggle(module.id, module.isEnabled)}
                  className={`btn-toggle ${module.isEnabled ? 'btn-disable' : 'btn-enable'}`}
                >
                  {module.isEnabled ? 'Disable' : 'Enable'}
                </button>
              )}
            </div>
          </div>
        ))}
      </div>

      {modules.length === 0 && (
        <div className="empty-state">
          <p>No modules found</p>
        </div>
      )}
    </div>
  );
};

export default ModuleManager;
