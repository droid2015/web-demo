-- Seed sample data for CONG_VIEC table
-- Insert 10 sample tasks with different statuses and priorities

INSERT INTO CONG_VIEC (TenCongViec, MoTa, TrangThai, DoUuTien, NguoiPhuTrachId, NgayBatDau, NgayKetThuc)
VALUES ('Phát triển tính năng đăng nhập', 'Implement JWT authentication cho hệ thống', 'HoanThanh', 'Cao', 1, 
        TO_TIMESTAMP('2024-01-01 09:00:00', 'YYYY-MM-DD HH24:MI:SS'), 
        TO_TIMESTAMP('2024-01-15 17:00:00', 'YYYY-MM-DD HH24:MI:SS'));

INSERT INTO CONG_VIEC (TenCongViec, MoTa, TrangThai, DoUuTien, NguoiPhuTrachId, NgayBatDau, NgayKetThuc)
VALUES ('Thiết kế database schema', 'Thiết kế cấu trúc database cho module QuanLyCongViec', 'HoanThanh', 'Cao', 1,
        TO_TIMESTAMP('2024-01-05 09:00:00', 'YYYY-MM-DD HH24:MI:SS'),
        TO_TIMESTAMP('2024-01-10 17:00:00', 'YYYY-MM-DD HH24:MI:SS'));

INSERT INTO CONG_VIEC (TenCongViec, MoTa, TrangThai, DoUuTien, NguoiPhuTrachId, NgayBatDau, NgayKetThuc)
VALUES ('Xây dựng API endpoints', 'Tạo REST API cho quản lý công việc', 'ĐangThucHien', 'Cao', 2,
        TO_TIMESTAMP('2024-01-20 09:00:00', 'YYYY-MM-DD HH24:MI:SS'),
        TO_TIMESTAMP('2024-02-05 17:00:00', 'YYYY-MM-DD HH24:MI:SS'));

INSERT INTO CONG_VIEC (TenCongViec, MoTa, TrangThai, DoUuTien, NguoiPhuTrachId, NgayBatDau)
VALUES ('Phát triển giao diện người dùng', 'Xây dựng React components cho module công việc', 'ĐangThucHien', 'TrungBinh', 2,
        TO_TIMESTAMP('2024-01-25 09:00:00', 'YYYY-MM-DD HH24:MI:SS'));

INSERT INTO CONG_VIEC (TenCongViec, MoTa, TrangThai, DoUuTien, NguoiPhuTrachId, NgayBatDau)
VALUES ('Viết unit tests', 'Tạo unit tests cho CongViecService', 'Mới', 'TrungBinh', NULL,
        TO_TIMESTAMP('2024-02-01 09:00:00', 'YYYY-MM-DD HH24:MI:SS'));

INSERT INTO CONG_VIEC (TenCongViec, MoTa, TrangThai, DoUuTien, NguoiPhuTrachId, NgayBatDau)
VALUES ('Tối ưu hóa performance', 'Optimize database queries và caching', 'Mới', 'Thap', NULL,
        TO_TIMESTAMP('2024-02-10 09:00:00', 'YYYY-MM-DD HH24:MI:SS'));

INSERT INTO CONG_VIEC (TenCongViec, MoTa, TrangThai, DoUuTien, NguoiPhuTrachId, NgayBatDau, NgayKetThuc)
VALUES ('Fix bug đăng nhập', 'Sửa lỗi timeout khi đăng nhập', 'HoanThanh', 'KhanCap', 1,
        TO_TIMESTAMP('2024-01-18 14:00:00', 'YYYY-MM-DD HH24:MI:SS'),
        TO_TIMESTAMP('2024-01-18 16:30:00', 'YYYY-MM-DD HH24:MI:SS'));

INSERT INTO CONG_VIEC (TenCongViec, MoTa, TrangThai, DoUuTien, NguoiPhuTrachId, NgayBatDau)
VALUES ('Cập nhật documentation', 'Update API documentation và user guide', 'Mới', 'Thap', NULL,
        TO_TIMESTAMP('2024-02-15 09:00:00', 'YYYY-MM-DD HH24:MI:SS'));

INSERT INTO CONG_VIEC (TenCongViec, MoTa, TrangThai, DoUuTien, NguoiPhuTrachId, NgayBatDau, NgayKetThuc)
VALUES ('Code review module Products', 'Review code cho ProductManagement module', 'Huy', 'TrungBinh', 2,
        TO_TIMESTAMP('2024-01-12 10:00:00', 'YYYY-MM-DD HH24:MI:SS'),
        TO_TIMESTAMP('2024-01-12 11:00:00', 'YYYY-MM-DD HH24:MI:SS'));

INSERT INTO CONG_VIEC (TenCongViec, MoTa, TrangThai, DoUuTien, NguoiPhuTrachId, NgayBatDau)
VALUES ('Setup CI/CD pipeline', 'Configure GitHub Actions cho automated testing', 'ĐangThucHien', 'Cao', 1,
        TO_TIMESTAMP('2024-01-28 09:00:00', 'YYYY-MM-DD HH24:MI:SS'));

COMMIT;
