using Postgrest;
using VetInHouse.Entities;
using VetInHouse.Models;

namespace VetInHouse.Services;

public class ShopService(SupabaseService supabase)
{
    public async Task<List<ProductoResponse>> GetProductosAsync()
    {
        var result = await supabase.AdminClient
            .From<ProductoEntity>()
            .Filter("activo", Constants.Operator.Equals, "true")
            .Get();

        return result.Models.Select(ToProductoResponse).ToList();
    }

    public async Task<ProductoResponse?> GetProductoByIdAsync(Guid id)
    {
        var result = await supabase.AdminClient
            .From<ProductoEntity>()
            .Filter("id", Constants.Operator.Equals, id.ToString())
            .Get();

        return result.Models.FirstOrDefault() is { } p ? ToProductoResponse(p) : null;
    }

    public async Task<PedidoResponse> CrearPedidoAsync(Guid clienteId, CrearPedidoRequest request)
    {
        decimal total = 0;
        var itemsConPrecio = new List<(PedidoItemRequest Item, decimal Precio)>();

        // Validar stock y calcular total
        foreach (var item in request.Items)
        {
            var prodResult = await supabase.AdminClient
                .From<ProductoEntity>()
                .Filter("id", Constants.Operator.Equals, item.ProductoId.ToString())
                .Get();

            var producto = prodResult.Models.FirstOrDefault()
                ?? throw new InvalidOperationException($"Producto {item.ProductoId} no encontrado");

            if (producto.Stock < item.Cantidad)
                throw new InvalidOperationException($"Stock insuficiente para '{producto.Nombre}'");

            itemsConPrecio.Add((item, producto.Precio));
            total += producto.Precio * item.Cantidad;
        }

        // Crear pedido
        var pedido = new PedidoEntity
        {
            ClienteId = clienteId,
            Total = total,
            Estado = "pendiente",
            DireccionEnvio = request.DireccionEnvio,
            UpdatedAt = DateTime.UtcNow
        };

        var pedidoInserted = await supabase.AdminClient.From<PedidoEntity>().Insert(pedido);
        var pedidoId = pedidoInserted.Models.First().Id;

        // Insertar items y reducir stock
        foreach (var (item, precio) in itemsConPrecio)
        {
            await supabase.AdminClient.From<PedidoItemEntity>().Insert(new PedidoItemEntity
            {
                PedidoId = pedidoId,
                ProductoId = item.ProductoId,
                Cantidad = item.Cantidad,
                PrecioUnitario = precio
            });

            // Reducir stock (fetch → update)
            var prod = (await supabase.AdminClient
                .From<ProductoEntity>()
                .Filter("id", Constants.Operator.Equals, item.ProductoId.ToString())
                .Get()).Models.First();

            prod.Stock -= item.Cantidad;
            await supabase.AdminClient.From<ProductoEntity>().Update(prod);
        }

        var p = pedidoInserted.Models.First();
        return new PedidoResponse(p.Id, p.Total, p.Estado, p.DireccionEnvio, p.CreatedAt);
    }

    public async Task<List<PedidoResponse>> GetPedidosByClienteAsync(Guid clienteId)
    {
        var result = await supabase.AdminClient
            .From<PedidoEntity>()
            .Filter("cliente_id", Constants.Operator.Equals, clienteId.ToString())
            .Get();

        return result.Models.Select(ToPedidoResponse).ToList();
    }

    public async Task<PedidoResponse?> CambiarEstadoPedidoAsync(Guid pedidoId, CambiarEstadoPedidoRequest request)
    {
        string[] validos = ["pagado", "enviado", "entregado", "cancelado"];
        if (!validos.Contains(request.Estado)) return null;

        var result = await supabase.AdminClient
            .From<PedidoEntity>()
            .Filter("id", Constants.Operator.Equals, pedidoId.ToString())
            .Get();

        var pedido = result.Models.FirstOrDefault();
        if (pedido is null) return null;

        pedido.Estado = request.Estado;
        pedido.UpdatedAt = DateTime.UtcNow;

        var updated = await supabase.AdminClient.From<PedidoEntity>().Update(pedido);
        return ToPedidoResponse(updated.Models.First());
    }

    private static ProductoResponse ToProductoResponse(ProductoEntity p) =>
        new(p.Id, p.Nombre, p.Descripcion, p.Precio, p.Categoria, p.FotoUrl, p.Stock);

    private static PedidoResponse ToPedidoResponse(PedidoEntity p) =>
        new(p.Id, p.Total, p.Estado, p.DireccionEnvio, p.CreatedAt);
}
