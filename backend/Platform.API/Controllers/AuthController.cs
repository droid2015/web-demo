using Microsoft.AspNetCore.Mvc;
using Platform.Core.Services;
using Platform.Core.Domain.Entities;
using Platform.Infrastructure.Services;

namespace Platform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthService _authService;
    private readonly UserPermissionService _userPermissionService;

    public AuthController(UserService userService, AuthService authService, UserPermissionService userPermissionService)
    {
        _userService = userService;
        _authService = authService;
        _userPermissionService = userPermissionService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.ValidateUserAsync(request.Username, request.Password);
        
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        var token = _authService.GenerateJwtToken(user);
        var roles = await _userPermissionService.GetUserRolesAsync(user.Id);
        var modules = await _userPermissionService.GetUserModulesAsync(user.Id);
        
        return Ok(new
        {
            token,
            user = new
            {
                user.Id,
                user.Username,
                user.Email,
                roles = roles.ToList(),
                modules = modules.Select(m => new
                {
                    m.Id,
                    m.Name,
                    m.Version,
                    m.IsEnabled
                }).ToList()
            }
        });
    }
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
