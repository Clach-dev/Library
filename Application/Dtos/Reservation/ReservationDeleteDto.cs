namespace Application.Dtos.Reservation;

/// <summary>
/// Dto for Reservation Delete operation
/// </summary>
/// <param name="Id">Guid identifier of reservation</param>
public record ReservationDeleteDto(Guid Id);