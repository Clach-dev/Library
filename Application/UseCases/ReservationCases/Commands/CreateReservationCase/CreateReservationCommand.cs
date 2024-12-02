using Application.Common.Dtos.Reservation;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Commands.CreateReservationCase;

public record CreateReservationCommand(
    Guid BookId,
    Guid UserId)
    : IRequest<Result<ReadReservationDto>>;