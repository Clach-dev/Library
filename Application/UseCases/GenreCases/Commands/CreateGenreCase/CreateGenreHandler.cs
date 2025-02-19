using Application.Common.Dtos.Genre;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.CreateGenreCase;

public class CreateGenreHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : IRequestHandler<CreateGenreCommand, Result<ReadGenreDto>>
{
    public async Task<Result<ReadGenreDto>> Handle(
        CreateGenreCommand createGenreCommand,
        CancellationToken cancellationToken)
    {
        var existingGenre = (await unitOfWork
            .Genres
            .GetByPredicateAsync(genre => genre.Name == createGenreCommand.Name, cancellationToken))
            .FirstOrDefault();
        if (existingGenre is not null)
        {
            return ResultBuilder.ConflictResult<ReadGenreDto>(ErrorMessages.ExistingGenreError);
        }
        
        var newGenre = mapper.Map<Genre>(createGenreCommand);
        
        await unitOfWork.Genres.CreateAsync(newGenre, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var genreReadDto = mapper.Map<ReadGenreDto>(newGenre);
        return ResultBuilder.CreatedResult(genreReadDto);
    }
}