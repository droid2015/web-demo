import { useState } from 'react';
import { congViecService } from '../../../services/congViecService';
import './CongViecForm.css';

const CongViecForm = ({ onSuccess, onCancel }) => {
  const [formData, setFormData] = useState({
    tenCongViec: '',
    moTa: '',
    trangThai: 'Mới',
    doUuTien: 'TrungBinh',
    nguoiPhuTrachId: '',
    ngayBatDau: new Date().toISOString().split('T')[0],
    ngayKetThuc: '',
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    try {
      // Validate date range
      if (formData.ngayKetThuc && formData.ngayKetThuc < formData.ngayBatDau) {
        setError('Ngày kết thúc không thể trước ngày bắt đầu');
        setLoading(false);
        return;
      }

      // Convert dates to proper format for API
      const dataToSubmit = {
        ...formData,
        nguoiPhuTrachId: formData.nguoiPhuTrachId ? parseInt(formData.nguoiPhuTrachId) : null,
        ngayBatDau: new Date(formData.ngayBatDau).toISOString(),
        ngayKetThuc: formData.ngayKetThuc ? new Date(formData.ngayKetThuc).toISOString() : null,
      };

      await congViecService.create(dataToSubmit);
      onSuccess();
    } catch (err) {
      setError(err.response?.data?.message || 'Không thể tạo công việc');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="congviec-form-container">
      <form onSubmit={handleSubmit} className="congviec-form">
        <h2>Thêm Công Việc Mới</h2>
        
        {error && <div className="error-message">{error}</div>}

        <div className="form-group">
          <label htmlFor="tenCongViec">Tên công việc *</label>
          <input
            type="text"
            id="tenCongViec"
            name="tenCongViec"
            value={formData.tenCongViec}
            onChange={handleChange}
            required
            disabled={loading}
            placeholder="Nhập tên công việc"
          />
        </div>

        <div className="form-group">
          <label htmlFor="moTa">Mô tả</label>
          <textarea
            id="moTa"
            name="moTa"
            value={formData.moTa}
            onChange={handleChange}
            disabled={loading}
            placeholder="Nhập mô tả công việc"
            rows="4"
          />
        </div>

        <div className="form-row">
          <div className="form-group">
            <label htmlFor="trangThai">Trạng thái</label>
            <select
              id="trangThai"
              name="trangThai"
              value={formData.trangThai}
              onChange={handleChange}
              disabled={loading}
            >
              <option value="Mới">Mới</option>
              <option value="ĐangThucHien">Đang thực hiện</option>
              <option value="HoanThanh">Hoàn thành</option>
              <option value="Huy">Hủy</option>
            </select>
          </div>

          <div className="form-group">
            <label htmlFor="doUuTien">Độ ưu tiên</label>
            <select
              id="doUuTien"
              name="doUuTien"
              value={formData.doUuTien}
              onChange={handleChange}
              disabled={loading}
            >
              <option value="Thap">Thấp</option>
              <option value="TrungBinh">Trung bình</option>
              <option value="Cao">Cao</option>
              <option value="KhanCap">Khẩn cấp</option>
            </select>
          </div>
        </div>

        <div className="form-row">
          <div className="form-group">
            <label htmlFor="ngayBatDau">Ngày bắt đầu *</label>
            <input
              type="date"
              id="ngayBatDau"
              name="ngayBatDau"
              value={formData.ngayBatDau}
              onChange={handleChange}
              required
              disabled={loading}
            />
          </div>

          <div className="form-group">
            <label htmlFor="ngayKetThuc">Ngày kết thúc</label>
            <input
              type="date"
              id="ngayKetThuc"
              name="ngayKetThuc"
              value={formData.ngayKetThuc}
              onChange={handleChange}
              disabled={loading}
              min={formData.ngayBatDau}
            />
          </div>
        </div>

        <div className="form-group">
          <label htmlFor="nguoiPhuTrachId">ID người phụ trách</label>
          <input
            type="number"
            id="nguoiPhuTrachId"
            name="nguoiPhuTrachId"
            value={formData.nguoiPhuTrachId}
            onChange={handleChange}
            disabled={loading}
            placeholder="Nhập ID người phụ trách"
            min="1"
          />
        </div>

        <div className="form-actions">
          <button 
            type="button" 
            onClick={onCancel} 
            className="btn-cancel"
            disabled={loading}
          >
            Hủy
          </button>
          <button 
            type="submit" 
            className="btn-submit"
            disabled={loading}
          >
            {loading ? 'Đang tạo...' : 'Tạo công việc'}
          </button>
        </div>
      </form>
    </div>
  );
};

export default CongViecForm;
