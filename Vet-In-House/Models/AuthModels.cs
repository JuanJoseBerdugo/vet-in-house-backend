using System.ComponentModel.DataAnnotations;

namespace VetInHouse.Models;

public record RegistroRequest(
    [Required] string Nombre,
    [Required, EmailAddress] string Email,
    [Required, MinLength(6)] string Password,
    string? Telefono,
    string Rol = "cliente"
);

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password
);

public record AuthResponse(
    string AccessToken,
    string Email,
    string Rol,
    Guid UserId
);
