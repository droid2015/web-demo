using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Core.Domain.Entities;
using Platform.Core.Services;

namespace Platform.API.Controllers;

/// <summary>
/// API Controller for managing module functions
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ModuleFunctionsController : ControllerBase
{
    private readonly ModuleFunctionService _moduleFunctionService;

    public ModuleFunctionsController(ModuleFunctionService moduleFunctionService)
    {
        _moduleFunctionService = moduleFunctionService;
    }

    /// <summary>
    /// Get all module functions
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var functions = await _moduleFunctionService.GetAllFunctionsAsync();
            return Ok(functions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving module functions", error = ex.Message });
        }
    }

    /// <summary>
    /// Get module functions by module ID
    /// </summary>
    [HttpGet("module/{moduleId}")]
    public async Task<IActionResult> GetByModuleId(int moduleId)
    {
        try
        {
            var functions = await _moduleFunctionService.GetFunctionsByModuleIdAsync(moduleId);
            return Ok(functions);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving module functions", error = ex.Message });
        }
    }

    /// <summary>
    /// Get module function by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var function = await _moduleFunctionService.GetFunctionByIdAsync(id);
            if (function == null)
                return NotFound(new { message = $"Module function with ID {id} not found" });
            
            return Ok(function);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error retrieving module function", error = ex.Message });
        }
    }

    /// <summary>
    /// Create a new module function
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateModuleFunctionRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new { message = "Function name is required" });

            if (string.IsNullOrWhiteSpace(request.Code))
                return BadRequest(new { message = "Function code is required" });

            var function = new ModuleFunction
            {
                ModuleId = request.ModuleId,
                Name = request.Name,
                Code = request.Code,
                Description = request.Description,
                IsEnabled = request.IsEnabled
            };

            var createdFunction = await _moduleFunctionService.CreateFunctionAsync(function);
            return CreatedAtAction(nameof(GetById), new { id = createdFunction.Id }, createdFunction);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error creating module function", error = ex.Message });
        }
    }

    /// <summary>
    /// Update an existing module function
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateModuleFunctionRequest request)
    {
        try
        {
            var function = await _moduleFunctionService.GetFunctionByIdAsync(id);
            if (function == null)
                return NotFound(new { message = $"Module function with ID {id} not found" });

            function.Name = request.Name ?? function.Name;
            function.Description = request.Description ?? function.Description;
            function.IsEnabled = request.IsEnabled;

            await _moduleFunctionService.UpdateFunctionAsync(function);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating module function", error = ex.Message });
        }
    }

    /// <summary>
    /// Delete a module function
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var function = await _moduleFunctionService.GetFunctionByIdAsync(id);
            if (function == null)
                return NotFound(new { message = $"Module function with ID {id} not found" });

            await _moduleFunctionService.DeleteFunctionAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error deleting module function", error = ex.Message });
        }
    }

    /// <summary>
    /// Toggle module function enabled status
    /// </summary>
    [HttpPut("{id}/toggle")]
    public async Task<IActionResult> Toggle(int id, [FromBody] ToggleFunctionRequest request)
    {
        try
        {
            await _moduleFunctionService.ToggleFunctionAsync(id, request.IsEnabled);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error toggling module function", error = ex.Message });
        }
    }
}

public class CreateModuleFunctionRequest
{
    public int ModuleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
}

public class UpdateModuleFunctionRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsEnabled { get; set; }
}

public class ToggleFunctionRequest
{
    public bool IsEnabled { get; set; }
}
