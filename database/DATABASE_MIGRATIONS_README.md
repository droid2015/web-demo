# Module Functions and Task Comments - Database Setup

## Overview
This directory contains SQL migration scripts for the new module function management and task comments features.

## Migration Scripts

### Core Schema (Run First)

#### 08_create_module_functions_table.sql
Creates the MODULE_FUNCTIONS table to store function definitions for each module.

**What it does:**
- Creates MODULE_FUNCTIONS table with foreign key to MODULES
- Adds indexes for performance
- Seeds sample functions for QuanLyCongViec and ProductManagement modules

**Tables created:**
- `MODULE_FUNCTIONS` - Stores module function definitions

**Sample data:**
- 6 functions for QuanLyCongViec module (CREATE_TASK, VIEW_TASK, etc.)
- 4 functions for ProductManagement module (CREATE_PRODUCT, VIEW_PRODUCT, etc.)

### QuanLyCongViec Module (Run Second)

#### 05_create_comments_table.sql
Creates the CONG_VIEC_COMMENTS table for task commenting functionality.

**What it does:**
- Creates CONG_VIEC_COMMENTS table with foreign keys to CONG_VIEC and USERS
- Adds indexes for query performance
- Seeds 3 sample comments

**Tables created:**
- `CONG_VIEC_COMMENTS` - Stores comments on tasks

## Installation Instructions

### Using SQL*Plus or Oracle SQL Developer

1. Connect to your Oracle database
2. Run the core schema script:
```sql
@/path/to/database/core/08_create_module_functions_table.sql
```

3. Run the QuanLyCongViec module script:
```sql
@/path/to/database/modules/quan_ly_cong_viec/05_create_comments_table.sql
```

### Using Command Line

```bash
# Core schema
sqlplus username/password@database @database/core/08_create_module_functions_table.sql

# QuanLyCongViec module
sqlplus username/password@database @database/modules/quan_ly_cong_viec/05_create_comments_table.sql
```

## Table Schemas

### MODULE_FUNCTIONS

| Column | Type | Description |
|--------|------|-------------|
| Id | NUMBER | Primary key (auto-generated) |
| ModuleId | NUMBER | Foreign key to MODULES table |
| Name | VARCHAR2(200) | Function display name |
| Code | VARCHAR2(100) | Unique function code |
| Description | VARCHAR2(1000) | Function description |
| IsEnabled | NUMBER(1) | 1 = enabled, 0 = disabled |

**Indexes:**
- `idx_module_functions_moduleid` - For filtering by module
- `idx_module_functions_code` - For code lookups
- `idx_module_functions_enabled` - For filtering enabled functions

**Constraints:**
- `fk_module_functions_module` - Foreign key to MODULES
- `uk_module_function_code` - Unique (ModuleId, Code)

### CONG_VIEC_COMMENTS

| Column | Type | Description |
|--------|------|-------------|
| Id | NUMBER | Primary key (auto-generated) |
| CongViecId | NUMBER | Foreign key to CONG_VIEC table |
| UserId | NUMBER | Foreign key to USERS table |
| Content | VARCHAR2(4000) | Comment text |
| CreatedAt | TIMESTAMP | Creation timestamp |
| UpdatedAt | TIMESTAMP | Last update timestamp |

**Indexes:**
- `idx_comments_congviecid` - For fetching task comments
- `idx_comments_userid` - For user comment history
- `idx_comments_createdat` - For chronological ordering

**Constraints:**
- `fk_comment_congviec` - Foreign key to CONG_VIEC (CASCADE DELETE)
- `fk_comment_user` - Foreign key to USERS (CASCADE DELETE)

## Verification Queries

After running the migrations, verify the setup:

### Check MODULE_FUNCTIONS table
```sql
-- Count functions by module
SELECT m.Name as ModuleName, COUNT(mf.Id) as FunctionCount
FROM MODULES m
LEFT JOIN MODULE_FUNCTIONS mf ON m.Id = mf.ModuleId
GROUP BY m.Name
ORDER BY m.Name;

-- View all functions
SELECT mf.Id, m.Name as Module, mf.Name, mf.Code, mf.IsEnabled
FROM MODULE_FUNCTIONS mf
JOIN MODULES m ON mf.ModuleId = m.Id
ORDER BY m.Name, mf.Name;
```

Expected output:
- QuanLyCongViec: 6 functions
- ProductManagement: 4 functions

### Check CONG_VIEC_COMMENTS table
```sql
-- Count comments
SELECT COUNT(*) as TotalComments FROM CONG_VIEC_COMMENTS;

-- View sample comments
SELECT c.Id, cv.TenCongViec, u.Username, c.Content, c.CreatedAt
FROM CONG_VIEC_COMMENTS c
JOIN CONG_VIEC cv ON c.CongViecId = cv.Id
JOIN USERS u ON c.UserId = u.Id
ORDER BY c.CreatedAt DESC;
```

## Rollback Scripts

If you need to rollback the changes:

### Remove MODULE_FUNCTIONS
```sql
DROP TABLE MODULE_FUNCTIONS CASCADE CONSTRAINTS;
```

### Remove CONG_VIEC_COMMENTS
```sql
DROP TABLE CONG_VIEC_COMMENTS CASCADE CONSTRAINTS;
```

## Sample Function Codes

### QuanLyCongViec Module
- `CREATE_TASK` - Create new tasks
- `VIEW_TASK` - View task list and details
- `UPDATE_TASK` - Update task information
- `DELETE_TASK` - Delete tasks
- `ASSIGN_TASK` - Assign tasks to users
- `COMMENT_TASK` - Comment on tasks

### ProductManagement Module
- `CREATE_PRODUCT` - Create new products
- `VIEW_PRODUCT` - View product list and details
- `UPDATE_PRODUCT` - Update product information
- `DELETE_PRODUCT` - Delete products

## Notes

1. **Dependencies**: These scripts assume the following tables exist:
   - MODULES (from core schema)
   - CONG_VIEC (from QuanLyCongViec module)
   - USERS (from core schema)

2. **Data Integrity**: The foreign key constraints ensure:
   - Functions are deleted when their module is deleted
   - Comments are deleted when their task or user is deleted

3. **Performance**: Indexes are created for common query patterns:
   - Listing functions by module
   - Fetching comments for a task
   - Ordering comments chronologically

4. **Sample Data**: The scripts include sample data for testing. In production, you may want to remove the INSERT statements for sample comments.

## Troubleshooting

### Error: Table or view does not exist
Ensure you've run the core schema scripts first:
- 01_create_users_table.sql
- 03_create_modules_table.sql
- modules/quan_ly_cong_viec/01_create_cong_viec_table.sql

### Error: Unique constraint violated
If you're re-running the scripts, drop existing tables first or use the rollback scripts above.

### Error: Foreign key constraint violated
Check that the referenced modules exist in the MODULES table:
```sql
SELECT * FROM MODULES WHERE Name IN ('QuanLyCongViec', 'ProductManagement');
```

## Support

For issues or questions about these migrations:
1. Check the main documentation: `/MODULE_FUNCTIONS_IMPLEMENTATION.md`
2. Review the API implementation in `/backend/Platform.API/Controllers/`
3. Check the frontend implementation in `/frontend/src/services/`
