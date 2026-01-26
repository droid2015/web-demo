using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Platform.API.Middleware;
using Platform.Core.Domain.Entities;
using Platform.Core.Domain.Interfaces;
using Platform.Core.Services;
using Platform.Infrastructure.Data;
using Platform.Infrastructure.Data.Repositories;
using Platform.Modules.ProductManagement;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.File("logs/platform-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers()
    .AddApplicationPart(typeof(ProductModule).Assembly); // Add module controllers

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add JWT Authentication
var jwtSecret = builder.Configuration["Jwt:Secret"] ?? "YourSecretKeyForJWTTokenGeneration123456";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "PlatformAPI",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "PlatformClient",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
        };
    });

builder.Services.AddAuthorization();

// Register Database Context
builder.Services.AddSingleton<OracleDbContext>();

// Register Repositories
builder.Services.AddScoped<IRepository<User>>(sp =>
{
    var context = sp.GetRequiredService<OracleDbContext>();
    return new UserRepository(context);
});

builder.Services.AddScoped<IRepository<Role>>(sp =>
{
    var context = sp.GetRequiredService<OracleDbContext>();
    return new RoleRepository(context);
});

builder.Services.AddScoped<IRepository<Module>>(sp =>
{
    var context = sp.GetRequiredService<OracleDbContext>();
    return new ModuleRepository(context);
});

// Register Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ModuleService>();

// Initialize modules
var productModule = new ProductModule();
productModule.Initialize(builder.Services);

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Platform API",
        Version = "v1",
        Description = "Modular Platform API with JWT Authentication"
    });

    // Add JWT authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Platform API v1");
    });
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Configure modules
productModule.Configure(app);

Log.Information("Platform API starting...");

app.Run();

Log.CloseAndFlush();
