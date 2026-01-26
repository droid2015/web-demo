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
    public static List<IModule> DiscoverModules(ILogger? logger = null)
    {
        var modules = new List<IModule>();
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in loadedAssemblies)
        {
            // Skip system assemblies - only look in Platform.Modules.* assemblies
            if (assembly.FullName == null || 
                !assembly.FullName.StartsWith("Platform.Modules."))
                continue;

            try
            {
                var moduleTypes = assembly.GetTypes()
                    .Where(t => typeof(IModule).IsAssignableFrom(t) && 
                               !t.IsInterface && 
                               !t.IsAbstract &&
                               t.GetConstructor(Type.EmptyTypes) != null) // Ensure parameterless constructor exists
                    .ToList();

                foreach (var moduleType in moduleTypes)
                {
                    try
                    {
                        var module = (IModule?)Activator.CreateInstance(moduleType);
                        if (module != null && module.IsEnabled)
                        {
                            modules.Add(module);
                            logger?.LogInformation("Discovered module: {ModuleName} v{ModuleVersion}", module.Name, module.Version);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger?.LogWarning(ex, "Failed to instantiate module type {ModuleType}", moduleType.FullName);
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                logger?.LogWarning("Failed to load types from assembly {AssemblyName}: {Message}", 
                    assembly.GetName().Name, ex.Message);
                // Log loader exceptions for debugging
                foreach (var loaderException in ex.LoaderExceptions.Where(e => e != null))
                {
                    logger?.LogDebug(loaderException, "Loader exception detail");
                }
            }
            catch (BadImageFormatException ex)
            {
                logger?.LogWarning("Assembly {AssemblyName} has invalid format: {Message}", 
                    assembly.GetName().Name, ex.Message);
            }
            catch (Exception ex)
            {
                logger?.LogWarning(ex, "Unexpected error scanning assembly {AssemblyName}", 
                    assembly.GetName().Name);
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
