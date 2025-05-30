namespace Application.Common.Dtos.Reservation;

/// <summary>
/// Dto for reading reservations with pagination.
/// </summary>
/// <param name="Reservations">IEnumerable_ReadReservationDto which contains Reservations</param>
/// <param name="TotalCount">int which contains TotalCount of reservations</param>
public record ReadReservationsDto(
    IEnumerable<ReadReservationDto> Reservations,
    int TotalCount);