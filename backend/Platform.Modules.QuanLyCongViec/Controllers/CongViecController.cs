using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Modules.QuanLyCongViec.Domain.Entities;
using Platform.Modules.QuanLyCongViec.Services;
using System.Security.Claims;

namespace Platform.Modules.QuanLyCongViec.Controllers;

/// <summary>
/// API Controller for managing CongViec (Tasks)
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CongViecController : ControllerBase
{
    private readonly CongViecService _congViecService;

    public CongViecController(CongViecService congViecService)
    {
        _congViecService = congViecService;
    }

    /// <summary>
    /// Get all CongViec
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var congViecList = await _congViecService.GetAllCongViecAsync();
            return Ok(congViecList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving CongViec list", error = ex.Message });
        }
    }

    /// <summary>
    /// Get CongViec by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var congViec = await _congViecService.GetCongViecByIdAsync(id);
            if (congViec == null)
                return NotFound(new { message = $"CongViec with ID {id} not found" });
            
            return Ok(congViec);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving CongViec", error = ex.Message });
        }
    }

    /// <summary>
    /// Get CongViec by TrangThai (Status)
    /// </summary>
    [HttpGet("trangthai/{trangThai}")]
    public async Task<IActionResult> GetByTrangThai(string trangThai)
    {
        try
        {
            var congViecList = await _congViecService.GetCongViecByTrangThaiAsync(trangThai);
            return Ok(congViecList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving CongViec by TrangThai", error = ex.Message });
        }
    }

    /// <summary>
    /// Get CongViec for current user (created by or assigned to)
    /// </summary>
    [HttpGet("my-tasks")]
    public async Task<IActionResult> GetMyTasks()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var congViecList = await _congViecService.GetCongViecByUserAsync(userId);
            return Ok(congViecList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving user's CongViec", error = ex.Message });
        }
    }

    /// <summary>
    /// Get CongViec created by current user
    /// </summary>
    [HttpGet("created-by-me")]
    public async Task<IActionResult> GetCreatedByMe()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var congViecList = await _congViecService.GetCongViecByCreatorAsync(userId);
            return Ok(congViecList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving created CongViec", error = ex.Message });
        }
    }

    /// <summary>
    /// Get CongViec assigned to current user
    /// </summary>
    [HttpGet("assigned-to-me")]
    public async Task<IActionResult> GetAssignedToMe()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var congViecList = await _congViecService.GetCongViecByAssigneeAsync(userId);
            return Ok(congViecList);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving assigned CongViec", error = ex.Message });
        }
    }

    /// <summary>
    /// Create a new CongViec
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CongViec congViec)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(congViec.TenCongViec))
                return BadRequest(new { message = "TenCongViec is required" });

            // Get current user ID from JWT claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            // Set the creator user ID
            congViec.NguoiTaoId = userId;

            var createdCongViec = await _congViecService.CreateCongViecAsync(congViec);
            return CreatedAtAction(nameof(GetById), new { id = createdCongViec.Id }, createdCongViec);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating CongViec", error = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing CongViec
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CongViec congViec)
    {
        try
        {
            var existingCongViec = await _congViecService.GetCongViecByIdAsync(id);
            if (existingCongViec == null)
                return NotFound(new { message = $"CongViec with ID {id} not found" });

            congViec.Id = id;
            await _congViecService.UpdateCongViecAsync(congViec);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating CongViec", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a CongViec by ID
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var existingCongViec = await _congViecService.GetCongViecByIdAsync(id);
            if (existingCongViec == null)
                return NotFound(new { message = $"CongViec with ID {id} not found" });

            await _congViecService.DeleteCongViecAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting CongViec", error = ex.Message });
        }
    }
}
