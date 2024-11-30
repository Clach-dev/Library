using Application.Dtos.Genre;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.CreateGenreCase;

public class CreateGenreHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<CreateGenreCommand, Result<GenreReadDto>>
{
    public async Task<Result<GenreReadDto>> Handle(
        CreateGenreCommand createGenreCommand,
        CancellationToken cancellationToken)
    {
        var existingGenre = (await unitOfWork
            .Genres
            .GetByPredicateAsync(genre => genre.Name == createGenreCommand.Name, cancellationToken))
            .FirstOrDefault();

        if (existingGenre is not null)
        {
            return ResultBuilder.NotFoundResult<GenreReadDto>(ErrorMessages.ExistingGenreError);
        }
        
        var newGenre = mapper.Map<Genre>(createGenreCommand);
        
        await unitOfWork.Genres.AddAsync(newGenre, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var genreReadDto = mapper.Map<GenreReadDto>(newGenre);
        return ResultBuilder.CreatedResult(genreReadDto);
    }
}