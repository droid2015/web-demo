using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platform.Core.Domain.Interfaces;
using Platform.Infrastructure.Data;
using Platform.Infrastructure.Data.Repositories;
using Platform.Modules.Base;
using Platform.Modules.QuanLyCongViec.Domain.Entities;
using Platform.Modules.QuanLyCongViec.Services;

namespace Platform.Modules.QuanLyCongViec;

public class QuanLyCongViecModule : ModuleBase
{
    public override string Name => "QuanLyCongViec";
    public override string Version => "1.0.0";

    public override void Initialize(IServiceCollection services)
    {
        // Register CongViec repository
        services.AddScoped<IRepository<CongViec>>(sp =>
        {
            var context = sp.GetRequiredService<OracleDbContext>();
            return new GenericRepository<CongViec>(context, "CONG_VIEC");
        });

        // Register CongViecComment repository
        services.AddScoped<IRepository<CongViecComment>>(sp =>
        {
            var context = sp.GetRequiredService<OracleDbContext>();
            return new GenericRepository<CongViecComment>(context, "CONG_VIEC_COMMENTS");
        });

        // Register services
        services.AddScoped<CongViecService>();
        services.AddScoped<CongViecCommentService>();
    }

    public override void Configure(IApplicationBuilder app)
    {
        // Module-specific middleware configuration if needed
    }
}
