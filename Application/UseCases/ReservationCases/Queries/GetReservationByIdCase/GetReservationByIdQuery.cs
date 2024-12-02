using Application.Common.Dtos.Reservation;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetReservationByIdCase;

public record GetReservationByIdQuery(
    Guid Id)
    : IRequest<Result<ReadReservationDto>>;