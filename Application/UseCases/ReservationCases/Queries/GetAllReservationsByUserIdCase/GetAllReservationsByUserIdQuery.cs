using Application.Common.Dtos;
using Application.Common.Dtos.Reservation;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetAllReservationsByUserIdCase;

public record GetAllReservationsByUserIdQuery(
    Guid UserId,
    PageInfoDto PageInfoDto) : IRequest<Result<IEnumerable<ReadReservationDto>>>;