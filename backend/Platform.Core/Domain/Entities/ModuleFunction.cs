namespace Platform.Core.Domain.Entities;

/// <summary>
/// Represents a function or feature within a module
/// </summary>
public class ModuleFunction
{
    public int Id { get; set; }
    public int ModuleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public Module Module { get; set; } = null!;
}
