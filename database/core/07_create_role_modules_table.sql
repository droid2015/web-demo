-- Create Role_Modules Junction Table
-- This table links roles to modules, allowing role-based module access control
CREATE TABLE ROLE_MODULES (
    RoleId NUMBER NOT NULL,
    ModuleId NUMBER NOT NULL,
    PRIMARY KEY (RoleId, ModuleId),
    FOREIGN KEY (RoleId) REFERENCES ROLES(Id) ON DELETE CASCADE,
    FOREIGN KEY (ModuleId) REFERENCES MODULES(Id) ON DELETE CASCADE
);

CREATE INDEX idx_role_modules_roleid ON ROLE_MODULES(RoleId);
CREATE INDEX idx_role_modules_moduleid ON ROLE_MODULES(ModuleId);

-- Grant all modules to Admin role by default
INSERT INTO ROLE_MODULES (RoleId, ModuleId)
SELECT r.Id, m.Id 
FROM ROLES r, MODULES m 
WHERE r.Name = 'Admin';

-- Grant Core and ProductManagement modules to Manager and User roles
INSERT INTO ROLE_MODULES (RoleId, ModuleId)
SELECT r.Id, m.Id 
FROM ROLES r, MODULES m 
WHERE r.Name IN ('Manager', 'User') AND m.Name IN ('Core', 'ProductManagement');

COMMIT;
