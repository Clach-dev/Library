using Application.Common.Dtos.Reservation;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetAllReservationsCase;

public class GetAllReservationsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<GetAllReservationsQuery, Result<ReadReservationsDto>>
{
    public async Task<Result<ReadReservationsDto>> Handle(
        GetAllReservationsQuery getAllReservationsQuery,
        CancellationToken cancellationToken)
    {
        var reservations = await unitOfWork.Reservations.GetAllAsync(
            mapper.Map<PageInfo>(getAllReservationsQuery.PageInfoDto),
            cancellationToken);

        var reservationReadDto = mapper.Map<ReadReservationsDto>(reservations);

        return ResultBuilder.SuccessResult(reservationReadDto);
    }
}