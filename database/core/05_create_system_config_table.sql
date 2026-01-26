-- Create System Config Table
CREATE TABLE SYSTEM_CONFIG (
    Key VARCHAR2(100) PRIMARY KEY,
    Value VARCHAR2(1000) NOT NULL,
    Description VARCHAR2(500)
);

CREATE INDEX idx_system_config_key ON SYSTEM_CONFIG(Key);
