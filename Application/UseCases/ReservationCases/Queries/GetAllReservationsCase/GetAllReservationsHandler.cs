using Application.Common.Dtos.Reservation;
using Application.Common.Interfaces.IRepositories;
using Application.Common.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetAllReservationsCase;

public class GetAllReservationsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<GetAllReservationsQuery, Result<IEnumerable<ReadReservationDto>>>
{
    public async Task<Result<IEnumerable<ReadReservationDto>>> Handle(
        GetAllReservationsQuery getAllReservationsQuery,
        CancellationToken cancellationToken)
    {
        var reservations = await unitOfWork.Reservations.GetAllAsync(getAllReservationsQuery.PageInfo, cancellationToken);

        var reservationReadDto = mapper.Map<IEnumerable<ReadReservationDto>>(reservations);

        return ResultBuilder.SuccessResult(reservationReadDto);
    }
}