using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetInHouse.Models;
using VetInHouse.Services;

namespace VetInHouse.Controllers;

[ApiController]
[Route("api/mascotas")]
[Authorize]
public class MascotasController(MascotasService mascotasService) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirst("sub")?.Value
        ?? throw new UnauthorizedAccessException());

    [HttpGet]
    public async Task<IActionResult> GetMascotas()
    {
        var mascotas = await mascotasService.GetByOwnerAsync(UserId);
        return Ok(ApiResponse<List<MascotaResponse>>.Ok(mascotas));
    }

    [HttpPost]
    public async Task<IActionResult> CrearMascota([FromBody] CrearMascotaRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var mascota = await mascotasService.CrearAsync(UserId, request);
        return CreatedAtAction(nameof(GetMascotas), ApiResponse<MascotaResponse>.Ok(mascota));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> ActualizarMascota(Guid id, [FromBody] ActualizarMascotaRequest request)
    {
        var mascota = await mascotasService.ActualizarAsync(id, UserId, request);
        if (mascota is null) return NotFound(ApiResponse.Fail("Mascota no encontrada"));

        return Ok(ApiResponse<MascotaResponse>.Ok(mascota));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> EliminarMascota(Guid id)
    {
        var deleted = await mascotasService.EliminarAsync(id, UserId);
        if (!deleted) return NotFound(ApiResponse.Fail("Mascota no encontrada"));

        return Ok(ApiResponse.Ok("Mascota eliminada"));
    }
}
