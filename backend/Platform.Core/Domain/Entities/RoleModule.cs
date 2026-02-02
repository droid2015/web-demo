namespace Platform.Core.Domain.Entities;

public class RoleModule
{
    public int RoleId { get; set; }
    public int ModuleId { get; set; }
    public Role Role { get; set; } = null!;
    public Module Module { get; set; } = null!;
}
