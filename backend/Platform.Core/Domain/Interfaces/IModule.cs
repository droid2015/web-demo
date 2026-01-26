using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Platform.Core.Domain.Interfaces;

public interface IModule
{
    string Name { get; }
    string Version { get; }
    bool IsEnabled { get; }
    void Initialize(IServiceCollection services);
    void Configure(IApplicationBuilder app);
}
