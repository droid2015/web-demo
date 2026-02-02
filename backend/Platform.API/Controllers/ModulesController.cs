using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Platform.Core.Domain.Entities;
using Platform.Core.Services;

namespace Platform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ModulesController : ControllerBase
{
    private readonly ModuleService _moduleService;

    public ModulesController(ModuleService moduleService)
    {
        _moduleService = moduleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var modules = await _moduleService.GetAllModulesAsync();
        return Ok(modules);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var module = await _moduleService.GetModuleByIdAsync(id);
        if (module == null)
            return NotFound();
        
        return Ok(module);
    }

    /// <summary>
    /// Register a new module
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterModuleRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new { message = "Module name is required" });

            if (string.IsNullOrWhiteSpace(request.Version))
                return BadRequest(new { message = "Module version is required" });

            var module = new Module
            {
                Name = request.Name,
                Version = request.Version,
                IsEnabled = request.IsEnabled,
                LoadOrder = request.LoadOrder
            };

            var createdModule = await _moduleService.CreateModuleAsync(module);
            return CreatedAtAction(nameof(GetById), new { id = createdModule.Id }, createdModule);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error registering module", error = ex.Message });
        }
    }

    /// <summary>
    /// Update module metadata
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateModuleRequest request)
    {
        try
        {
            var module = await _moduleService.GetModuleByIdAsync(id);
            if (module == null)
                return NotFound(new { message = $"Module with ID {id} not found" });

            module.Version = request.Version ?? module.Version;
            module.LoadOrder = request.LoadOrder;

            await _moduleService.UpdateModuleAsync(module);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error updating module", error = ex.Message });
        }
    }

    [HttpPut("{id}/toggle")]
    public async Task<IActionResult> Toggle(int id, [FromBody] ToggleModuleRequest request)
    {
        await _moduleService.ToggleModuleAsync(id, request.IsEnabled);
        return NoContent();
    }
}

public class RegisterModuleRequest
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;
    public int LoadOrder { get; set; } = 0;
}

public class UpdateModuleRequest
{
    public string? Version { get; set; }
    public int LoadOrder { get; set; }
}

public class ToggleModuleRequest
{
    public bool IsEnabled { get; set; }
}
