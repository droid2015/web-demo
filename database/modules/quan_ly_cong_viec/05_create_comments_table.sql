-- Create CongViec Comments table
CREATE TABLE CONG_VIEC_COMMENTS (
    Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    CongViecId NUMBER NOT NULL,
    UserId NUMBER NOT NULL,
    Content VARCHAR2(4000) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT SYSTIMESTAMP NOT NULL,
    UpdatedAt TIMESTAMP,
    CONSTRAINT fk_comment_congviec FOREIGN KEY (CongViecId) REFERENCES CONG_VIEC(Id) ON DELETE CASCADE,
    CONSTRAINT fk_comment_user FOREIGN KEY (UserId) REFERENCES USERS(Id) ON DELETE CASCADE
);

-- Create indexes for better query performance
CREATE INDEX idx_comments_congviecid ON CONG_VIEC_COMMENTS(CongViecId);
CREATE INDEX idx_comments_userid ON CONG_VIEC_COMMENTS(UserId);
CREATE INDEX idx_comments_createdat ON CONG_VIEC_COMMENTS(CreatedAt);

-- Insert sample comments
INSERT INTO CONG_VIEC_COMMENTS (CongViecId, UserId, Content, CreatedAt)
SELECT 1, 1, 'Đã bắt đầu làm công việc này', SYSTIMESTAMP - INTERVAL '2' DAY
FROM DUAL WHERE EXISTS (SELECT 1 FROM CONG_VIEC WHERE Id = 1);

INSERT INTO CONG_VIEC_COMMENTS (CongViecId, UserId, Content, CreatedAt)
SELECT 1, 1, 'Đã hoàn thành 50% công việc', SYSTIMESTAMP - INTERVAL '1' DAY
FROM DUAL WHERE EXISTS (SELECT 1 FROM CONG_VIEC WHERE Id = 1);

INSERT INTO CONG_VIEC_COMMENTS (CongViecId, UserId, Content, CreatedAt)
SELECT 2, 1, 'Cần hỗ trợ thêm tài nguyên cho công việc này', SYSTIMESTAMP - INTERVAL '3' HOUR
FROM DUAL WHERE EXISTS (SELECT 1 FROM CONG_VIEC WHERE Id = 2);

COMMIT;
