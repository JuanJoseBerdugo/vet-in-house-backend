namespace VetInHouse.Models;

public record TrackingRequest(decimal Latitud, decimal Longitud);

public record TrackingResponse(
    Guid ServicioId,
    decimal Latitud,
    decimal Longitud,
    DateTime Timestamp
);
