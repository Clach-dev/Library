using Application.Dtos;
using Application.Dtos.Reservation;
using Application.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetAllReservationsCase;

public record GetAllReservationsQuery()
    : PageInfo, IRequest<Result<IEnumerable<ReservationReadDto>>>;