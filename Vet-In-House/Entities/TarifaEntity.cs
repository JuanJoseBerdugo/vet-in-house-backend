using Postgrest.Attributes;
using Postgrest.Models;

namespace VetInHouse.Entities;

[Table("tarifas")]
public class TarifaEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("tipo_servicio")]
    public string TipoServicio { get; set; } = string.Empty;

    [Column("precio_por_hora")]
    public decimal PrecioPorHora { get; set; }

    [Column("precio_base")]
    public decimal PrecioBase { get; set; }

    [Column("activa")]
    public bool Activa { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
