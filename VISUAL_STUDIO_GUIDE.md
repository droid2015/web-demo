# Hướng dẫn Build với Visual Studio / Visual Studio Build Guide

## Tiếng Việt

### Yêu cầu hệ thống

- Visual Studio 2022 hoặc mới hơn
- .NET 8 SDK hoặc mới hơn
- Oracle Database (hoặc Docker)

### Các bước build dự án

1. **Mở Solution**
   - Điều hướng đến thư mục `backend`
   - Double-click vào file `Platform.sln` để mở trong Visual Studio
   - Hoặc từ Visual Studio: File → Open → Project/Solution → chọn `Platform.sln`

2. **Restore NuGet Packages**
   - Right-click vào Solution trong Solution Explorer
   - Chọn "Restore NuGet Packages"
   - Đợi quá trình restore hoàn tất

3. **Build Solution**
   - Sử dụng menu: Build → Build Solution
   - Hoặc nhấn phím tắt: `Ctrl+Shift+B`
   - Kiểm tra Output window để xem quá trình build

4. **Cấu hình Startup Project**
   - Right-click vào project `Platform.API` trong Solution Explorer
   - Chọn "Set as Startup Project"

5. **Chạy ứng dụng**
   - Nhấn `F5` để chạy với debug
   - Hoặc `Ctrl+F5` để chạy không có debug
   - API sẽ chạy tại `http://localhost:5000`

### Cấu trúc Solution

Solution bao gồm các project sau:

- **Platform.Core** - Domain entities và core services
- **Platform.Infrastructure** - Data access và logging
- **Platform.API** - REST API controllers và startup configuration
- **Platform.Modules.Base** - Base classes cho modules
- **Platform.Modules.ProductManagement** - Sample module

### Troubleshooting

**Lỗi restore packages:**
- Kiểm tra kết nối internet
- Xóa thư mục `bin` và `obj` trong từng project
- Chạy lại "Restore NuGet Packages"

**Lỗi build:**
- Đảm bảo đã cài đặt .NET 8 SDK
- Kiểm tra Output window để xem chi tiết lỗi
- Clean solution: Build → Clean Solution, sau đó rebuild

**Lỗi khi chạy:**
- Kiểm tra file `Platform.API/appsettings.json` có cấu hình đúng
- Đảm bảo Oracle Database đang chạy
- Kiểm tra connection string trong appsettings.json

### Debug trong Visual Studio

1. Đặt breakpoint bằng cách click vào lề trái của dòng code
2. Nhấn F5 để bắt đầu debug
3. Sử dụng các phím tắt:
   - `F10` - Step Over
   - `F11` - Step Into
   - `Shift+F11` - Step Out
   - `F5` - Continue

### Xem Swagger UI

Khi ứng dụng đang chạy, mở trình duyệt và truy cập:
```
http://localhost:5000/swagger
```

---

## English

### System Requirements

- Visual Studio 2022 or later
- .NET 8 SDK or later
- Oracle Database (or Docker)

### Build Steps

1. **Open Solution**
   - Navigate to the `backend` folder
   - Double-click on `Platform.sln` to open in Visual Studio
   - Or from Visual Studio: File → Open → Project/Solution → select `Platform.sln`

2. **Restore NuGet Packages**
   - Right-click on the Solution in Solution Explorer
   - Select "Restore NuGet Packages"
   - Wait for the restore process to complete

3. **Build Solution**
   - Use menu: Build → Build Solution
   - Or press shortcut: `Ctrl+Shift+B`
   - Check the Output window to see the build process

4. **Configure Startup Project**
   - Right-click on the `Platform.API` project in Solution Explorer
   - Select "Set as Startup Project"

5. **Run Application**
   - Press `F5` to run with debugging
   - Or `Ctrl+F5` to run without debugging
   - The API will run at `http://localhost:5000`

### Solution Structure

The solution includes the following projects:

- **Platform.Core** - Domain entities and core services
- **Platform.Infrastructure** - Data access and logging
- **Platform.API** - REST API controllers and startup configuration
- **Platform.Modules.Base** - Base classes for modules
- **Platform.Modules.ProductManagement** - Sample module

### Troubleshooting

**Package restore errors:**
- Check internet connection
- Delete `bin` and `obj` folders in each project
- Run "Restore NuGet Packages" again

**Build errors:**
- Ensure .NET 8 SDK is installed
- Check Output window for detailed errors
- Clean solution: Build → Clean Solution, then rebuild

**Runtime errors:**
- Check `Platform.API/appsettings.json` configuration
- Ensure Oracle Database is running
- Verify connection string in appsettings.json

### Debugging in Visual Studio

1. Set breakpoints by clicking on the left margin of a code line
2. Press F5 to start debugging
3. Use keyboard shortcuts:
   - `F10` - Step Over
   - `F11` - Step Into
   - `Shift+F11` - Step Out
   - `F5` - Continue

### View Swagger UI

When the application is running, open a browser and navigate to:
```
http://localhost:5000/swagger
```

### Build from Command Line (Alternative)

If you prefer using the command line:

```bash
cd backend
dotnet restore Platform.sln
dotnet build Platform.sln
dotnet run --project Platform.API
```

### Additional Resources

- [Backend README](backend/README.md) - Detailed backend documentation
- [Development Guide](DEVELOPMENT_GUIDE.md) - How to create new modules
- [Main README](README.md) - Project overview
