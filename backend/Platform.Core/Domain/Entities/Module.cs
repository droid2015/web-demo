namespace Platform.Core.Domain.Entities;

public class Module
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public bool IsEnabled { get; set; }
    public int LoadOrder { get; set; }
    public List<RoleModule> RoleModules { get; set; } = new();
    public List<ModuleFunction> ModuleFunctions { get; set; } = new();
}
