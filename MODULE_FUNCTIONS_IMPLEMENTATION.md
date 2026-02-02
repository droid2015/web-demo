# Module Management and Permissions Implementation

## Overview
This document describes the new features added to support dynamic module registration, module function management, and enhanced task management capabilities.

## Features Implemented

### 1. Module Registration API

#### Backend Endpoints

**POST /api/modules** - Register a new module
```json
{
  "name": "NewModule",
  "version": "1.0.0",
  "isEnabled": true,
  "loadOrder": 10
}
```

**PUT /api/modules/{id}** - Update module metadata
```json
{
  "version": "1.1.0",
  "loadOrder": 5
}
```

**PUT /api/modules/{id}/toggle** - Enable/disable a module
```json
{
  "isEnabled": false
}
```

#### Frontend Service
```javascript
import { moduleService } from './services/moduleService';

// Register a new module
const module = await moduleService.register({
  name: 'MyModule',
  version: '1.0.0',
  isEnabled: true,
  loadOrder: 10
});

// Update module
await moduleService.update(moduleId, {
  version: '1.1.0',
  loadOrder: 5
});
```

### 2. Module Function Management

Module functions represent individual features or permissions within a module. This allows fine-grained access control.

#### Database Schema

**MODULE_FUNCTIONS Table:**
- `Id` - Primary key
- `ModuleId` - Foreign key to MODULES
- `Name` - Function display name
- `Code` - Unique function code (e.g., "CREATE_TASK")
- `Description` - Function description
- `IsEnabled` - Enable/disable flag

#### Backend Endpoints

**GET /api/modulefunctions** - Get all module functions

**GET /api/modulefunctions/module/{moduleId}** - Get functions for a specific module

**POST /api/modulefunctions** - Create a new function
```json
{
  "moduleId": 2,
  "name": "Tạo công việc",
  "code": "CREATE_TASK",
  "description": "Cho phép tạo công việc mới",
  "isEnabled": true
}
```

**PUT /api/modulefunctions/{id}** - Update a function

**DELETE /api/modulefunctions/{id}** - Delete a function

**PUT /api/modulefunctions/{id}/toggle** - Toggle function status

#### Frontend Components

**ModuleFunctionManager** - UI component for managing module functions
- Located at: `/module-functions`
- Features:
  - View all functions by module
  - Add new functions
  - Enable/disable functions
  - Delete functions
  - Filter by module

#### Sample Functions Created

**QuanLyCongViec Module:**
- CREATE_TASK - Tạo công việc
- VIEW_TASK - Xem công việc
- UPDATE_TASK - Cập nhật công việc
- DELETE_TASK - Xóa công việc
- ASSIGN_TASK - Gán công việc
- COMMENT_TASK - Bình luận công việc

**ProductManagement Module:**
- CREATE_PRODUCT - Tạo sản phẩm
- VIEW_PRODUCT - Xem sản phẩm
- UPDATE_PRODUCT - Cập nhật sản phẩm
- DELETE_PRODUCT - Xóa sản phẩm

### 3. Task Comments (QuanLyCongViec Enhancement)

Added commenting functionality to tasks for better collaboration.

#### Database Schema

**CONG_VIEC_COMMENTS Table:**
- `Id` - Primary key
- `CongViecId` - Foreign key to CONG_VIEC
- `UserId` - Foreign key to USERS
- `Content` - Comment content
- `CreatedAt` - Creation timestamp
- `UpdatedAt` - Last update timestamp

#### Backend Endpoints

**GET /api/congviec/{congViecId}/comments** - Get all comments for a task

**GET /api/congviec/{congViecId}/comments/{commentId}** - Get specific comment

**POST /api/congviec/{congViecId}/comments** - Add a comment
```json
{
  "content": "Đã hoàn thành 50% công việc"
}
```

**PUT /api/congviec/{congViecId}/comments/{commentId}** - Update a comment
- Users can only update their own comments

**DELETE /api/congviec/{congViecId}/comments/{commentId}** - Delete a comment
- Users can only delete their own comments

#### Frontend Service
```javascript
import { congViecService } from './services/congViecService';

// Get comments
const comments = await congViecService.getComments(taskId);

// Add comment
await congViecService.createComment(taskId, 'My comment text');

// Update comment
await congViecService.updateComment(taskId, commentId, 'Updated text');

// Delete comment
await congViecService.deleteComment(taskId, commentId);
```

## Database Migration Scripts

Run these scripts in order to set up the new features:

1. **Core schema:**
   ```sql
   @database/core/08_create_module_functions_table.sql
   ```

2. **QuanLyCongViec module:**
   ```sql
   @database/modules/quan_ly_cong_viec/05_create_comments_table.sql
   ```

## Security Features

- All endpoints require JWT authentication
- Comment editing/deletion is restricted to the comment owner
- Input validation on all POST/PUT requests
- SQL injection protection through parameterized queries
- No vulnerabilities detected by CodeQL scanner

## Usage Examples

### 1. Register a New Module

```bash
curl -X POST http://localhost:5000/api/modules \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "ReportingModule",
    "version": "1.0.0",
    "isEnabled": true,
    "loadOrder": 3
  }'
```

### 2. Add a Module Function

```bash
curl -X POST http://localhost:5000/api/modulefunctions \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "moduleId": 2,
    "name": "Export Report",
    "code": "EXPORT_REPORT",
    "description": "Export data to PDF/Excel",
    "isEnabled": true
  }'
```

### 3. Add a Comment to a Task

```bash
curl -X POST http://localhost:5000/api/congviec/1/comments \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "content": "This task is now in progress"
  }'
```

## Future Enhancements

Potential improvements for consideration:

1. **Role-Function Mapping** - Link module functions to roles for permission management
2. **Comment Attachments** - Allow file attachments on task comments
3. **Comment Notifications** - Notify users when someone comments on their tasks
4. **Function Groups** - Group related functions for easier management
5. **Module Dependencies** - Define dependencies between modules
6. **Audit Trail** - Track all changes to module configurations

## Technical Details

### Services Added

**Backend:**
- `ModuleFunctionService` - Business logic for module functions
- `CongViecCommentService` - Business logic for task comments

**Frontend:**
- `moduleFunctionService` - API client for module functions
- Updated `moduleService` - Added registration methods
- Updated `congViecService` - Added comment methods

### Components Added

**Frontend:**
- `ModuleFunctionManager` - UI for managing module functions
- Route added: `/module-functions`
- Navigation link added to sidebar

### Database Indexes

For optimal performance, the following indexes were created:

**MODULE_FUNCTIONS:**
- `idx_module_functions_moduleid` on ModuleId
- `idx_module_functions_code` on Code
- `idx_module_functions_enabled` on IsEnabled

**CONG_VIEC_COMMENTS:**
- `idx_comments_congviecid` on CongViecId
- `idx_comments_userid` on UserId
- `idx_comments_createdat` on CreatedAt

## Testing

### Build Status
✅ Backend builds successfully (0 errors)
✅ Frontend builds successfully (0 errors)
✅ Security scan passed (0 vulnerabilities)

### Manual Testing Checklist

Backend API:
- [ ] POST /api/modules - Register new module
- [ ] PUT /api/modules/{id} - Update module
- [ ] POST /api/modulefunctions - Create function
- [ ] GET /api/modulefunctions/module/{id} - List functions
- [ ] PUT /api/modulefunctions/{id}/toggle - Toggle function
- [ ] POST /api/congviec/{id}/comments - Add comment
- [ ] GET /api/congviec/{id}/comments - List comments
- [ ] PUT /api/congviec/{id}/comments/{id} - Update comment
- [ ] DELETE /api/congviec/{id}/comments/{id} - Delete comment

Frontend UI:
- [ ] Navigate to /module-functions
- [ ] Select a module from dropdown
- [ ] Add a new function
- [ ] Toggle function status
- [ ] Delete a function
- [ ] View task comments (when UI is added)

## Conclusion

These enhancements provide a solid foundation for:
1. Dynamic module management and registration
2. Fine-grained permission control through module functions
3. Improved collaboration with task comments

All code follows established patterns, is fully documented, and passes security scans.
