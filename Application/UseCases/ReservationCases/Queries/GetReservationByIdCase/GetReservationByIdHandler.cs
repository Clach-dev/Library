using Application.Dtos.Reservation;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetReservationByIdCase;

public class GetReservationByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetReservationByIdQuery, Result<ReservationReadDto>>
{
    public async Task<Result<ReservationReadDto>> Handle(GetReservationByIdQuery getReservationByIdQuery, CancellationToken cancellationToken)
    {
        var reservation = await unitOfWork.Reservations.GetByIdAsync(getReservationByIdQuery.Id, cancellationToken);

        if (reservation is null)
        {
            return ResultBuilder.NotFoundResult<ReservationReadDto>(ErrorMessages.ReservationIdNotFound);
        }

        var reservationReadDto = mapper.Map<ReservationReadDto>(reservation);

        return ResultBuilder.SuccessResult(reservationReadDto);
    }
}