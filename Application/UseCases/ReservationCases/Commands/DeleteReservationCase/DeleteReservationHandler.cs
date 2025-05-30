using Application.Common.Utils;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.ReservationCases.Commands.DeleteReservationCase;

public class DeleteReservationHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteReservationCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        DeleteReservationCommand deleteReservationCommand,
        CancellationToken cancellationToken)
    {
        var reservation = await unitOfWork.Reservations.GetByIdAsync(deleteReservationCommand.Id, cancellationToken);
        if (reservation is null)
        {
            return ResultBuilder.NotFoundResult<Unit>(ErrorMessages.ReservationIdNotFound);
        }

        await unitOfWork.Reservations.Delete(reservation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultBuilder.NoContentResult<Unit>();
    }
}