using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platform.Core.Domain.Interfaces;
using Platform.Infrastructure.Data;
using Platform.Infrastructure.Data.Repositories;
using Platform.Modules.Base;
using Platform.Modules.ProductManagement.Domain.Entities;
using Platform.Modules.ProductManagement.Services;

namespace Platform.Modules.ProductManagement;

public class ProductModule : ModuleBase
{
    public override string Name => "ProductManagement";
    public override string Version => "1.0.0";

    public override void Initialize(IServiceCollection services)
    {
        // Register Product repository
        services.AddScoped<IRepository<Product>>(sp =>
        {
            var context = sp.GetRequiredService<OracleDbContext>();
            return new GenericRepository<Product>(context, "PRODUCTS");
        });

        // Register Product service
        services.AddScoped<ProductService>();
    }

    public override void Configure(IApplicationBuilder app)
    {
        // Module-specific middleware configuration if needed
    }
}
