namespace Application.Dtos.Reservation;

/// <summary>
/// DTO for Reservation Read operation
/// </summary>
/// <param name="Id">Guid identifier of Reservation</param>
/// <param name="BookId">Guid identifier of Book</param>
/// <param name="UserId">Guid identifier of User</param>
/// <param name="ReceiptDate">DateTime which represents date and time of book receiving</param>
/// <param name="ReturnDate">DateTime which represents date and time of book returning</param>
public record ReservationReadDto (
    Guid Id,
    Guid BookId,
    Guid UserId,
    DateTime ReceiptDate,
    DateTime ReturnDate);