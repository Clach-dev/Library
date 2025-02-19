using Application.Common.Dtos.Reservation;
using Application.Common.Utils;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetReservationByIdCase;

public class GetReservationByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetReservationByIdQuery, Result<ReadReservationDto>>
{
    public async Task<Result<ReadReservationDto>> Handle(
        GetReservationByIdQuery getReservationByIdQuery,
        CancellationToken cancellationToken)
    {
        var reservation = await unitOfWork.Reservations.GetByIdAsync(getReservationByIdQuery.Id, cancellationToken);
        if (reservation is null)
        {
            return ResultBuilder.NotFoundResult<ReadReservationDto>(ErrorMessages.ReservationIdNotFound);
        }

        var reservationReadDto = mapper.Map<ReadReservationDto>(reservation);

        return ResultBuilder.SuccessResult(reservationReadDto);
    }
}