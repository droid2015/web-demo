import { useState, useEffect } from 'react';
import { moduleFunctionService } from '../../../services/moduleFunctionService';
import { moduleService } from '../../../services/moduleService';
import './ModuleFunctionManager.css';

const ModuleFunctionManager = () => {
  const [modules, setModules] = useState([]);
  const [functions, setFunctions] = useState([]);
  const [selectedModule, setSelectedModule] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [showAddForm, setShowAddForm] = useState(false);
  const [newFunction, setNewFunction] = useState({
    name: '',
    code: '',
    description: '',
    isEnabled: true
  });

  useEffect(() => {
    loadModules();
  }, []);

  useEffect(() => {
    if (selectedModule) {
      loadFunctions(selectedModule);
    }
  }, [selectedModule]);

  const loadModules = async () => {
    try {
      setLoading(true);
      const data = await moduleService.getAll();
      setModules(data);
      if (data.length > 0 && !selectedModule) {
        setSelectedModule(data[0].id);
      }
    } catch (err) {
      setError('Không thể tải danh sách module');
    } finally {
      setLoading(false);
    }
  };

  const loadFunctions = async (moduleId) => {
    try {
      setLoading(true);
      const data = await moduleFunctionService.getByModuleId(moduleId);
      setFunctions(data);
    } catch (err) {
      setError('Không thể tải danh sách chức năng');
    } finally {
      setLoading(false);
    }
  };

  const handleAddFunction = async (e) => {
    e.preventDefault();
    try {
      await moduleFunctionService.create({
        moduleId: selectedModule,
        ...newFunction
      });
      setShowAddForm(false);
      setNewFunction({ name: '', code: '', description: '', isEnabled: true });
      loadFunctions(selectedModule);
    } catch (err) {
      alert('Không thể thêm chức năng');
    }
  };

  const handleToggle = async (id, currentStatus) => {
    try {
      await moduleFunctionService.toggle(id, !currentStatus);
      loadFunctions(selectedModule);
    } catch (err) {
      alert('Không thể thay đổi trạng thái chức năng');
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm('Bạn có chắc chắn muốn xóa chức năng này?')) return;
    
    try {
      await moduleFunctionService.delete(id);
      loadFunctions(selectedModule);
    } catch (err) {
      alert('Không thể xóa chức năng');
    }
  };

  if (loading && !modules.length) return <div className="loading">Đang tải...</div>;
  if (error) return <div className="error">{error}</div>;

  return (
    <div className="module-function-manager">
      <div className="page-header">
        <h1>Quản lý chức năng Module</h1>
        <button 
          className="btn-add"
          onClick={() => setShowAddForm(!showAddForm)}
        >
          {showAddForm ? 'Hủy' : '+ Thêm chức năng'}
        </button>
      </div>

      <div className="module-selector">
        <label>Chọn Module:</label>
        <select 
          value={selectedModule || ''} 
          onChange={(e) => setSelectedModule(Number(e.target.value))}
        >
          {modules.map(module => (
            <option key={module.id} value={module.id}>
              {module.name}
            </option>
          ))}
        </select>
      </div>

      {showAddForm && (
        <div className="add-function-form">
          <h3>Thêm chức năng mới</h3>
          <form onSubmit={handleAddFunction}>
            <div className="form-group">
              <label>Tên chức năng:</label>
              <input
                type="text"
                value={newFunction.name}
                onChange={(e) => setNewFunction({...newFunction, name: e.target.value})}
                required
              />
            </div>
            <div className="form-group">
              <label>Mã chức năng:</label>
              <input
                type="text"
                value={newFunction.code}
                onChange={(e) => setNewFunction({...newFunction, code: e.target.value})}
                required
                placeholder="VD: CREATE_TASK"
              />
            </div>
            <div className="form-group">
              <label>Mô tả:</label>
              <textarea
                value={newFunction.description}
                onChange={(e) => setNewFunction({...newFunction, description: e.target.value})}
                rows="3"
              />
            </div>
            <div className="form-group checkbox">
              <label>
                <input
                  type="checkbox"
                  checked={newFunction.isEnabled}
                  onChange={(e) => setNewFunction({...newFunction, isEnabled: e.target.checked})}
                />
                Kích hoạt
              </label>
            </div>
            <button type="submit" className="btn-submit">Thêm chức năng</button>
          </form>
        </div>
      )}

      <div className="functions-list">
        {loading ? (
          <div>Đang tải chức năng...</div>
        ) : functions.length === 0 ? (
          <div className="empty-state">
            <p>Chưa có chức năng nào cho module này</p>
          </div>
        ) : (
          <table className="functions-table">
            <thead>
              <tr>
                <th>Tên chức năng</th>
                <th>Mã</th>
                <th>Mô tả</th>
                <th>Trạng thái</th>
                <th>Hành động</th>
              </tr>
            </thead>
            <tbody>
              {functions.map(func => (
                <tr key={func.id}>
                  <td>{func.name}</td>
                  <td><code>{func.code}</code></td>
                  <td>{func.description}</td>
                  <td>
                    <span className={`status ${func.isEnabled ? 'enabled' : 'disabled'}`}>
                      {func.isEnabled ? 'Kích hoạt' : 'Vô hiệu hóa'}
                    </span>
                  </td>
                  <td className="actions">
                    <button
                      onClick={() => handleToggle(func.id, func.isEnabled)}
                      className={`btn-toggle ${func.isEnabled ? 'btn-disable' : 'btn-enable'}`}
                    >
                      {func.isEnabled ? 'Vô hiệu hóa' : 'Kích hoạt'}
                    </button>
                    <button
                      onClick={() => handleDelete(func.id)}
                      className="btn-delete"
                    >
                      Xóa
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
};

export default ModuleFunctionManager;
