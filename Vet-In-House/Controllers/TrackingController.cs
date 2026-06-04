using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetInHouse.Models;
using VetInHouse.Services;

namespace VetInHouse.Controllers;

[ApiController]
[Route("api/tracking")]
[Authorize]
public class TrackingController(TrackingService trackingService) : ControllerBase
{
    [HttpPost("{servicioId:guid}")]
    public async Task<IActionResult> RegistrarPosicion(Guid servicioId, [FromBody] TrackingRequest request)
    {
        var result = await trackingService.RegistrarPosicionAsync(servicioId, request);
        return Ok(ApiResponse<TrackingResponse>.Ok(result));
    }

    [HttpGet("{servicioId:guid}")]
    public async Task<IActionResult> GetUltimaPosicion(Guid servicioId)
    {
        var result = await trackingService.GetUltimaPosicionAsync(servicioId);
        if (result is null) return NotFound(ApiResponse.Fail("Sin datos de tracking para este servicio"));

        return Ok(ApiResponse<TrackingResponse>.Ok(result));
    }
}
