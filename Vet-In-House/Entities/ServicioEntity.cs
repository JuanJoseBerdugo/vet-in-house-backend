using Postgrest.Attributes;
using Postgrest.Models;

namespace VetInHouse.Entities;

[Table("servicios")]
public class ServicioEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("cliente_id")]
    public Guid ClienteId { get; set; }

    [Column("paseador_id")]
    public Guid? PaseadorId { get; set; }

    [Column("mascota_id")]
    public Guid MascotaId { get; set; }

    [Column("horas")]
    public decimal Horas { get; set; }

    [Column("tarifa_por_hora")]
    public decimal TarifaPorHora { get; set; }

    [Column("tarifa_total")]
    public decimal TarifaTotal { get; set; }

    [Column("estado")]
    public string Estado { get; set; } = "buscando";

    [Column("ubicacion_lat")]
    public decimal? UbicacionLat { get; set; }

    [Column("ubicacion_lng")]
    public decimal? UbicacionLng { get; set; }

    [Column("notas")]
    public string? Notas { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
