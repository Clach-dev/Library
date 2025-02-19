using Application.Common.Dtos.Genre;
using Application.Common.Utils;
using AutoMapper;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetGenresByNameCase;

public class GetGenresByNameHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetGenresByNameQuery, Result<IEnumerable<ReadGenreDto>>>
{
    public async Task<Result<IEnumerable<ReadGenreDto>>> Handle(
        GetGenresByNameQuery getGenresByNameQuery,
        CancellationToken cancellationToken)
    {
        var genres = (await unitOfWork
            .Genres
            .GetByPredicateAsync(genre => genre.Name.Contains(getGenresByNameQuery.Name), cancellationToken));
        
        var genresReadDto = mapper.Map<IEnumerable<ReadGenreDto>>(genres);
        
        return ResultBuilder.SuccessResult(genresReadDto);
    }
}