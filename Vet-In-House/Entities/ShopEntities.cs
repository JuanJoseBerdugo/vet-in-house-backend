using Postgrest.Attributes;
using Postgrest.Models;

namespace VetInHouse.Entities;

[Table("productos")]
public class ProductoEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Column("precio")]
    public decimal Precio { get; set; }

    [Column("categoria")]
    public string Categoria { get; set; } = string.Empty;

    [Column("foto_url")]
    public string? FotoUrl { get; set; }

    [Column("stock")]
    public int Stock { get; set; }

    [Column("activo")]
    public bool Activo { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}

[Table("pedidos")]
public class PedidoEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("cliente_id")]
    public Guid ClienteId { get; set; }

    [Column("total")]
    public decimal Total { get; set; }

    [Column("estado")]
    public string Estado { get; set; } = "pendiente";

    [Column("direccion_envio")]
    public string DireccionEnvio { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

[Table("pedido_items")]
public class PedidoItemEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("pedido_id")]
    public Guid PedidoId { get; set; }

    [Column("producto_id")]
    public Guid ProductoId { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }

    [Column("precio_unitario")]
    public decimal PrecioUnitario { get; set; }
}
