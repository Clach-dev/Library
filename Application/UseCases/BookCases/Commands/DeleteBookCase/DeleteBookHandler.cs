using Application.Common.Utils;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.BookCases.Commands.DeleteBookCase;

public class DeleteBookHandler(
    IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteBookCommand, Result<byte?>>
{
    public async Task<Result<byte?>> Handle(
        DeleteBookCommand deleteBookCommand,
        CancellationToken cancellationToken)
    {
        var book = await unitOfWork.Books.GetByIdAsync(deleteBookCommand.Id, cancellationToken);
        if (book is null)
        {
            return ResultBuilder.NotFoundResult<byte?>(ErrorMessages.BookIdNotFound);
        }
        
        await unitOfWork.Books.Delete(book);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return ResultBuilder.NoContentResult<byte?>();
    }
}