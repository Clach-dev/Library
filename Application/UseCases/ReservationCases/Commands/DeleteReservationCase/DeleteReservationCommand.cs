using Application.Dtos.Reservation;
using Application.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Commands.DeleteReservationCase;

public record DeleteReservationCommand(
    Guid Id)
    : IRequest<Result<byte>>;