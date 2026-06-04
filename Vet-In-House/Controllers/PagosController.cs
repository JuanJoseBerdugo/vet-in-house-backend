using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetInHouse.Entities;
using VetInHouse.Models;
using VetInHouse.Services;

namespace VetInHouse.Controllers;

[ApiController]
[Route("api/pagos")]
[Authorize]
public class PagosController(SupabaseService supabase) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirst("sub")?.Value
        ?? throw new UnauthorizedAccessException());

    [HttpPost("procesar")]
    public async Task<IActionResult> ProcesarPago([FromBody] ProcesarPagoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        string[] metodosValidos = ["tarjeta", "pse", "nequi", "daviplata"];
        if (!metodosValidos.Contains(request.Metodo))
            return BadRequest(ApiResponse.Fail("Método de pago no soportado"));

        // TODO: Integrar con pasarela de pagos (ePayco, PayU, MercadoPago)
        // Aquí va la llamada al SDK de la pasarela elegida

        var pago = new PagoEntity
        {
            ServicioId = request.ServicioId,
            PedidoId = request.PedidoId,
            Monto = 0, // Calcular según servicio/pedido
            Metodo = request.Metodo,
            Estado = "pendiente",
            ReferenciaExterna = Guid.NewGuid().ToString() // Reemplazar con referencia real de pasarela
        };

        var inserted = await supabase.AdminClient.From<PagoEntity>().Insert(pago);
        var p = inserted.Models.First();

        return Ok(ApiResponse<object>.Ok(new
        {
            p.Id,
            p.Estado,
            p.Metodo,
            p.ReferenciaExterna,
            Mensaje = "Pago procesado — integra tu pasarela en PagosController"
        }));
    }
}
