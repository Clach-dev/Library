﻿using Application.Dtos.Genre;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.UpdateGenreCase;

public class UpdateGenreHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateGenreCommand, Result<ReadGenreDto>>
{
    public async Task<Result<ReadGenreDto>> Handle(
        UpdateGenreCommand updateGenreCommand,
        CancellationToken cancellationToken)
    {
        var currentGenre = await unitOfWork.Genres.GetByIdAsync(updateGenreCommand.Id, cancellationToken);
        if (currentGenre is null)
        {
            return ResultBuilder.NotFoundResult<ReadGenreDto>(ErrorMessages.NotFoundError);
        }
        
        var genre = (await unitOfWork
            .Genres
            .GetByPredicateAsync(genre => genre.Name == updateGenreCommand.Name, cancellationToken))
            .FirstOrDefault();
        if (genre is not null)
        {
            return ResultBuilder.ConflictResult<ReadGenreDto>(ErrorMessages.ExistingGenreError);
        }

        mapper.Map(updateGenreCommand, currentGenre);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var genreReadDto = mapper.Map<ReadGenreDto>(currentGenre);
        return ResultBuilder.SuccessResult(genreReadDto);
    }
}