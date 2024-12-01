using System.Reflection;
using System.Xml;
using Application.Dtos.Reservation;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.ReservationCases.Commands.UpdateReservationCase;

public class UpdateReservationHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateReservationCommand, Result<ReadReservationDto>>
{
    public async Task<Result<ReadReservationDto>> Handle(
        UpdateReservationCommand updateReservationCommand,
        CancellationToken cancellationToken)
    {
        var currentReservation = await unitOfWork.Reservations.GetByIdAsync(updateReservationCommand.Id, cancellationToken);

        if (currentReservation is null)
        {
            return ResultBuilder.NotFoundResult<ReadReservationDto>(ErrorMessages.ExistingReservationError);
        }

        mapper.Map(updateReservationCommand, currentReservation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var reservationReadDto = mapper.Map<ReadReservationDto>(currentReservation);
        return ResultBuilder.SuccessResult(reservationReadDto);
    }
}