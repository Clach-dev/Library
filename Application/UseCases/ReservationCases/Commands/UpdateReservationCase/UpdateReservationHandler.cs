using Application.Common.Dtos.Reservation;
using Application.Common.Utils;
using AutoMapper;
using Domain.Interfaces.IRepositories;
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
        var currentReservation = await unitOfWork
            .Reservations
            .GetByIdAsync(updateReservationCommand.Id, cancellationToken);

        if (currentReservation is null)
        {
            return ResultBuilder.NotFoundResult<ReadReservationDto>(ErrorMessages.ExistingReservationError);
        }

        if (updateReservationCommand.BookId is not null)
        {
            var book = await unitOfWork.Books.GetByIdAsync((Guid)updateReservationCommand.BookId, cancellationToken);
            if (book is null)
            {
                ResultBuilder.NotFoundResult<ReadReservationDto>(ErrorMessages.BookIdNotFound);
            }
        }

        if (updateReservationCommand.UserId is not null)
        {
            var user = await unitOfWork.Users.GetByIdAsync((Guid)updateReservationCommand.UserId, cancellationToken);
            if (user is null)
            {
                ResultBuilder.NotFoundResult<ReadReservationDto>(ErrorMessages.UserIdNotFound);
            }
        }
        
        mapper.Map(updateReservationCommand, currentReservation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var reservationReadDto = mapper.Map<ReadReservationDto>(currentReservation);
        return ResultBuilder.SuccessResult(reservationReadDto);
    }
}