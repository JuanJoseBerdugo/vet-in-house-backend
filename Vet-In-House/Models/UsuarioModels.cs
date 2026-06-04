namespace VetInHouse.Models;

public record PerfilResponse(
    Guid Id,
    string Nombre,
    string Email,
    string? Telefono,
    string? FotoUrl,
    string Rol
);

public record ActualizarPerfilRequest(
    string? Nombre,
    string? Telefono,
    string? FotoUrl
);

public record VerificacionPaseadorRequest(
    string DocumentoUrl,
    string Biografia
);

public record DisponibilidadRequest(bool Disponible);
