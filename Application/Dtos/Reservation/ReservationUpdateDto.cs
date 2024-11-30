namespace Application.Dtos.Reservation;

/// <summary>
/// DTO for Reservation Update operation
/// </summary>
/// <param name="Id">Guid identifier of Reservation</param>
/// <param name="ReturnDate">DateTime which represents date and time when the book need be returned</param>
public record ReservationUpdateDto(
    Guid Id,
    DateTime ReturnDate);