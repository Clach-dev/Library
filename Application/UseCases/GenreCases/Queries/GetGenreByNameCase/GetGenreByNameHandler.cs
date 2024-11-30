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
        var genres = (await unitOfWork
            .Genres
            .GetByPredicateAsync(genre => genre.Name.Contains(getGenreByNameQuery.Name), cancellationToken));
        
        var genresReadDto = mapper.Map<GenreReadDto>(genres);
        
        return ResultBuilder.SuccessResult(genresReadDto);
    }
}