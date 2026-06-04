using Microsoft.AspNetCore.Mvc;
using Postgrest;
using VetInHouse.Entities;
using VetInHouse.Models;
using VetInHouse.Services;

namespace VetInHouse.Controllers;

[ApiController]
[Route("api/tarifas")]
public class TarifasController(SupabaseService supabase) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTarifas()
    {
        var result = await supabase.AdminClient
            .From<TarifaEntity>()
            .Filter("activa", Constants.Operator.Equals, "true")
            .Get();

        var tarifas = result.Models.Select(t => new
        {
            t.Id,
            t.TipoServicio,
            t.PrecioPorHora,
            t.PrecioBase
        });

        return Ok(ApiResponse<object>.Ok(tarifas));
    }
}
