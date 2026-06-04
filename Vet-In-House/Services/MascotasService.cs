using Postgrest;
using VetInHouse.Entities;
using VetInHouse.Models;

namespace VetInHouse.Services;

public class MascotasService(SupabaseService supabase)
{
    public async Task<List<MascotaResponse>> GetByOwnerAsync(Guid ownerId)
    {
        var result = await supabase.AdminClient
            .From<MascotaEntity>()
            .Filter("owner_id", Constants.Operator.Equals, ownerId.ToString())
            .Get();

        return result.Models.Select(ToResponse).ToList();
    }

    public async Task<MascotaResponse?> GetByIdAsync(Guid id, Guid ownerId)
    {
        var result = await supabase.AdminClient
            .From<MascotaEntity>()
            .Filter("id", Constants.Operator.Equals, id.ToString())
            .Filter("owner_id", Constants.Operator.Equals, ownerId.ToString())
            .Get();

        return result.Models.FirstOrDefault() is { } m ? ToResponse(m) : null;
    }

    public async Task<MascotaResponse> CrearAsync(Guid ownerId, CrearMascotaRequest request)
    {
        var entity = new MascotaEntity
        {
            OwnerId = ownerId,
            Nombre = request.Nombre,
            Raza = request.Raza,
            Peso = request.Peso,
            Edad = request.Edad,
            FotoUrl = request.FotoUrl,
            IndicacionesMedicas = request.IndicacionesMedicas
        };

        var inserted = await supabase.AdminClient.From<MascotaEntity>().Insert(entity);
        return ToResponse(inserted.Models.First());
    }

    public async Task<MascotaResponse?> ActualizarAsync(Guid id, Guid ownerId, ActualizarMascotaRequest request)
    {
        var existing = await GetByIdAsync(id, ownerId);
        if (existing is null) return null;

        var updated = new MascotaEntity
        {
            Id = id,
            OwnerId = ownerId,
            Nombre = request.Nombre ?? existing.Nombre,
            Raza = request.Raza ?? existing.Raza,
            Peso = request.Peso ?? existing.Peso,
            Edad = request.Edad ?? existing.Edad,
            FotoUrl = request.FotoUrl ?? existing.FotoUrl,
            IndicacionesMedicas = request.IndicacionesMedicas ?? existing.IndicacionesMedicas
        };

        var result = await supabase.AdminClient.From<MascotaEntity>().Update(updated);
        return ToResponse(result.Models.First());
    }

    public async Task<bool> EliminarAsync(Guid id, Guid ownerId)
    {
        var existing = await GetByIdAsync(id, ownerId);
        if (existing is null) return false;

        await supabase.AdminClient
            .From<MascotaEntity>()
            .Filter("id", Constants.Operator.Equals, id.ToString())
            .Filter("owner_id", Constants.Operator.Equals, ownerId.ToString())
            .Delete();

        return true;
    }

    private static MascotaResponse ToResponse(MascotaEntity m) =>
        new(m.Id, m.Nombre, m.Raza, m.Peso, m.Edad, m.FotoUrl, m.IndicacionesMedicas);
}
