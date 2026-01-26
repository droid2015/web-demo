# Backend - Platform API

ASP.NET Core Web API with .NET 8, Oracle database, and modular architecture.

## Setup

### Prerequisites

- .NET 8 SDK
- Oracle Database (or Docker container)

### Build and Run

```bash
dotnet restore
dotnet build
cd Platform.API
dotnet run
```

The API will be available at `http://localhost:5000`

### Swagger UI

Interactive API documentation: `http://localhost:5000/swagger`

## Configuration

Edit `Platform.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "OracleConnection": "User Id=SYSTEM;Password=oracle;Data Source=localhost:1521/XE"
  },
  "Jwt": {
    "Secret": "YourSecretKeyForJWTTokenGeneration123456",
    "Issuer": "PlatformAPI",
    "Audience": "PlatformClient"
  }
}
```

## Project Structure

- **Platform.Core** - Domain entities, interfaces, core services
- **Platform.Infrastructure** - Database context, repositories, logging
- **Platform.API** - Controllers, middleware, startup configuration
- **Platform.Modules.Base** - Base classes and interfaces for modules
- **Platform.Modules.ProductManagement** - Sample module implementation

## API Endpoints

### Authentication

- `POST /api/auth/login` - User login

### Users

- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

### Modules

- `GET /api/modules` - Get all modules
- `GET /api/modules/{id}` - Get module by ID
- `PUT /api/modules/{id}/toggle` - Enable/disable module

### Products

- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product

## Authentication

All endpoints except `/api/auth/login` require JWT authentication.

Include the token in the Authorization header:
```
Authorization: Bearer <your-jwt-token>
```

## Development

### Adding a New Module

See [DEVELOPMENT_GUIDE.md](../DEVELOPMENT_GUIDE.md) in the root directory.

### Testing

```bash
dotnet test
```

### Logging

Logs are written to:
- Console
- `logs/platform-{date}.log`

## Technologies

- .NET 8
- ASP.NET Core Web API
- Oracle.ManagedDataAccess.Core
- Dapper (micro-ORM)
- BCrypt.Net (password hashing)
- Serilog (logging)
- Swashbuckle (Swagger/OpenAPI)
- JWT Bearer Authentication
