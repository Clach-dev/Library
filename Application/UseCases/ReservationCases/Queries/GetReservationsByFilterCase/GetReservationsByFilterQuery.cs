using Application.Common.Dtos;
using Application.Common.Dtos.Reservation;
using Application.Common.Utils;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetReservationsByFilterCase;

public record GetReservationsByFilterQuery(
    Guid? UserId,
    Guid? BookId,
    DateTime? FromRecieptDate,
    DateTime? ToRecieptDate,
    DateTime? FromReturnDate,
    DateTime? ToReturnDate,
    ReservationStatuses? Status,
    PageInfoDto PageInfoDto)
    : IRequest<Result<ReadReservationsDto>>;