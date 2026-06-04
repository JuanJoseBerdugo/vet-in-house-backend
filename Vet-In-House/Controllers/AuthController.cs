using Microsoft.AspNetCore.Mvc;
using VetInHouse.Models;
using VetInHouse.Services;

namespace VetInHouse.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("registro")]
    public async Task<IActionResult> Registro([FromBody] RegistroRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var (data, error) = await authService.RegistrarAsync(request);
        if (data is null)
            return BadRequest(ApiResponse.Fail(error ?? "No se pudo crear el usuario"));

        return Ok(ApiResponse<AuthResponse>.Ok(data));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var (data, error) = await authService.LoginAsync(request);
        if (data is null)
            return Unauthorized(ApiResponse.Fail(error ?? "Credenciales inválidas"));

        return Ok(ApiResponse<AuthResponse>.Ok(data));
    }
}
