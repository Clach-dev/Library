using Application.Dtos.Reservation;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetAllReservationsCase;

public class GetAllReservationsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<GetAllReservationsQuery, Result<IEnumerable<ReservationReadDto>>>
{
    public async Task<Result<IEnumerable<ReservationReadDto>>> Handle(GetAllReservationsQuery getAllReservationsQuery, CancellationToken cancellationToken)
    {
        var reservations = await unitOfWork.Reservations.GetAllAsync(getAllReservationsQuery, cancellationToken);

        var reservationReadDto = mapper.Map<IEnumerable<ReservationReadDto>>(reservations);

        return ResultBuilder.SuccessResult(reservationReadDto);
    }
}