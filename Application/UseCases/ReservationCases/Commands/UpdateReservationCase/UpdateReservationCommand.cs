using Application.Dtos.Reservation;
using Application.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Commands.UpdateReservationCase;

public record UpdateReservationCommand(
    Guid Id,
    Guid? BookId,
    Guid? UserId,
    DateTime? ReceiptDate,
    DateTime? ReturnDate,
    bool? IsReturned)
    : IRequest<Result<ReadReservationDto>>;