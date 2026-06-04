using Postgrest;
using Supabase.Gotrue.Exceptions;
using VetInHouse.Entities;
using VetInHouse.Models;

namespace VetInHouse.Services;

public class AuthService(SupabaseService supabase)
{
    public async Task<(AuthResponse? Data, string? Error)> RegistrarAsync(RegistroRequest request)
    {
        try
        {
            var session = await supabase.AnonClient.Auth.SignUp(request.Email, request.Password);
            if (session?.User?.Id is null)
                return (null, "No se pudo crear el usuario");

            var perfil = new PerfilEntity
            {
                UserId = Guid.Parse(session.User.Id),
                Nombre = request.Nombre,
                Email = request.Email,
                Telefono = request.Telefono,
                Rol = request.Rol
            };

            await supabase.AdminClient.From<PerfilEntity>().Insert(perfil);

            return (new AuthResponse(
                session.AccessToken ?? string.Empty,
                request.Email,
                request.Rol,
                Guid.Parse(session.User.Id)), null);
        }
        catch (GotrueException ex)
        {
            return (null, ParseGotrueError(ex.Message));
        }
        catch (Exception ex)
        {
            return (null, ex.Message);
        }
    }

    public async Task<(AuthResponse? Data, string? Error)> LoginAsync(LoginRequest request)
    {
        try
        {
            var session = await supabase.AnonClient.Auth.SignIn(request.Email, request.Password);
            if (session?.AccessToken is null || session.User?.Id is null)
                return (null, "Credenciales inválidas");

            var result = await supabase.AdminClient
                .From<PerfilEntity>()
                .Filter("user_id", Constants.Operator.Equals, session.User.Id)
                .Get();

            var rol = result.Models.FirstOrDefault()?.Rol ?? "cliente";

            return (new AuthResponse(
                session.AccessToken,
                request.Email,
                rol,
                Guid.Parse(session.User.Id)), null);
        }
        catch (GotrueException ex)
        {
            return (null, ParseGotrueError(ex.Message));
        }
        catch (Exception ex)
        {
            return (null, ex.Message);
        }
    }

    private static string ParseGotrueError(string raw)
    {
        if (raw.Contains("invalid_credentials")) return "Correo o contraseña incorrectos";
        if (raw.Contains("email_not_confirmed")) return "Debes confirmar tu correo antes de iniciar sesión";
        if (raw.Contains("user_already_exists") || raw.Contains("already registered")) return "Este correo ya está registrado";
        if (raw.Contains("weak_password")) return "La contraseña es muy débil (mínimo 6 caracteres)";
        return "Error de autenticación";
    }
}
