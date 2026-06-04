using System.ComponentModel.DataAnnotations;

namespace VetInHouse.Models;

public record CrearMascotaRequest(
    [Required] string Nombre,
    [Required] string Raza,
    decimal? Peso,
    int? Edad,
    string? FotoUrl,
    string? IndicacionesMedicas
);

public record ActualizarMascotaRequest(
    string? Nombre,
    string? Raza,
    decimal? Peso,
    int? Edad,
    string? FotoUrl,
    string? IndicacionesMedicas
);

public record MascotaResponse(
    Guid Id,
    string Nombre,
    string Raza,
    decimal? Peso,
    int? Edad,
    string? FotoUrl,
    string? IndicacionesMedicas
);
