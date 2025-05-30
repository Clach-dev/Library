using Application.Common.Utils;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.DeleteGenreCase;

public class DeleteGenreHandler(
    IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteGenreCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(
        DeleteGenreCommand deleteGenreCommand,
        CancellationToken cancellationToken)
    {
        var genre = await unitOfWork.Genres.GetByIdAsync(deleteGenreCommand.Id, cancellationToken);
        if (genre is null)
        {
            return ResultBuilder.NotFoundResult<Unit>(ErrorMessages.NotFoundError);
        }
        
        await unitOfWork.Genres.Delete(genre);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return ResultBuilder.NoContentResult<Unit>();
    }
}