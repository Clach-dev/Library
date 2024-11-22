﻿namespace Application.Dtos.Reservation;

/// <summary>
/// DTO for Reservation Create operation
/// </summary>
/// <param name="BookId">Guid identifier of Book</param>
/// <param name="ReceiptDate">DateTime which represents date and time of book receiving</param>
/// <param name="ReturnDate">DateTime which represents date and time when the book need be returned</param>
public record ReservationCreateDto(
    Guid BookId,
    DateTime ReceiptDate,
    DateTime ReturnDate);