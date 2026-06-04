using Postgrest.Attributes;
using Postgrest.Models;

namespace VetInHouse.Entities;

[Table("tracking")]
public class TrackingEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("servicio_id")]
    public Guid ServicioId { get; set; }

    [Column("latitud")]
    public decimal Latitud { get; set; }

    [Column("longitud")]
    public decimal Longitud { get; set; }

    [Column("timestamp")]
    public DateTime Timestamp { get; set; }
}
