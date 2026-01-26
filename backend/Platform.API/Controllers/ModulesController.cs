using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [HttpPut("{id}/toggle")]
    public async Task<IActionResult> Toggle(int id, [FromBody] ToggleModuleRequest request)
    {
        await _moduleService.ToggleModuleAsync(id, request.IsEnabled);
        return NoContent();
    }
}

public class ToggleModuleRequest
{
    public bool IsEnabled { get; set; }
}
