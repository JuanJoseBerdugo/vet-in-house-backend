using Postgrest;
using VetInHouse.Entities;
using VetInHouse.Models;

namespace VetInHouse.Services;

public class TrackingService(SupabaseService supabase)
{
    public async Task<TrackingResponse> RegistrarPosicionAsync(Guid servicioId, TrackingRequest request)
    {
        var point = new TrackingEntity
        {
            ServicioId = servicioId,
            Latitud = request.Latitud,
            Longitud = request.Longitud,
            Timestamp = DateTime.UtcNow
        };

        var inserted = await supabase.AdminClient.From<TrackingEntity>().Insert(point);
        var p = inserted.Models.First();
        return new TrackingResponse(p.ServicioId, p.Latitud, p.Longitud, p.Timestamp);
    }

    public async Task<TrackingResponse?> GetUltimaPosicionAsync(Guid servicioId)
    {
        var result = await supabase.AdminClient
            .From<TrackingEntity>()
            .Filter("servicio_id", Constants.Operator.Equals, servicioId.ToString())
            .Order("timestamp", Postgrest.Constants.Ordering.Descending)
            .Limit(1)
            .Get();

        var p = result.Models.FirstOrDefault();
        if (p is null) return null;
        return new TrackingResponse(p.ServicioId, p.Latitud, p.Longitud, p.Timestamp);
    }
}
