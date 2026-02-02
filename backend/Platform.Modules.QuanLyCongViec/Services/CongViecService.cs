using Platform.Core.Domain.Interfaces;
using Platform.Modules.QuanLyCongViec.Domain.Entities;

namespace Platform.Modules.QuanLyCongViec.Services;

/// <summary>
/// Service for managing CongViec (Task) operations
/// </summary>
public class CongViecService
{
    private readonly IRepository<CongViec> _congViecRepository;

    public CongViecService(IRepository<CongViec> congViecRepository)
    {
        _congViecRepository = congViecRepository;
    }

    /// <summary>
    /// Get all CongViec records
    /// </summary>
    public async Task<IEnumerable<CongViec>> GetAllCongViecAsync()
    {
        return await _congViecRepository.GetAllAsync();
    }

    /// <summary>
    /// Get CongViec by ID
    /// </summary>
    public async Task<CongViec?> GetCongViecByIdAsync(int id)
    {
        return await _congViecRepository.GetByIdAsync(id);
    }

    /// <summary>
    /// Create a new CongViec
    /// </summary>
    public async Task<CongViec> CreateCongViecAsync(CongViec congViec)
    {
        congViec.NgayTao = DateTime.UtcNow;
        return await _congViecRepository.AddAsync(congViec);
    }

    /// <summary>
    /// Update an existing CongViec
    /// </summary>
    public async Task UpdateCongViecAsync(CongViec congViec)
    {
        congViec.NgayCapNhat = DateTime.UtcNow;
        await _congViecRepository.UpdateAsync(congViec);
    }

    /// <summary>
    /// Delete a CongViec by ID
    /// </summary>
    public async Task DeleteCongViecAsync(int id)
    {
        await _congViecRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Get CongViec by TrangThai (Status)
    /// </summary>
    public async Task<IEnumerable<CongViec>> GetCongViecByTrangThaiAsync(string trangThai)
    {
        var allCongViec = await _congViecRepository.GetAllAsync();
        return allCongViec.Where(cv => cv.TrangThai.Equals(trangThai, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Get CongViec by User (created by or assigned to)
    /// </summary>
    public async Task<IEnumerable<CongViec>> GetCongViecByUserAsync(int userId)
    {
        var allCongViec = await _congViecRepository.GetAllAsync();
        return allCongViec.Where(cv => cv.NguoiTaoId == userId || cv.NguoiPhuTrachId == userId);
    }

    /// <summary>
    /// Get CongViec created by a specific user
    /// </summary>
    public async Task<IEnumerable<CongViec>> GetCongViecByCreatorAsync(int userId)
    {
        var allCongViec = await _congViecRepository.GetAllAsync();
        return allCongViec.Where(cv => cv.NguoiTaoId == userId);
    }

    /// <summary>
    /// Get CongViec assigned to a specific user
    /// </summary>
    public async Task<IEnumerable<CongViec>> GetCongViecByAssigneeAsync(int userId)
    {
        var allCongViec = await _congViecRepository.GetAllAsync();
        return allCongViec.Where(cv => cv.NguoiPhuTrachId == userId);
    }
}
