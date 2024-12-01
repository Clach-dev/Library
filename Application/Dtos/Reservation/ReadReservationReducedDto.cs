using Application.Dtos.Book;

namespace Application.Dtos.Reservation;

/// <summary>
/// Dto for Reservation reduced Read operation
/// </summary>
/// <param name="Id">Guid identifier of Reservation</param>
/// <param name="ReceiptDate">DateTime which represents date and time of book receiving</param>
/// <param name="ReturnDate">DateTime which represents date and time of book returning</param>
/// <param name="IsReturned">bool which represents if book is returned</param>
/// <param name="Book">ReadBookReducedDto which contains Book</param>
public record ReadReservationReducedDto(
    Guid Id,
    DateTime ReceiptDate,
    DateTime ReturnDate,
    bool IsReturned,
    ReadBookReducedDto Book);
