using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Platform.Core.Domain.Interfaces;

namespace Platform.Modules.Base;

public abstract class ModuleBase : IModule
{
    public abstract string Name { get; }
    public abstract string Version { get; }
    public virtual bool IsEnabled => true;

    public abstract void Initialize(IServiceCollection services);
    
    public virtual void Configure(IApplicationBuilder app)
    {
        // Default implementation - can be overridden
    }
}
