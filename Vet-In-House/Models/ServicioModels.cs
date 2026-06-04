using System.ComponentModel.DataAnnotations;

namespace VetInHouse.Models;

public record SolicitarServicioRequest(
    [Required] Guid MascotaId,
    [Required, Range(0.5, 24)] decimal Horas,
    decimal UbicacionLat,
    decimal UbicacionLng,
    string? Notas
);

public record CambiarEstadoRequest([Required] string Estado);

public record ServicioResponse(
    Guid Id,
    Guid ClienteId,
    Guid? PaseadorId,
    Guid MascotaId,
    decimal Horas,
    decimal TarifaPorHora,
    decimal TarifaTotal,
    string Estado,
    decimal? UbicacionLat,
    decimal? UbicacionLng,
    string? Notas,
    DateTime CreatedAt
);
