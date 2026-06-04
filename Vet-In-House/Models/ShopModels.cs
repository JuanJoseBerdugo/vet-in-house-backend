using System.ComponentModel.DataAnnotations;

namespace VetInHouse.Models;

public record ProductoResponse(
    Guid Id,
    string Nombre,
    string? Descripcion,
    decimal Precio,
    string Categoria,
    string? FotoUrl,
    int Stock
);

public record PedidoItemRequest(
    [Required] Guid ProductoId,
    [Required, Range(1, 999)] int Cantidad
);

public record CrearPedidoRequest(
    [Required] string DireccionEnvio,
    [Required, MinLength(1)] List<PedidoItemRequest> Items
);

public record PedidoResponse(
    Guid Id,
    decimal Total,
    string Estado,
    string DireccionEnvio,
    DateTime CreatedAt
);

public record CambiarEstadoPedidoRequest([Required] string Estado);

public record ProcesarPagoRequest(
    [Required] string Metodo,
    Guid? ServicioId,
    Guid? PedidoId
);
