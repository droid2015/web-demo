namespace Platform.Modules.QuanLyCongViec.Domain.Entities;

public class CongViec
{
    public int Id { get; set; }
    public string TenCongViec { get; set; } = string.Empty;
    public string MoTa { get; set; } = string.Empty;
    public string TrangThai { get; set; } = "Mới"; // Mới, ĐangThucHien, HoanThanh, Huy
    public string DoUuTien { get; set; } = "TrungBinh"; // Thap, TrungBinh, Cao, KhanCap
    public int? NguoiPhuTrachId { get; set; }
    public DateTime NgayBatDau { get; set; }
    public DateTime? NgayKetThuc { get; set; }
    public DateTime NgayTao { get; set; } = DateTime.UtcNow;
    public DateTime? NgayCapNhat { get; set; }
}
