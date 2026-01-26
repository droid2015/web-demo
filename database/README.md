# Database - Oracle Schema

Oracle database scripts for the platform core and modules.

## Setup

### Prerequisites

- Oracle Database 12c or higher
- SQL*Plus or any Oracle client

### Using Docker

```bash
docker run -d -p 1521:1521 -e ORACLE_PASSWORD=oracle \
  container-registry.oracle.com/database/express:latest
```

Wait for Oracle to start (check logs with `docker logs <container-id>`).

## Running Migration Scripts

### Core Schema

Run scripts in order:

```bash
sqlplus SYSTEM/oracle@localhost:1521/XE
```

Then execute each script:

```sql
@database/core/01_create_users_table.sql
@database/core/02_create_roles_permissions_tables.sql
@database/core/03_create_modules_table.sql
@database/core/04_create_audit_logs_table.sql
@database/core/05_create_system_config_table.sql
@database/core/06_seed_initial_data.sql
```

### Module Schemas

For each module, run its migration scripts:

```sql
@database/modules/product_management/01_create_products_table.sql
@database/modules/product_management/02_seed_sample_products.sql
```

## Schema Overview

### Core Tables

- **USERS** - System users
  - Id (PK, auto-increment)
  - Username (unique)
  - Email (unique)
  - PasswordHash
  - IsActive
  - CreatedAt

- **ROLES** - User roles
  - Id (PK)
  - Name (unique)
  - Description

- **PERMISSIONS** - System permissions
  - Id (PK)
  - Name (unique)
  - Resource
  - Action

- **USER_ROLES** - User-Role mapping
  - UserId (FK)
  - RoleId (FK)

- **ROLE_PERMISSIONS** - Role-Permission mapping
  - RoleId (FK)
  - PermissionId (FK)

- **MODULES** - Platform modules
  - Id (PK)
  - Name (unique)
  - Version
  - IsEnabled
  - LoadOrder

- **AUDIT_LOGS** - Audit trail
  - Id (PK)
  - UserId (FK)
  - Action
  - EntityType
  - EntityId
  - Changes
  - CreatedAt

- **SYSTEM_CONFIG** - System configuration
  - Key (PK)
  - Value
  - Description

### Module Tables

#### Product Management Module

- **PRODUCTS**
  - Id (PK)
  - Name
  - Description
  - Price
  - StockQuantity
  - IsActive
  - CreatedAt
  - UpdatedAt

## Initial Data

The seed script creates:

- Admin user (username: `admin`, password: `Admin@123`)
- 3 roles: Admin, Manager, User
- 8 permissions for users and products
- 2 modules: Core, ProductManagement
- System configuration entries
- 10 sample products

## Connection String

Default connection string format:
```
User Id=SYSTEM;Password=oracle;Data Source=localhost:1521/XE
```

## Backup and Restore

### Backup

```bash
exp SYSTEM/oracle file=platform_backup.dmp owner=SYSTEM
```

### Restore

```bash
imp SYSTEM/oracle file=platform_backup.dmp
```

## Troubleshooting

### Cannot connect to Oracle

- Check if Oracle service is running
- Verify port 1521 is open
- Check connection string format

### Tables already exist

Drop existing tables before running migration scripts:

```sql
DROP TABLE USER_ROLES CASCADE CONSTRAINTS;
DROP TABLE ROLE_PERMISSIONS CASCADE CONSTRAINTS;
DROP TABLE AUDIT_LOGS CASCADE CONSTRAINTS;
DROP TABLE PRODUCTS CASCADE CONSTRAINTS;
DROP TABLE PERMISSIONS CASCADE CONSTRAINTS;
DROP TABLE ROLES CASCADE CONSTRAINTS;
DROP TABLE MODULES CASCADE CONSTRAINTS;
DROP TABLE SYSTEM_CONFIG CASCADE CONSTRAINTS;
DROP TABLE USERS CASCADE CONSTRAINTS;
```

## Adding New Module Schema

1. Create directory: `database/modules/your_module/`
2. Create migration scripts numbered sequentially
3. Follow naming convention: `01_create_table.sql`
4. Add foreign keys to core tables if needed
5. Document schema in module README
