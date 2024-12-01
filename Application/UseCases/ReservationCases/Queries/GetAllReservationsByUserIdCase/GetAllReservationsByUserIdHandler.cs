using Application.Dtos.Reservation;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetAllReservationsByUserIdCase;

public class GetAllReservationsByUserIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetAllReservationsByUserIdQuery, Result<IEnumerable<ReadReservationDto>>>
{
    public async Task<Result<IEnumerable<ReadReservationDto>>> Handle(
        GetAllReservationsByUserIdQuery getAllReservationsByUserIdQuery,
        CancellationToken cancellationToken)
    {
        var reservations = await unitOfWork.Reservations.GetByPredicateAsync(reservation =>
            reservation.UserId == getAllReservationsByUserIdQuery.UserId, cancellationToken);

        var reservationsReadDto = mapper.Map<IEnumerable<ReadReservationDto>>(reservations);

        return ResultBuilder.SuccessResult(reservationsReadDto);
    }
}