using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetInHouse.Models;
using VetInHouse.Services;

namespace VetInHouse.Controllers;

[ApiController]
[Route("api/servicios")]
[Authorize]
public class ServiciosController(ServiciosService serviciosService) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirst("sub")?.Value
        ?? throw new UnauthorizedAccessException());

    [HttpPost("solicitar")]
    public async Task<IActionResult> Solicitar([FromBody] SolicitarServicioRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var servicio = await serviciosService.SolicitarAsync(UserId, request);
        return CreatedAtAction(nameof(GetHistorial), ApiResponse<ServicioResponse>.Ok(servicio));
    }

    [HttpGet("disponibles")]
    public async Task<IActionResult> GetDisponibles()
    {
        var servicios = await serviciosService.GetDisponiblesAsync();
        return Ok(ApiResponse<List<ServicioResponse>>.Ok(servicios));
    }

    [HttpPatch("{id:guid}/aceptar")]
    public async Task<IActionResult> Aceptar(Guid id)
    {
        var servicio = await serviciosService.AceptarAsync(id, UserId);
        if (servicio is null)
            return BadRequest(ApiResponse.Fail("Servicio no disponible o ya fue tomado"));

        return Ok(ApiResponse<ServicioResponse>.Ok(servicio));
    }

    [HttpPatch("{id:guid}/estado")]
    public async Task<IActionResult> CambiarEstado(Guid id, [FromBody] CambiarEstadoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var servicio = await serviciosService.CambiarEstadoAsync(id, UserId, request);
        if (servicio is null)
            return BadRequest(ApiResponse.Fail("Estado inválido o sin permisos"));

        return Ok(ApiResponse<ServicioResponse>.Ok(servicio));
    }

    [HttpGet("historial")]
    public async Task<IActionResult> GetHistorial()
    {
        var historial = await serviciosService.GetHistorialAsync(UserId);
        return Ok(ApiResponse<List<ServicioResponse>>.Ok(historial));
    }
}
