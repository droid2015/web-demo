using Platform.Core.Domain.Interfaces;
using Platform.Modules.QuanLyCongViec.Domain.Entities;

namespace Platform.Modules.QuanLyCongViec.Services;

/// <summary>
/// Service for managing CongViec comments
/// </summary>
public class CongViecCommentService
{
    private readonly IRepository<CongViecComment> _commentRepository;
    private readonly IRepository<CongViec> _congViecRepository;

    public CongViecCommentService(
        IRepository<CongViecComment> commentRepository,
        IRepository<CongViec> congViecRepository)
    {
        _commentRepository = commentRepository;
        _congViecRepository = congViecRepository;
    }

    public async Task<IEnumerable<CongViecComment>> GetCommentsByCongViecIdAsync(int congViecId)
    {
        var allComments = await _commentRepository.GetAllAsync();
        return allComments.Where(c => c.CongViecId == congViecId).OrderBy(c => c.CreatedAt);
    }

    public async Task<CongViecComment?> GetCommentByIdAsync(int id)
    {
        return await _commentRepository.GetByIdAsync(id);
    }

    public async Task<CongViecComment> CreateCommentAsync(CongViecComment comment)
    {
        // Verify CongViec exists
        var congViec = await _congViecRepository.GetByIdAsync(comment.CongViecId);
        if (congViec == null)
        {
            throw new ArgumentException($"CongViec with ID {comment.CongViecId} does not exist");
        }

        comment.CreatedAt = DateTime.UtcNow;
        return await _commentRepository.AddAsync(comment);
    }

    public async Task UpdateCommentAsync(CongViecComment comment)
    {
        comment.UpdatedAt = DateTime.UtcNow;
        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteCommentAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
    }
}
