using Domain.Enums;

namespace Application.Common.Dtos.Reservation;

/// <summary>
/// Dto for Reservation reduced Read operation
/// </summary>
/// <param name="Id">Guid identifier of Reservation</param>
/// <param name="BookId">Guid identifier of Book</param>
/// <param name="ReceiptDate">DateTime which represents date and time of book receiving</param>
/// <param name="ReturnDate">DateTime which represents date and time of book returning</param>
/// <param name="Status">Enum which represents status book of reservation</param>
public record ReadReservationReducedDto(
    Guid Id,
    Guid BookId,
    DateTime ReceiptDate,
    DateTime ReturnDate,
    ReservationStatuses Status);
