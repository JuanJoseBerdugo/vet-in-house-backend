using Postgrest.Attributes;
using Postgrest.Models;

namespace VetInHouse.Entities;

[Table("profiles")]
public class PerfilEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("telefono")]
    public string? Telefono { get; set; }

    [Column("foto_url")]
    public string? FotoUrl { get; set; }

    [Column("rol")]
    public string Rol { get; set; } = "cliente";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
