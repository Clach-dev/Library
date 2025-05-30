using Application.Common.Utils;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.UserCases.Commands.DeleteUserCase;

public class DeleteUserHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        DeleteUserCommand deleteUserCommand,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(deleteUserCommand.Id, cancellationToken);
        if (user is null)
        {
            return ResultBuilder.NotFoundResult<Unit>(ErrorMessages.NotFoundError);
        }

        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await unitOfWork.Users.Delete(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            if (user.ProfileImage is not null)
            {
                await unitOfWork.UserImages.DeleteFileAsync(user.ProfileImage, cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);
            return ResultBuilder.NoContentResult<Unit>();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            return ResultBuilder.InternalServerErrorResult<Unit>(ErrorMessages.UserDeletionFailureError);
        }
    }
}