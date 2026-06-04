using Postgrest;
using VetInHouse.Entities;
using VetInHouse.Models;

namespace VetInHouse.Services;

public class ServiciosService(SupabaseService supabase)
{
    private static readonly string[] EstadosValidos = ["en_progreso", "finalizado", "cancelado"];

    public async Task<ServicioResponse> SolicitarAsync(Guid clienteId, SolicitarServicioRequest request)
    {
        // Obtener tarifa activa
        var tarifaResult = await supabase.AdminClient
            .From<TarifaEntity>()
            .Filter("activa", Constants.Operator.Equals, "true")
            .Get();

        var tarifa = tarifaResult.Models.FirstOrDefault();
        var tarifaPorHora = tarifa?.PrecioPorHora ?? 25000m;
        var tarifaTotal = tarifaPorHora * request.Horas;

        var servicio = new ServicioEntity
        {
            ClienteId = clienteId,
            MascotaId = request.MascotaId,
            Horas = request.Horas,
            TarifaPorHora = tarifaPorHora,
            TarifaTotal = tarifaTotal,
            Estado = "buscando",
            UbicacionLat = request.UbicacionLat,
            UbicacionLng = request.UbicacionLng,
            Notas = request.Notas,
            UpdatedAt = DateTime.UtcNow
        };

        var inserted = await supabase.AdminClient.From<ServicioEntity>().Insert(servicio);
        return ToResponse(inserted.Models.First());
    }

    public async Task<List<ServicioResponse>> GetDisponiblesAsync()
    {
        var result = await supabase.AdminClient
            .From<ServicioEntity>()
            .Filter("estado", Constants.Operator.Equals, "buscando")
            .Get();

        return result.Models.Select(ToResponse).ToList();
    }

    public async Task<ServicioResponse?> AceptarAsync(Guid servicioId, Guid paseadorId)
    {
        var existing = await GetByIdAsync(servicioId);
        if (existing is null || existing.Estado != "buscando") return null;

        var updated = new ServicioEntity
        {
            Id = servicioId,
            ClienteId = existing.ClienteId,
            MascotaId = existing.MascotaId,
            PaseadorId = paseadorId,
            Horas = existing.Horas,
            TarifaPorHora = existing.TarifaPorHora,
            TarifaTotal = existing.TarifaTotal,
            Estado = "aceptado",
            UbicacionLat = existing.UbicacionLat,
            UbicacionLng = existing.UbicacionLng,
            Notas = existing.Notas,
            UpdatedAt = DateTime.UtcNow
        };

        var result = await supabase.AdminClient.From<ServicioEntity>().Update(updated);
        return ToResponse(result.Models.First());
    }

    public async Task<ServicioResponse?> CambiarEstadoAsync(Guid servicioId, Guid userId, CambiarEstadoRequest request)
    {
        if (!EstadosValidos.Contains(request.Estado)) return null;

        var existing = await GetByIdAsync(servicioId);
        if (existing is null) return null;

        // Verificar que el solicitante sea parte del servicio
        if (existing.ClienteId != userId && existing.PaseadorId != userId) return null;

        var updated = new ServicioEntity
        {
            Id = servicioId,
            ClienteId = existing.ClienteId,
            MascotaId = existing.MascotaId,
            PaseadorId = existing.PaseadorId,
            Horas = existing.Horas,
            TarifaPorHora = existing.TarifaPorHora,
            TarifaTotal = existing.TarifaTotal,
            Estado = request.Estado,
            UbicacionLat = existing.UbicacionLat,
            UbicacionLng = existing.UbicacionLng,
            Notas = existing.Notas,
            UpdatedAt = DateTime.UtcNow
        };

        var result = await supabase.AdminClient.From<ServicioEntity>().Update(updated);
        return ToResponse(result.Models.First());
    }

    public async Task<List<ServicioResponse>> GetHistorialAsync(Guid userId)
    {
        // Historial del cliente
        var clienteResult = await supabase.AdminClient
            .From<ServicioEntity>()
            .Filter("cliente_id", Constants.Operator.Equals, userId.ToString())
            .Filter("estado", Constants.Operator.Equals, "finalizado")
            .Get();

        // Historial del paseador
        var paseadorResult = await supabase.AdminClient
            .From<ServicioEntity>()
            .Filter("paseador_id", Constants.Operator.Equals, userId.ToString())
            .Filter("estado", Constants.Operator.Equals, "finalizado")
            .Get();

        return clienteResult.Models
            .Concat(paseadorResult.Models)
            .DistinctBy(x => x.Id)
            .OrderByDescending(x => x.CreatedAt)
            .Select(ToResponse)
            .ToList();
    }

    private async Task<ServicioEntity?> GetByIdAsync(Guid id)
    {
        var result = await supabase.AdminClient
            .From<ServicioEntity>()
            .Filter("id", Constants.Operator.Equals, id.ToString())
            .Get();
        return result.Models.FirstOrDefault();
    }

    private static ServicioResponse ToResponse(ServicioEntity s) =>
        new(s.Id, s.ClienteId, s.PaseadorId, s.MascotaId,
            s.Horas, s.TarifaPorHora, s.TarifaTotal,
            s.Estado, s.UbicacionLat, s.UbicacionLng, s.Notas, s.CreatedAt);
}
