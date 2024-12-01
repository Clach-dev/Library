using Application.Dtos.Reservation;
using Application.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetReservationByIdCase;

public record GetReservationByIdQuery(
    Guid Id)
    : IRequest<Result<ReservationReadDto>>;