-- Seed initial data for the platform

-- Insert default admin user
-- Password: Admin@123 (hashed with BCrypt)
INSERT INTO USERS (Username, Email, PasswordHash, IsActive)
VALUES ('admin', 'admin@platform.com', '$2a$11$UwG9rOQS7vHGQTlLBdRFpOJ0qPYh1IQJwYb6Z3J5M7.xQPwGkQdFK', 1);

-- Insert roles
INSERT INTO ROLES (Name, Description) VALUES ('Admin', 'System Administrator with full access');
INSERT INTO ROLES (Name, Description) VALUES ('Manager', 'Manager with extended permissions');
INSERT INTO ROLES (Name, Description) VALUES ('User', 'Standard user with basic permissions');

-- Insert permissions
INSERT INTO PERMISSIONS (Name, ResourceName, Action) VALUES ('user.create', 'user', 'create');
INSERT INTO PERMISSIONS (Name, ResourceName, Action) VALUES ('user.read', 'user', 'read');
INSERT INTO PERMISSIONS (Name, ResourceName, Action) VALUES ('user.update', 'user', 'update');
INSERT INTO PERMISSIONS (Name, ResourceName, Action) VALUES ('user.delete', 'user', 'delete');
INSERT INTO PERMISSIONS (Name, ResourceName, Action) VALUES ('product.create', 'product', 'create');
INSERT INTO PERMISSIONS (Name, ResourceName, Action) VALUES ('product.read', 'product', 'read');
INSERT INTO PERMISSIONS (Name, ResourceName, Action) VALUES ('product.update', 'product', 'update');
INSERT INTO PERMISSIONS (Name, ResourceName, Action) VALUES ('product.delete', 'product', 'delete');

-- Assign admin user to Admin role
INSERT INTO USER_ROLES (UserId, RoleId)
SELECT u.Id, r.Id 
FROM USERS u, ROLES r 
WHERE u.Username = 'admin' AND r.Name = 'Admin';

-- Assign all permissions to Admin role
INSERT INTO ROLE_PERMISSIONS (RoleId, PermissionId)
SELECT r.Id, p.Id 
FROM ROLES r, PERMISSIONS p 
WHERE r.Name = 'Admin';

-- Assign read permissions to User role
INSERT INTO ROLE_PERMISSIONS (RoleId, PermissionId)
SELECT r.Id, p.Id 
FROM ROLES r, PERMISSIONS p 
WHERE r.Name = 'User' AND p.Action = 'read';

-- Insert modules
INSERT INTO MODULES (Name, Version, IsEnabled, LoadOrder) 
VALUES ('Core', '1.0.0', 1, 0);

INSERT INTO MODULES (Name, Version, IsEnabled, LoadOrder) 
VALUES ('ProductManagement', '1.0.0', 1, 1);

-- Insert system config
INSERT INTO SYSTEM_CONFIG (Key, Value, Description) 
VALUES ('APP_NAME', 'Platform', 'Application name');

INSERT INTO SYSTEM_CONFIG (Key, Value, Description) 
VALUES ('APP_VERSION', '1.0.0', 'Application version');

COMMIT;
