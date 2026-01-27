import { useState, useEffect } from 'react';
import { congViecService } from '../../../services/congViecService';
import './CongViecList.css';

const CongViecList = () => {
  const [congViecList, setCongViecList] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [filterTrangThai, setFilterTrangThai] = useState('all');

  useEffect(() => {
    loadCongViec();
  }, []);

  const loadCongViec = async () => {
    try {
      setLoading(true);
      const data = await congViecService.getAll();
      setCongViecList(data);
    } catch (err) {
      setError('Failed to load công việc');
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Bạn có chắc chắn muốn xóa công việc này?')) {
      try {
        await congViecService.delete(id);
        loadCongViec();
      } catch (err) {
        alert('Failed to delete công việc');
      }
    }
  };

  const handleFilterChange = (trangThai) => {
    setFilterTrangThai(trangThai);
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
      </div>

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
