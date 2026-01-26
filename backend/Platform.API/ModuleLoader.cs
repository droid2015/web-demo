using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Logging;
using Platform.Core.Domain.Interfaces;

namespace Platform.API;

public static class ModuleLoader
{
    /// <summary>
    /// Discovers and loads all modules from referenced assemblies
    /// </summary>
    public static List<IModule> DiscoverModules()
    {
        var modules = new List<IModule>();
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in loadedAssemblies)
        {
            // Skip system assemblies
            if (assembly.FullName == null || 
                assembly.FullName.StartsWith("System") || 
                assembly.FullName.StartsWith("Microsoft"))
                continue;

            try
            {
                var moduleTypes = assembly.GetTypes()
                    .Where(t => typeof(IModule).IsAssignableFrom(t) && 
                               !t.IsInterface && 
                               !t.IsAbstract)
                    .ToList();

                foreach (var moduleType in moduleTypes)
                {
                    var module = (IModule?)Activator.CreateInstance(moduleType);
                    if (module != null && module.IsEnabled)
                    {
                        modules.Add(module);
                    }
                }
            }
            catch (Exception)
            {
                // Skip assemblies that can't be reflected
                continue;
            }
        }

        return modules;
    }

    /// <summary>
    /// Initializes all discovered modules
    /// </summary>
    public static void InitializeModules(IServiceCollection services, List<IModule> modules, ILogger? logger = null)
    {
        foreach (var module in modules)
        {
            logger?.LogInformation("Initializing module: {ModuleName} v{ModuleVersion}", module.Name, module.Version);
            module.Initialize(services);
        }
    }

    /// <summary>
    /// Configures all discovered modules
    /// </summary>
    public static void ConfigureModules(IApplicationBuilder app, List<IModule> modules, ILogger? logger = null)
    {
        foreach (var module in modules)
        {
            logger?.LogInformation("Configuring module: {ModuleName}", module.Name);
            module.Configure(app);
        }
    }

    /// <summary>
    /// Registers module controllers with MVC
    /// </summary>
    public static void AddModuleControllers(IMvcBuilder mvcBuilder, List<IModule> modules)
    {
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        foreach (var assembly in loadedAssemblies)
        {
            // Add assemblies that contain modules
            if (assembly.FullName != null && 
                assembly.FullName.StartsWith("Platform.Modules."))
            {
                mvcBuilder.AddApplicationPart(assembly);
            }
        }
    }
}
