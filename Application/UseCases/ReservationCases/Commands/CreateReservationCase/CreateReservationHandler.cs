using Application.Dtos.Reservation;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.ReservationCases.Commands.CreateReservationCase;

public class CreateReservationHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<CreateReservationCommand, Result<ReservationReadDto>>
{
    public async Task<Result<ReservationReadDto>> Handle(
        CreateReservationCommand createReservationCommand,
        CancellationToken cancellationToken)
    {
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
            return ResultBuilder.ConflictResult<ReservationReadDto>(ErrorMessages.ExistingReservationError);
        }
        
        var newReservation = mapper.Map<Reservation>(createReservationCommand);
        
        await unitOfWork.Reservations.CreateAsync(newReservation, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var reservationReadDto = mapper.Map<ReservationReadDto>(newReservation);
        return ResultBuilder.CreatedResult(reservationReadDto);
    }
}