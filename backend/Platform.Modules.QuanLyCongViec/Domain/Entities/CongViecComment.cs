namespace Platform.Modules.QuanLyCongViec.Domain.Entities;

/// <summary>
/// Represents a comment on a CongViec (task)
/// </summary>
public class CongViecComment
{
    public int Id { get; set; }
    public int CongViecId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
