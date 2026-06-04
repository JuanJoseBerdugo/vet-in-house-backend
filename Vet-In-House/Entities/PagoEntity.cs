using Postgrest.Attributes;
using Postgrest.Models;

namespace VetInHouse.Entities;

[Table("pagos")]
public class PagoEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("servicio_id")]
    public Guid? ServicioId { get; set; }

    [Column("pedido_id")]
    public Guid? PedidoId { get; set; }

    [Column("monto")]
    public decimal Monto { get; set; }

    [Column("metodo")]
    public string Metodo { get; set; } = string.Empty;

    [Column("estado")]
    public string Estado { get; set; } = "pendiente";

    [Column("referencia_externa")]
    public string? ReferenciaExterna { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
