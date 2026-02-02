using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Modules.QuanLyCongViec.Domain.Entities;
using Platform.Modules.QuanLyCongViec.Services;
using System.Security.Claims;

namespace Platform.Modules.QuanLyCongViec.Controllers;

/// <summary>
/// API Controller for managing CongViec comments
/// </summary>
[ApiController]
[Route("api/congviec/{congViecId}/comments")]
[Authorize]
public class CongViecCommentsController : ControllerBase
{
    private readonly CongViecCommentService _commentService;

    public CongViecCommentsController(CongViecCommentService commentService)
    {
        _commentService = commentService;
    }

    /// <summary>
    /// Get all comments for a specific CongViec
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetComments(int congViecId)
    {
        try
        {
            var comments = await _commentService.GetCommentsByCongViecIdAsync(congViecId);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving comments", error = ex.Message });
        }
    }

    /// <summary>
    /// Get a specific comment by ID
    /// </summary>
    [HttpGet("{commentId}")]
    public async Task<IActionResult> GetComment(int congViecId, int commentId)
    {
        try
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            if (comment == null || comment.CongViecId != congViecId)
                return NotFound(new { message = $"Comment with ID {commentId} not found" });
            
            return Ok(comment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving comment", error = ex.Message });
        }
    }

    /// <summary>
    /// Create a new comment
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateComment(int congViecId, [FromBody] CreateCommentRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Content))
                return BadRequest(new { message = "Comment content is required" });

            // Get current user ID from JWT claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return StatusCode(401, new { message = "User not authenticated" });
            }

            var comment = new CongViecComment
            {
                CongViecId = congViecId,
                UserId = userId,
                Content = request.Content
            };

            var createdComment = await _commentService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetComment), 
                new { congViecId = congViecId, commentId = createdComment.Id }, 
                createdComment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating comment", error = ex.Message });
        }
    }

    /// <summary>
    /// Update a comment
    /// </summary>
    [HttpPut("{commentId}")]
    public async Task<IActionResult> UpdateComment(int congViecId, int commentId, [FromBody] UpdateCommentRequest request)
    {
        try
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            if (comment == null || comment.CongViecId != congViecId)
                return NotFound(new { message = $"Comment with ID {commentId} not found" });

            // Verify the user owns this comment
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return StatusCode(401, new { message = "User not authenticated" });
            }

            if (comment.UserId != userId)
                return StatusCode(403, new { message = "You can only edit your own comments" });

            comment.Content = request.Content;
            await _commentService.UpdateCommentAsync(comment);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating comment", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a comment
    /// </summary>
    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteComment(int congViecId, int commentId)
    {
        try
        {
            var comment = await _commentService.GetCommentByIdAsync(commentId);
            if (comment == null || comment.CongViecId != congViecId)
                return NotFound(new { message = $"Comment with ID {commentId} not found" });

            // Verify the user owns this comment
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return StatusCode(401, new { message = "User not authenticated" });
            }

            if (comment.UserId != userId)
                return StatusCode(403, new { message = "You can only delete your own comments" });

            await _commentService.DeleteCommentAsync(commentId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting comment", error = ex.Message });
        }
    }
}

public class CreateCommentRequest
{
    public string Content { get; set; } = string.Empty;
}

public class UpdateCommentRequest
{
    public string Content { get; set; } = string.Empty;
}
