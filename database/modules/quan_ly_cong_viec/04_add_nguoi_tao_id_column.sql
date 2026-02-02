-- Add NguoiTaoId column to CONG_VIEC table for tracking the creator of each task
ALTER TABLE CONG_VIEC ADD (
    NguoiTaoId NUMBER
);

-- Add foreign key constraint
ALTER TABLE CONG_VIEC ADD CONSTRAINT FK_CongViec_NguoiTao 
    FOREIGN KEY (NguoiTaoId) REFERENCES USERS(Id);

-- Create index for better query performance
CREATE INDEX IDX_CongViec_NguoiTao ON CONG_VIEC(NguoiTaoId);

-- Update existing records to set NguoiTaoId to the first admin user (Id = 1)
-- This is a default value for existing data
UPDATE CONG_VIEC SET NguoiTaoId = 1 WHERE NguoiTaoId IS NULL;

-- Make the column NOT NULL after setting default values
ALTER TABLE CONG_VIEC MODIFY NguoiTaoId NUMBER NOT NULL;

COMMIT;
