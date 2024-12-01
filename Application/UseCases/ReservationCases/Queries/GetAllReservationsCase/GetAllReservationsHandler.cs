using Application.Dtos.Reservation;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetAllReservationsCase;

public class GetAllReservationsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<GetAllReservationsQuery, Result<IEnumerable<ReadReservationDto>>>
{
    public async Task<Result<IEnumerable<ReadReservationDto>>> Handle(GetAllReservationsQuery getAllReservationsQuery, CancellationToken cancellationToken)
    {
        var reservations = await unitOfWork.Reservations.GetAllAsync(getAllReservationsQuery, cancellationToken);

        var reservationReadDto = mapper.Map<IEnumerable<ReadReservationDto>>(reservations);

        return ResultBuilder.SuccessResult(reservationReadDto);
    }
}