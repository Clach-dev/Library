using Domain.Enums;

namespace Application.Common.Dtos.Reservation;

/// <summary>
/// Dto for reservations by filter read operation
/// </summary>
/// <param name="UserId">Guid identifier of User</param>
/// <param name="BookId">Guid identifier of Book</param>
/// <param name="FromRecieptDate">DateTime which indicates the lower limit of the selection of date and time when the book was receipt</param>
/// <param name="ToRecieptDate">DateTime which indicates the upper limit of the selection of date and time when the book was receipt</param>
/// <param name="FromReturnDate">DateTime which indicates the lower limit of the selection of date and time when the book could be returned</param>
/// <param name="ToReturnDate">DateTime which indicates the upper limit of the selection of date and time when the book could be returned</param>
/// <param name="Status">Enum which represents status of reservation</param>
/// <param name="PageInfoDto">Dto which contains Pagination information</param>
public record FilterReservationsDto(
    Guid? UserId,
    Guid? BookId,
    DateTime? FromRecieptDate,
    DateTime? ToRecieptDate,
    DateTime? FromReturnDate,
    DateTime? ToReturnDate,
    ReservationStatuses? Status,
    PageInfoDto PageInfoDto);