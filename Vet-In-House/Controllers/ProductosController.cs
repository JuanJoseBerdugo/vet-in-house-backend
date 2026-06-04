using Microsoft.AspNetCore.Mvc;
using VetInHouse.Models;
using VetInHouse.Services;

namespace VetInHouse.Controllers;

[ApiController]
[Route("api/productos")]
public class ProductosController(ShopService shopService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProductos()
    {
        var productos = await shopService.GetProductosAsync();
        return Ok(ApiResponse<List<ProductoResponse>>.Ok(productos));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProducto(Guid id)
    {
        var producto = await shopService.GetProductoByIdAsync(id);
        if (producto is null) return NotFound(ApiResponse.Fail("Producto no encontrado"));

        return Ok(ApiResponse<ProductoResponse>.Ok(producto));
    }
}
