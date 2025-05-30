using Application.Common.Utils;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.BookCases.Commands.DeleteBookCase;

public class DeleteBookHandler(
    IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteBookCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        DeleteBookCommand deleteBookCommand,
        CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdAsync(deleteBookCommand.Id, cancellationToken);
        if (book is null)
        {
            return ResultBuilder.NotFoundResult<Unit>(ErrorMessages.BookIdNotFound);
        }
        
        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            foreach (var imageUri in book.Images)
            {
                await unitOfWork.BookImages.DeleteFileAsync(imageUri, cancellationToken);
            }

            await unitOfWork.Books.Delete(book);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return ResultBuilder.NoContentResult<Unit>();
        }
        catch
        {
            return ResultBuilder.InternalServerErrorResult<Unit>(ErrorMessages.BookDeletionFailureError);
        }
    }
}