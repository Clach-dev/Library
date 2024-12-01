using Application.Dtos.Book;
using Application.Dtos.User;

namespace Application.Dtos.Reservation;

/// <summary>
/// DTO for Reservation Read operation
/// </summary>
/// <param name="Id">Guid identifier of Reservation</param>
/// <param name="ReceiptDate">DateTime which represents date and time of book receiving</param>
/// <param name="ReturnDate">DateTime which represents date and time of book returning</param>
/// <param name="IsReturned">bool which represents if book is returned</param>
/// <param name="Book">BookReducedReadDto which contains Book</param>
/// <param name="User">ReadUserReducedDto which contains User</param>
public record ReadReservationDto(
    Guid Id,
    DateTime ReceiptDate,
    DateTime ReturnDate,
    bool IsReturned,
    ReadBookReducedDto Book,
    ReadUserReducedDto User);
