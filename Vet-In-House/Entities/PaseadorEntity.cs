using Postgrest.Attributes;
using Postgrest.Models;

namespace VetInHouse.Entities;

[Table("paseadores")]
public class PaseadorEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("documento_url")]
    public string? DocumentoUrl { get; set; }

    [Column("biografia")]
    public string? Biografia { get; set; }

    [Column("estado_verificacion")]
    public string EstadoVerificacion { get; set; } = "pendiente";

    [Column("disponible")]
    public bool Disponible { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
