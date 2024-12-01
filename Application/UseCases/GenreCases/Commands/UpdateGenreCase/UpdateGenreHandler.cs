using Application.Dtos.Genre;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.UpdateGenreCase;

public class UpdateGenreHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateGenreCommand, Result<GenreReadDto>>
{
    public async Task<Result<GenreReadDto>> Handle(
        UpdateGenreCommand updateGenreCommand,
        CancellationToken cancellationToken)
    {
        var currentGenre = await unitOfWork.Genres.GetByIdAsync(updateGenreCommand.Id, cancellationToken);

        if (currentGenre is null)
        {
            return ResultBuilder.NotFoundResult<GenreReadDto>(ErrorMessages.NotFoundError);
        }
        
        var genre = (await unitOfWork
            .Genres
            .GetByPredicateAsync(genre => genre.Name == updateGenreCommand.Name, cancellationToken))
            .FirstOrDefault();
        
        if (genre is not null)
        {
            return ResultBuilder.ConflictResult<GenreReadDto>(ErrorMessages.ExistingGenreError);
        }

        mapper.Map(updateGenreCommand, currentGenre);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var genreReadDto = mapper.Map<GenreReadDto>(currentGenre);
        return ResultBuilder.SuccessResult(genreReadDto);
    }
}