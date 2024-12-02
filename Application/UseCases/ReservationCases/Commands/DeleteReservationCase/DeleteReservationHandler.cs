using Application.Interfaces.IRepositories;
using Application.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Commands.DeleteReservationCase;

public class DeleteReservationHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteReservationCommand, Result<byte>>
{
    public async Task<Result<byte>> Handle(
        DeleteReservationCommand deleteReservationCommand,
        CancellationToken cancellationToken)
    {
        var reservation = await unitOfWork.Reservations.GetByIdAsync(deleteReservationCommand.Id, cancellationToken);
        if (reservation is null)
        {
            return ResultBuilder.NotFoundResult<byte>(ErrorMessages.ReservationIdNotFound);
        }

        await unitOfWork.Reservations.Delete(reservation);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResultBuilder.NoContentResult<byte>();
    }
}