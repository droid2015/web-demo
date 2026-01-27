# Module QuanLyCongViec (Task Management)

## Mô tả
Module Quản Lý Công Việc cung cấp chức năng quản lý và theo dõi tiến độ công việc trong hệ thống Platform.

## Tính năng chính
- Tạo, đọc, cập nhật và xóa công việc
- Phân loại công việc theo trạng thái (Mới, Đang thực hiện, Hoàn thành, Hủy)
- Phân loại công việc theo độ ưu tiên (Thấp, Trung bình, Cao, Khẩn cấp)
- Gán công việc cho người phụ trách
- Theo dõi thời gian bắt đầu và kết thúc

## Database Schema

### Table: CONG_VIEC

| Column | Type | Description |
|--------|------|-------------|
| Id | NUMBER | Primary key, auto-generated |
| TenCongViec | NVARCHAR2(200) | Task name (required) |
| MoTa | NVARCHAR2(2000) | Task description |
| TrangThai | NVARCHAR2(50) | Status: Mới, ĐangThucHien, HoanThanh, Huy |
| DoUuTien | NVARCHAR2(50) | Priority: Thap, TrungBinh, Cao, KhanCap |
| NguoiPhuTrachId | NUMBER | Assigned user ID (FK to USERS) |
| NgayBatDau | TIMESTAMP | Start date (required) |
| NgayKetThuc | TIMESTAMP | End date (optional) |
| NgayTao | TIMESTAMP | Created timestamp |
| NgayCapNhat | TIMESTAMP | Last updated timestamp |

### Indexes
- `IDX_CongViec_TrangThai` - Index on TrangThai for filtering by status
- `IDX_CongViec_DoUuTien` - Index on DoUuTien for filtering by priority
- `IDX_CongViec_NguoiPhuTrach` - Index on NguoiPhuTrachId for filtering by assignee
- `IDX_CongViec_NgayBatDau` - Index on NgayBatDau for date range queries

## API Endpoints

### GET /api/congviec
Lấy tất cả công việc

**Response:**
```json
[
  {
    "id": 1,
    "tenCongViec": "Task name",
    "moTa": "Description",
    "trangThai": "Mới",
    "doUuTien": "Cao",
    "nguoiPhuTrachId": 1,
    "ngayBatDau": "2024-01-01T09:00:00Z",
    "ngayKetThuc": "2024-01-15T17:00:00Z",
    "ngayTao": "2024-01-01T08:00:00Z",
    "ngayCapNhat": null
  }
]
```

### GET /api/congviec/{id}
Lấy công việc theo ID

### GET /api/congviec/trangthai/{trangThai}
Lọc công việc theo trạng thái

**Example:** `/api/congviec/trangthai/ĐangThucHien`

### POST /api/congviec
Tạo công việc mới

**Request Body:**
```json
{
  "tenCongViec": "New task",
  "moTa": "Description",
  "trangThai": "Mới",
  "doUuTien": "TrungBinh",
  "nguoiPhuTrachId": 1,
  "ngayBatDau": "2024-02-01T09:00:00Z"
}
```

### PUT /api/congviec/{id}
Cập nhật công việc

### DELETE /api/congviec/{id}
Xóa công việc

## Cài đặt

1. Chạy script tạo bảng:
   ```sql
   @01_create_cong_viec_table.sql
   ```

2. Chạy script seed data (optional):
   ```sql
   @02_seed_sample_data.sql
   ```

3. Đăng ký module:
   ```sql
   @03_register_module.sql
   ```

## Sử dụng

Module tự động được discover và initialize khi ứng dụng khởi động nếu IsEnabled = 1 trong bảng MODULES.

Tất cả API endpoints yêu cầu JWT authentication (header: `Authorization: Bearer <token>`).

## Phiên bản
- **Version:** 1.0.0
- **Load Order:** 2
