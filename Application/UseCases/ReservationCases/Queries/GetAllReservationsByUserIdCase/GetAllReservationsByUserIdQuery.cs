using Application.Dtos.Reservation;
using Application.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetAllReservationsByUserIdCase;

public record GetAllReservationsByUserIdQuery(
    Guid UserId) : IRequest<Result<IEnumerable<ReservationReadDto>>>;