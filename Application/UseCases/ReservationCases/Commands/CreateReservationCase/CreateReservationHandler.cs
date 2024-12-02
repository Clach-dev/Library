using Application.Common.Dtos.Reservation;
using Application.Common.Interfaces.IRepositories;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.ReservationCases.Commands.CreateReservationCase;

public class CreateReservationHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<CreateReservationCommand, Result<ReadReservationDto>>
{
    public async Task<Result<ReadReservationDto>> Handle(
        CreateReservationCommand createReservationCommand,
        CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdAsync(createReservationCommand.BookId, cancellationToken);
        if (book is null)
        {
            ResultBuilder.NotFoundResult<ReadReservationDto>(ErrorMessages.BookIdNotFound);
        }

        var user = await unitOfWork.Users.GetByIdAsync(createReservationCommand.UserId, cancellationToken);
        if (user is null)
        {
            ResultBuilder.NotFoundResult<ReadReservationDto>(ErrorMessages.UserIdNotFound);
        }
        
        var existedReservation = (await unitOfWork
            .Reservations
            .GetByPredicateAsync(reservation =>
                    reservation.UserId == createReservationCommand.UserId &&
                    reservation.BookId == createReservationCommand.BookId &&
                    reservation.IsReturned == false,
                cancellationToken))
            .FirstOrDefault();
        if (existedReservation is not null)
        {
            return ResultBuilder.ConflictResult<ReadReservationDto>(ErrorMessages.ExistingReservationError);
        }
        
        var newReservation = mapper.Map<Reservation>(createReservationCommand);
        
        await unitOfWork.Reservations.CreateAsync(newReservation, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var reservationReadDto = mapper.Map<ReadReservationDto>(newReservation);
        return ResultBuilder.CreatedResult(reservationReadDto);
    }
}