import { useState, useEffect } from 'react';
import { congViecService } from '../../../services/congViecService';
import { useAuth } from '../../../context/AuthContext';
import CongViecForm from './CongViecForm';
import './CongViecList.css';

const CongViecList = () => {
  const { user } = useAuth();
  const [congViecList, setCongViecList] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [filterTrangThai, setFilterTrangThai] = useState('all');
  const [filterUser, setFilterUser] = useState('all'); // 'all', 'my-tasks', 'created', 'assigned'
  const [showForm, setShowForm] = useState(false);

  const loadCongViec = async () => {
    try {
      setLoading(true);
      let data;
      
      // Load based on user filter
      switch (filterUser) {
        case 'my-tasks':
          data = await congViecService.getMyTasks();
          break;
        case 'created':
          data = await congViecService.getCreatedByMe();
          break;
        case 'assigned':
          data = await congViecService.getAssignedToMe();
          break;
        default:
          data = await congViecService.getAll();
      }
      
      setCongViecList(data);
    } catch {
      setError('Failed to load công việc');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadCongViec();
  }, [filterUser]); // eslint-disable-line react-hooks/exhaustive-deps

  const handleDelete = async (id) => {
    if (window.confirm('Bạn có chắc chắn muốn xóa công việc này?')) {
      try {
        await congViecService.delete(id);
        loadCongViec();
      } catch {
        alert('Failed to delete công việc');
      }
    }
  };

  const handleFilterChange = (trangThai) => {
    setFilterTrangThai(trangThai);
  };

  const handleUserFilterChange = (filter) => {
    setFilterUser(filter);
  };

  const handleFormSuccess = () => {
    setShowForm(false);
    loadCongViec();
  };

  const handleFormCancel = () => {
    setShowForm(false);
  };

  const getFilteredCongViec = () => {
    if (filterTrangThai === 'all') {
      return congViecList;
    }
    return congViecList.filter(cv => cv.trangThai === filterTrangThai);
  };

  const getPriorityClass = (doUuTien) => {
    const priorityMap = {
      'KhanCap': 'priority-urgent',
      'Cao': 'priority-high',
      'TrungBinh': 'priority-medium',
      'Thap': 'priority-low'
    };
    return priorityMap[doUuTien] || 'priority-medium';
  };

  const getStatusClass = (trangThai) => {
    const statusMap = {
      'Mới': 'status-new',
      'ĐangThucHien': 'status-in-progress',
      'HoanThanh': 'status-completed',
      'Huy': 'status-cancelled'
    };
    return statusMap[trangThai] || 'status-new';
  };

  const formatDate = (dateString) => {
    if (!dateString) return 'N/A';
    const date = new Date(dateString);
    return date.toLocaleDateString('vi-VN');
  };

  if (loading) return <div className="loading">Đang tải công việc...</div>;
  if (error) return <div className="error">{error}</div>;

  const filteredCongViec = getFilteredCongViec();

  return (
    <div className="congviec-list">
      <div className="page-header">
        <h1>Quản Lý Công Việc</h1>
        <button 
          className="btn-add"
          onClick={() => setShowForm(!showForm)}
        >
          {showForm ? 'Hủy' : 'Thêm công việc'}
        </button>
      </div>

      {showForm && (
        <CongViecForm 
          onSuccess={handleFormSuccess}
          onCancel={handleFormCancel}
        />
      )}

      <div className="filter-section">
        <div className="filter-group">
          <label>Hiển thị công việc:</label>
          <div className="filter-bar">
            <button 
              className={filterUser === 'all' ? 'filter-btn active' : 'filter-btn'}
              onClick={() => handleUserFilterChange('all')}
            >
              Tất cả
            </button>
            <button 
              className={filterUser === 'my-tasks' ? 'filter-btn active' : 'filter-btn'}
              onClick={() => handleUserFilterChange('my-tasks')}
            >
              Công việc của tôi
            </button>
            <button 
              className={filterUser === 'created' ? 'filter-btn active' : 'filter-btn'}
              onClick={() => handleUserFilterChange('created')}
            >
              Tôi đã tạo
            </button>
            <button 
              className={filterUser === 'assigned' ? 'filter-btn active' : 'filter-btn'}
              onClick={() => handleUserFilterChange('assigned')}
            >
              Được giao cho tôi
            </button>
          </div>
        </div>

        <div className="filter-group">
          <label>Lọc theo trạng thái:</label>
          <div className="filter-bar">
            <button 
              className={filterTrangThai === 'all' ? 'filter-btn active' : 'filter-btn'}
              onClick={() => handleFilterChange('all')}
            >
              Tất cả
            </button>
            <button 
              className={filterTrangThai === 'Mới' ? 'filter-btn active' : 'filter-btn'}
              onClick={() => handleFilterChange('Mới')}
            >
              Mới
            </button>
            <button 
              className={filterTrangThai === 'ĐangThucHien' ? 'filter-btn active' : 'filter-btn'}
              onClick={() => handleFilterChange('ĐangThucHien')}
            >
              Đang thực hiện
            </button>
            <button 
              className={filterTrangThai === 'HoanThanh' ? 'filter-btn active' : 'filter-btn'}
              onClick={() => handleFilterChange('HoanThanh')}
            >
              Hoàn thành
            </button>
            <button 
              className={filterTrangThai === 'Huy' ? 'filter-btn active' : 'filter-btn'}
              onClick={() => handleFilterChange('Huy')}
            >
              Hủy
            </button>
          </div>
        </div>
      </div>

      <div className="congviec-grid">
        {filteredCongViec.map(congViec => (
          <div key={congViec.id} className="congviec-card">
            <div className="card-header">
              <h3>{congViec.tenCongViec}</h3>
              <div className="badges">
                <span className={`badge ${getPriorityClass(congViec.doUuTien)}`}>
                  {congViec.doUuTien}
                </span>
                <span className={`badge ${getStatusClass(congViec.trangThai)}`}>
                  {congViec.trangThai}
                </span>
              </div>
            </div>
            
            <p className="congviec-description">{congViec.moTa}</p>
            
            <div className="congviec-details">
              <div className="detail-row">
                <span className="label">Ngày bắt đầu:</span>
                <span className="value">{formatDate(congViec.ngayBatDau)}</span>
              </div>
              {congViec.ngayKetThuc && (
                <div className="detail-row">
                  <span className="label">Ngày kết thúc:</span>
                  <span className="value">{formatDate(congViec.ngayKetThuc)}</span>
                </div>
              )}
              {congViec.nguoiPhuTrachId && (
                <div className="detail-row">
                  <span className="label">Người phụ trách:</span>
                  <span className="value">User #{congViec.nguoiPhuTrachId}</span>
                </div>
              )}
              {congViec.nguoiTaoId && (
                <div className="detail-row">
                  <span className="label">Người tạo:</span>
                  <span className="value">
                    {congViec.nguoiTaoId === user?.id ? 'Bạn' : `User #${congViec.nguoiTaoId}`}
                  </span>
                </div>
              )}
            </div>

            <div className="card-actions">
              <button 
                onClick={() => handleDelete(congViec.id)}
                className="btn-delete"
              >
                Xóa
              </button>
            </div>
          </div>
        ))}
      </div>

      {filteredCongViec.length === 0 && (
        <div className="empty-state">
          <p>Không có công việc nào</p>
        </div>
      )}
    </div>
  );
};

export default CongViecList;
