using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetInHouse.Models;
using VetInHouse.Services;

namespace VetInHouse.Controllers;

[ApiController]
[Route("api/pedidos")]
[Authorize]
public class PedidosController(ShopService shopService) : ControllerBase
{
    private Guid UserId => Guid.Parse(User.FindFirst("sub")?.Value
        ?? throw new UnauthorizedAccessException());

    [HttpPost]
    public async Task<IActionResult> CrearPedido([FromBody] CrearPedidoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var pedido = await shopService.CrearPedidoAsync(UserId, request);
            return CreatedAtAction(nameof(GetMisOrdenes), ApiResponse<PedidoResponse>.Ok(pedido));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ApiResponse.Fail(ex.Message));
        }
    }

    [HttpGet("mis-ordenes")]
    public async Task<IActionResult> GetMisOrdenes()
    {
        var pedidos = await shopService.GetPedidosByClienteAsync(UserId);
        return Ok(ApiResponse<List<PedidoResponse>>.Ok(pedidos));
    }

    [HttpPatch("{id:guid}/estado")]
    public async Task<IActionResult> CambiarEstado(Guid id, [FromBody] CambiarEstadoPedidoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var pedido = await shopService.CambiarEstadoPedidoAsync(id, request);
        if (pedido is null) return BadRequest(ApiResponse.Fail("Pedido no encontrado o estado inválido"));

        return Ok(ApiResponse<PedidoResponse>.Ok(pedido));
    }
}
