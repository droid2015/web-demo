# Web Demo Platform

Modular platform system with C# .NET 8 Backend, ReactJS Frontend, and Oracle Database.

## Features

- **Backend**: ASP.NET Core Web API with .NET 8
  - JWT Authentication
  - Role-Based Access Control (RBAC)
  - Modular plugin architecture
  - Oracle database integration
  - Serilog logging
  - Swagger/OpenAPI documentation

- **Frontend**: React with Vite
  - Modern responsive UI
  - React Router for navigation
  - Axios for API calls
  - Authentication context
  - Module-based component structure

- **Database**: Oracle
  - Core schema (Users, Roles, Permissions, Modules)
  - Module-specific schemas
  - Migration scripts

## Quick Start

### Hướng dẫn nhanh chạy Frontend (Vietnamese)

**Yêu cầu:** Node.js 18+

```bash
# Di chuyển vào thư mục frontend
cd frontend

# Cài đặt thư viện
npm install

# Tạo file cấu hình
cp .env.example .env

# Chạy ứng dụng
npm run dev
```

Truy cập: `http://localhost:5173`

**Đăng nhập:**
- Tên đăng nhập: `admin`
- Mật khẩu: `Admin@123`

📖 [Xem hướng dẫn chi tiết tiếng Việt](frontend/README.md)

---

### Prerequisites

- .NET 8 SDK or later
- Visual Studio 2022 or later (optional, for backend development)
- Node.js 18+
- Oracle Database 12c or higher (or Docker)

### Running with Docker Compose

```bash
docker-compose up -d
```

### Manual Setup

#### Building with Visual Studio

1. Navigate to the `backend` folder
2. Open `Platform.sln` in Visual Studio 2022 or later
3. Right-click on the solution and select "Restore NuGet Packages"
4. Build the solution using Build → Build Solution (Ctrl+Shift+B)
5. Set `Platform.API` as the startup project
6. Press F5 to run the API

#### Building with Command Line

See detailed setup instructions in the sections below.

## Default Credentials

- **Username**: admin
- **Password**: Admin@123

## Documentation

- [Visual Studio Build Guide](VISUAL_STUDIO_GUIDE.md) - How to build with Visual Studio (Vietnamese & English)
- [Development Guide](DEVELOPMENT_GUIDE.md) - How to create new modules
- [Backend README](backend/README.md) - Backend setup and API documentation
- [Frontend README](frontend/README.md) - Frontend setup and development
- [Database README](database/README.md) - Database schema and migrations

## Presentations

- [**Presentation Slides**](PRESENTATION.md) - Comprehensive slides for team presentation (Vietnamese)
- [**Interactive HTML Slides**](presentation.html) - Open in browser for interactive slideshow
- [Presentation Guide](PRESENTATION_README.md) - How to use and present the slides

## Architecture

This platform uses a modular architecture where functionality is divided into independent modules that can be enabled/disabled dynamically.

### Backend (.NET 8)
- Platform.Core - Domain entities and services
- Platform.Infrastructure - Data access and logging
- Platform.API - REST API endpoints
- Platform.Modules.* - Feature modules

### Frontend (React + Vite)
- Component-based architecture
- JWT authentication
- API integration with Axios
- Responsive design

## License

MIT
