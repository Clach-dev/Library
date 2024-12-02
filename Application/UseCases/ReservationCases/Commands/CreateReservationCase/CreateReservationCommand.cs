using Application.Dtos.Reservation;
using Application.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Commands.CreateReservationCase;

public record CreateReservationCommand(
    Guid BookId,
    Guid UserId)
    : IRequest<Result<ReadReservationDto>>;