namespace Application.Dtos.Reservation;

/// <summary>
/// DTO for Reservation Create operation
/// </summary>
/// <param name="BookId">Guid identifier of Book</param>
public record CreateReservationDto(
    Guid BookId);