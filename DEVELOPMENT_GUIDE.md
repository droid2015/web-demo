# Development Guide

## Creating a New Module

This guide shows you how to create a new module for the platform.

### 1. Create Module Project

```bash
cd backend
dotnet new classlib -n Platform.Modules.YourModule -f net8.0
dotnet sln add Platform.Modules.YourModule/Platform.Modules.YourModule.csproj
```

### 2. Add Project References

```bash
dotnet add Platform.Modules.YourModule reference Platform.Core
dotnet add Platform.Modules.YourModule reference Platform.Modules.Base
dotnet add Platform.Modules.YourModule reference Platform.Infrastructure
```

### 3. Add Required Packages

```bash
dotnet add Platform.Modules.YourModule package Microsoft.AspNetCore.Mvc.Core
```

### 4. Create Module Structure

```
Platform.Modules.YourModule/
├── Controllers/
│   └── YourController.cs
├── Domain/
│   └── Entities/
│       └── YourEntity.cs
├── Services/
│   └── YourService.cs
└── YourModule.cs
```

### 5. Implement Module Class

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platform.Modules.Base;

namespace Platform.Modules.YourModule;

public class YourModule : ModuleBase
{
    public override string Name => "YourModule";
    public override string Version => "1.0.0";

    public override void Initialize(IServiceCollection services)
    {
        // Register services, repositories, etc.
        services.AddScoped<YourService>();
    }

    public override void Configure(IApplicationBuilder app)
    {
        // Configure middleware if needed
    }
}
```

### 6. Create Entity

```csharp
namespace Platform.Modules.YourModule.Domain.Entities;

public class YourEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```

### 7. Create Service

```csharp
using Platform.Core.Domain.Interfaces;
using Platform.Modules.YourModule.Domain.Entities;

namespace Platform.Modules.YourModule.Services;

public class YourService
{
    private readonly IRepository<YourEntity> _repository;

    public YourService(IRepository<YourEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<YourEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    // Add more methods as needed
}
```

### 8. Create Controller

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Modules.YourModule.Services;

namespace Platform.Modules.YourModule.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class YourController : ControllerBase
{
    private readonly YourService _service;

    public YourController(YourService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }
}
```

### 9. Register Module in Platform.API

Edit `Platform.API/Program.cs`:

```csharp
// Add reference to your module
using Platform.Modules.YourModule;

// Initialize module
var yourModule = new YourModule();
yourModule.Initialize(builder.Services);

// Configure module
yourModule.Configure(app);
```

### 10. Create Database Schema

Create migration scripts in `database/modules/your_module/`:

```sql
-- 01_create_your_table.sql
CREATE TABLE YOUR_TABLE (
    Id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Name VARCHAR2(200) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL
);
```

### 11. Frontend Component

Create React components in `frontend/src/components/modules/YourModule/`:

```jsx
// YourList.jsx
import { useState, useEffect } from 'react';
import api from '../../../services/api';

const YourList = () => {
  const [items, setItems] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadItems();
  }, []);

  const loadItems = async () => {
    try {
      const response = await api.get('/your');
      setItems(response.data);
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <div>Loading...</div>;

  return (
    <div>
      <h1>Your Module</h1>
      {/* Render your data */}
    </div>
  );
};

export default YourList;
```

### 12. Add Route

Update `frontend/src/routes/AppRoutes.jsx`:

```jsx
import YourList from '../components/modules/YourModule/YourList';

// Add route
<Route path="your-module" element={<YourList />} />
```

### 13. Add Navigation

Update `frontend/src/components/core/Layout/Sidebar.jsx`:

```jsx
<li>
  <Link to="/your-module">Your Module</Link>
</li>
```

## Best Practices

1. **Separation of Concerns**: Keep business logic in services, data access in repositories
2. **Dependency Injection**: Use DI for all dependencies
3. **Error Handling**: Implement proper error handling in controllers and services
4. **Validation**: Validate input data in controllers
5. **Logging**: Use logger for important operations
6. **Security**: Always use `[Authorize]` attribute for protected endpoints
7. **Database**: Create proper indexes for frequently queried columns
8. **API Design**: Follow RESTful conventions
9. **Testing**: Write unit tests for services and integration tests for controllers
10. **Documentation**: Document your API endpoints and complex logic

## Module Configuration

Modules can be enabled/disabled through the Module Manager UI or directly in the database:

```sql
UPDATE MODULES SET IsEnabled = 0 WHERE Name = 'YourModule';
```

## Troubleshooting

- **Module not loading**: Check that it's registered in `Program.cs` and enabled in database
- **Controller not found**: Ensure controller assembly is added via `AddApplicationPart`
- **Database errors**: Verify connection string and table schemas
- **CORS issues**: Check CORS policy in `Program.cs`
