namespace Application.Common.Dtos.Reservation;

/// <summary>
/// Dto for Reservation Delete operation
/// </summary>
/// <param name="Id">Guid identifier of reservation</param>
public record DeleteReservationDto(
    Guid Id);