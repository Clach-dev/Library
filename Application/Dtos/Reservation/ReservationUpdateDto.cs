namespace Application.Dtos.Reservation;

/// <summary>
/// DTO for Reservation Update operation
/// </summary>
/// <param name="BookId">Guid identifier of Book</param>
/// <param name="ReceiptDate">DateTime which represents date and time of book receiving</param>
/// <param name="ReturnDate">DateTime which represents date and time when the book need be returned</param>
public record ReservationUpdateDto(
    Guid BookId,
    DateTime ReceiptDate,
    DateTime ReturnDate);