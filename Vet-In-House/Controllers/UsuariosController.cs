using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Postgrest;
using VetInHouse.Entities;
using VetInHouse.Models;
using VetInHouse.Services;

namespace VetInHouse.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class UsuariosController(SupabaseService supabase) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirst("sub")?.Value
        ?? throw new UnauthorizedAccessException());

    [HttpGet("usuarios/perfil")]
    public async Task<IActionResult> GetPerfil()
    {
        var result = await supabase.AdminClient
            .From<PerfilEntity>()
            .Filter("user_id", Constants.Operator.Equals, UserId.ToString())
            .Get();

        var perfil = result.Models.FirstOrDefault();
        if (perfil is null) return NotFound(ApiResponse.Fail("Perfil no encontrado"));

        return Ok(ApiResponse<PerfilResponse>.Ok(new PerfilResponse(
            perfil.Id, perfil.Nombre, perfil.Email,
            perfil.Telefono, perfil.FotoUrl, perfil.Rol)));
    }

    [HttpPut("usuarios/perfil")]
    public async Task<IActionResult> ActualizarPerfil([FromBody] ActualizarPerfilRequest request)
    {
        var result = await supabase.AdminClient
            .From<PerfilEntity>()
            .Filter("user_id", Constants.Operator.Equals, UserId.ToString())
            .Get();

        var perfil = result.Models.FirstOrDefault();
        if (perfil is null) return NotFound(ApiResponse.Fail("Perfil no encontrado"));

        if (request.Nombre is not null) perfil.Nombre = request.Nombre;
        if (request.Telefono is not null) perfil.Telefono = request.Telefono;
        if (request.FotoUrl is not null) perfil.FotoUrl = request.FotoUrl;

        var updated = await supabase.AdminClient.From<PerfilEntity>().Update(perfil);
        var p = updated.Models.First();

        return Ok(ApiResponse<PerfilResponse>.Ok(new PerfilResponse(
            p.Id, p.Nombre, p.Email, p.Telefono, p.FotoUrl, p.Rol)));
    }

    [HttpPost("paseadores/verificacion")]
    public async Task<IActionResult> SolicitarVerificacion([FromBody] VerificacionPaseadorRequest request)
    {
        var existing = await supabase.AdminClient
            .From<PaseadorEntity>()
            .Filter("user_id", Constants.Operator.Equals, UserId.ToString())
            .Get();

        if (existing.Models.Any())
        {
            var paseador = existing.Models.First();
            paseador.DocumentoUrl = request.DocumentoUrl;
            paseador.Biografia = request.Biografia;
            paseador.EstadoVerificacion = "pendiente";
            await supabase.AdminClient.From<PaseadorEntity>().Update(paseador);
        }
        else
        {
            await supabase.AdminClient.From<PaseadorEntity>().Insert(new PaseadorEntity
            {
                UserId = UserId,
                DocumentoUrl = request.DocumentoUrl,
                Biografia = request.Biografia,
                EstadoVerificacion = "pendiente"
            });
        }

        return Ok(ApiResponse.Ok("Solicitud de verificación enviada"));
    }

    [HttpPatch("paseadores/disponibilidad")]
    public async Task<IActionResult> CambiarDisponibilidad([FromBody] DisponibilidadRequest request)
    {
        var result = await supabase.AdminClient
            .From<PaseadorEntity>()
            .Filter("user_id", Constants.Operator.Equals, UserId.ToString())
            .Get();

        var paseador = result.Models.FirstOrDefault();
        if (paseador is null) return NotFound(ApiResponse.Fail("Perfil de paseador no encontrado"));

        if (paseador.EstadoVerificacion != "aprobado")
            return BadRequest(ApiResponse.Fail("Tu cuenta no ha sido verificada aún"));

        paseador.Disponible = request.Disponible;
        await supabase.AdminClient.From<PaseadorEntity>().Update(paseador);

        return Ok(ApiResponse.Ok(request.Disponible ? "En línea" : "Desconectado"));
    }
}
