# Testing Guide: User-Based Permissions for Task Management

## Prerequisites

Before testing, ensure:
1. Oracle database is running and accessible
2. Database migration has been applied (04_add_nguoi_tao_id_column.sql)
3. Backend is running (Platform.API)
4. Frontend is running (React app)

## Test Scenarios

### Scenario 1: Task Creation with User Tracking

**Steps:**
1. Login as User A (e.g., admin/Admin@123)
2. Navigate to "Quản Lý Công Việc" page
3. Click "Thêm công việc" button
4. Fill in task details:
   - Tên công việc: "Test Task A"
   - Mô tả: "This is a test task"
   - Select other required fields
5. Click "Tạo công việc"

**Expected Result:**
- Task is created successfully
- In the task card, you should see "Người tạo: Bạn" (Creator: You)
- The task appears in the list

**Verification:**
- Check database: `SELECT * FROM CONG_VIEC WHERE TenCongViec = 'Test Task A'`
- Verify NguoiTaoId matches the logged-in user's ID

---

### Scenario 2: User-Based Filtering

**Setup:**
- Have at least 3 tasks:
  - Task 1: Created by User A, assigned to User B
  - Task 2: Created by User B, assigned to User A
  - Task 3: Created by User A, no assignee

**Steps (as User A):**
1. Click "Tất cả" (All) filter
   - Expected: See all 3 tasks
   
2. Click "Công việc của tôi" (My Tasks) filter
   - Expected: See Task 1, Task 2, Task 3 (created by or assigned to User A)
   
3. Click "Tôi đã tạo" (Created by me) filter
   - Expected: See only Task 1 and Task 3 (created by User A)
   
4. Click "Được giao cho tôi" (Assigned to me) filter
   - Expected: See only Task 2 (assigned to User A)

---

### Scenario 3: Multi-User Testing

**Steps:**
1. Login as User A
2. Create a task "Task from User A"
3. Logout
4. Login as User B
5. Navigate to "Quản Lý Công Việc"
6. Click "Tất cả" filter

**Expected Result:**
- See "Task from User A" with "Người tạo: User #[A's ID]"
- Click "Tôi đã tạo" filter
- Should NOT see "Task from User A"

7. Create a task "Task from User B"

**Expected Result:**
- See "Task from User B" with "Người tạo: Bạn"

---

### Scenario 4: API Endpoint Testing

Use Swagger UI at `http://localhost:5000/swagger` or curl:

```bash
# Get JWT token
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"Admin@123"}'

# Store the token
TOKEN="<your_token_here>"

# Test: Get all tasks
curl -X GET http://localhost:5000/api/congviec \
  -H "Authorization: Bearer $TOKEN"

# Test: Get my tasks
curl -X GET http://localhost:5000/api/congviec/my-tasks \
  -H "Authorization: Bearer $TOKEN"

# Test: Get tasks created by me
curl -X GET http://localhost:5000/api/congviec/created-by-me \
  -H "Authorization: Bearer $TOKEN"

# Test: Get tasks assigned to me
curl -X GET http://localhost:5000/api/congviec/assigned-to-me \
  -H "Authorization: Bearer $TOKEN"

# Test: Create a new task
curl -X POST http://localhost:5000/api/congviec \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "tenCongViec": "API Test Task",
    "moTa": "Created via API",
    "trangThai": "Mới",
    "doUuTien": "TrungBinh",
    "ngayBatDau": "2026-02-02T00:00:00Z"
  }'
```

**Expected Results:**
- All endpoints return 200 OK (except unauthorized)
- Created task has NguoiTaoId matching the authenticated user
- Filters return appropriate subsets of tasks

---

### Scenario 5: Security Testing

**Test 1: Unauthorized Access**
```bash
# Try to access without token
curl -X GET http://localhost:5000/api/congviec/my-tasks

# Expected: 401 Unauthorized
```

**Test 2: Invalid Token**
```bash
curl -X GET http://localhost:5000/api/congviec/my-tasks \
  -H "Authorization: Bearer invalid_token"

# Expected: 401 Unauthorized
```

**Test 3: Token Tampering**
- Try to modify JWT token payload to change user ID
- Expected: Token validation fails, request rejected

---

## Database Verification

After running tests, verify database state:

```sql
-- Check all tasks with creator information
SELECT 
    cv.Id,
    cv.TenCongViec,
    cv.NguoiTaoId,
    u.Username as Creator,
    cv.NguoiPhuTrachId
FROM CONG_VIEC cv
LEFT JOIN USERS u ON cv.NguoiTaoId = u.Id;

-- Verify foreign key constraint
SELECT 
    constraint_name,
    constraint_type,
    status
FROM user_constraints
WHERE table_name = 'CONG_VIEC'
    AND constraint_name LIKE 'FK_CongViec_NguoiTao';

-- Check index exists
SELECT index_name, column_name
FROM user_ind_columns
WHERE table_name = 'CONG_VIEC'
    AND column_name = 'NGUOITAOID';
```

---

## Troubleshooting

### Issue: "User not authenticated" error
**Solution:** Ensure JWT token is valid and included in Authorization header

### Issue: Filter shows no results
**Solution:** 
- Check if any tasks match the filter criteria
- Verify user ID in JWT token matches expected value
- Check database for NguoiTaoId and NguoiPhuTrachId values

### Issue: Task created without NguoiTaoId
**Solution:**
- Verify migration script has been applied
- Check backend controller is extracting user ID from claims
- Ensure authentication is working properly

### Issue: Cannot see "Người tạo" field
**Solution:**
- Clear browser cache
- Verify frontend is fetching latest code
- Check browser console for JavaScript errors

---

## Success Criteria

✅ All test scenarios pass
✅ Database constraints are in place
✅ Security checks prevent unauthorized access
✅ UI displays correctly for different users
✅ Filters work as expected
✅ No security vulnerabilities (CodeQL scan passes)
✅ Backend builds successfully
✅ Frontend lints without errors
