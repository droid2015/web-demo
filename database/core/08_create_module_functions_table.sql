-- Create Module Functions table
-- This table defines functions/features within each module
CREATE TABLE MODULE_FUNCTIONS (
    Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ModuleId NUMBER NOT NULL,
    Name VARCHAR2(200) NOT NULL,
    Code VARCHAR2(100) NOT NULL,
    Description VARCHAR2(1000),
    IsEnabled NUMBER(1) DEFAULT 1 NOT NULL,
    CONSTRAINT fk_module_functions_module FOREIGN KEY (ModuleId) REFERENCES MODULES(Id) ON DELETE CASCADE,
    CONSTRAINT uk_module_function_code UNIQUE (ModuleId, Code)
);

-- Create indexes
CREATE INDEX idx_module_functions_moduleid ON MODULE_FUNCTIONS(ModuleId);
CREATE INDEX idx_module_functions_code ON MODULE_FUNCTIONS(Code);
CREATE INDEX idx_module_functions_enabled ON MODULE_FUNCTIONS(IsEnabled);

-- Insert sample functions for QuanLyCongViec module
INSERT INTO MODULE_FUNCTIONS (ModuleId, Name, Code, Description, IsEnabled)
SELECT m.Id, 'Tạo công việc', 'CREATE_TASK', 'Cho phép tạo công việc mới', 1
FROM MODULES m WHERE m.Name = 'QuanLyCongViec';

INSERT INTO MODULE_FUNCTIONS (ModuleId, Name, Code, Description, IsEnabled)
SELECT m.Id, 'Xem công việc', 'VIEW_TASK', 'Cho phép xem danh sách và chi tiết công việc', 1
FROM MODULES m WHERE m.Name = 'QuanLyCongViec';

INSERT INTO MODULE_FUNCTIONS (ModuleId, Name, Code, Description, IsEnabled)
SELECT m.Id, 'Cập nhật công việc', 'UPDATE_TASK', 'Cho phép cập nhật thông tin công việc', 1
FROM MODULES m WHERE m.Name = 'QuanLyCongViec';

INSERT INTO MODULE_FUNCTIONS (ModuleId, Name, Code, Description, IsEnabled)
SELECT m.Id, 'Xóa công việc', 'DELETE_TASK', 'Cho phép xóa công việc', 1
FROM MODULES m WHERE m.Name = 'QuanLyCongViec';

INSERT INTO MODULE_FUNCTIONS (ModuleId, Name, Code, Description, IsEnabled)
SELECT m.Id, 'Gán công việc', 'ASSIGN_TASK', 'Cho phép gán công việc cho người khác', 1
FROM MODULES m WHERE m.Name = 'QuanLyCongViec';

INSERT INTO MODULE_FUNCTIONS (ModuleId, Name, Code, Description, IsEnabled)
SELECT m.Id, 'Bình luận công việc', 'COMMENT_TASK', 'Cho phép bình luận vào công việc', 1
FROM MODULES m WHERE m.Name = 'QuanLyCongViec';

-- Insert sample functions for ProductManagement module
INSERT INTO MODULE_FUNCTIONS (ModuleId, Name, Code, Description, IsEnabled)
SELECT m.Id, 'Tạo sản phẩm', 'CREATE_PRODUCT', 'Cho phép tạo sản phẩm mới', 1
FROM MODULES m WHERE m.Name = 'ProductManagement';

INSERT INTO MODULE_FUNCTIONS (ModuleId, Name, Code, Description, IsEnabled)
SELECT m.Id, 'Xem sản phẩm', 'VIEW_PRODUCT', 'Cho phép xem danh sách và chi tiết sản phẩm', 1
FROM MODULES m WHERE m.Name = 'ProductManagement';

INSERT INTO MODULE_FUNCTIONS (ModuleId, Name, Code, Description, IsEnabled)
SELECT m.Id, 'Cập nhật sản phẩm', 'UPDATE_PRODUCT', 'Cho phép cập nhật thông tin sản phẩm', 1
FROM MODULES m WHERE m.Name = 'ProductManagement';

INSERT INTO MODULE_FUNCTIONS (ModuleId, Name, Code, Description, IsEnabled)
SELECT m.Id, 'Xóa sản phẩm', 'DELETE_PRODUCT', 'Cho phép xóa sản phẩm', 1
FROM MODULES m WHERE m.Name = 'ProductManagement';

COMMIT;
