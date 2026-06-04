using Postgrest.Attributes;
using Postgrest.Models;

namespace VetInHouse.Entities;

[Table("mascotas")]
public class MascotaEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("owner_id")]
    public Guid OwnerId { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("raza")]
    public string Raza { get; set; } = string.Empty;

    [Column("peso")]
    public decimal? Peso { get; set; }

    [Column("edad")]
    public int? Edad { get; set; }

    [Column("foto_url")]
    public string? FotoUrl { get; set; }

    [Column("indicaciones_medicas")]
    public string? IndicacionesMedicas { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
