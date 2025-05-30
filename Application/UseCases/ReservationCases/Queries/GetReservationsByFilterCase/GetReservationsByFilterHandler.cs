using Application.Common.Dtos.Reservation;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetReservationsByFilterCase;

public class GetReservationsByFilterHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetReservationsByFilterQuery, Result<ReadReservationsDto>>
{
    public async Task<Result<ReadReservationsDto>> Handle(
        GetReservationsByFilterQuery getReservationsByFilterQuery,
        CancellationToken cancellationToken)
    {
        var reservations = await unitOfWork.Reservations.GetByPredicateAsync(reservation =>
                (getReservationsByFilterQuery.UserId == null ||
                 reservation.UserId == getReservationsByFilterQuery.UserId) && 
                (getReservationsByFilterQuery.BookId == null ||
                 reservation.BookId == getReservationsByFilterQuery.BookId) && 
                (getReservationsByFilterQuery.FromRecieptDate == null ||
                 reservation.ReceiptDate >= getReservationsByFilterQuery.FromRecieptDate) &&
                (getReservationsByFilterQuery.ToRecieptDate == null ||
                 reservation.ReceiptDate <= getReservationsByFilterQuery.ToRecieptDate) &&
                (getReservationsByFilterQuery.FromReturnDate == null ||
                 reservation.ReturnDate >= getReservationsByFilterQuery.FromReturnDate) &&
                (getReservationsByFilterQuery.ToReturnDate == null ||
                 reservation.ReturnDate <= getReservationsByFilterQuery.ToReturnDate) &&
                (getReservationsByFilterQuery.Status == null ||
                 reservation.Status == getReservationsByFilterQuery.Status),
            mapper.Map<PageInfo>(getReservationsByFilterQuery.PageInfoDto),
            cancellationToken);
        
        var reservationsReadDto = mapper.Map<ReadReservationsDto>(reservations);

        return ResultBuilder.SuccessResult(reservationsReadDto);
    }

}