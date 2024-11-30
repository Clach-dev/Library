using Application.Dtos.Genre;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetGenreByNameCase;

public class GetGenreByNameHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetGenreByNameQuery, Result<GenreReadDto>>
{
    public async Task<Result<GenreReadDto>> Handle(
        GetGenreByNameQuery getGenreByNameQuery,
        CancellationToken cancellationToken)
    {
        var genre = (await unitOfWork
            .Genres
            .GetByPredicateAsync(genre => genre.Name == getGenreByNameQuery.Name, cancellationToken))
            .FirstOrDefault();

        if (genre is null)
        {
            ResultBuilder.NotFoundResult<GenreReadDto>(ErrorMessages.GenreNameNotFound);
        }
        
        var genreReadDto = mapper.Map<GenreReadDto>(genre);
        
        return ResultBuilder.SuccessResult(genreReadDto);
    }
}