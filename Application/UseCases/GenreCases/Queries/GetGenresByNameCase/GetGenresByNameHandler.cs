using Application.Common.Dtos.Genre;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetGenresByNameCase;

public class GetGenresByNameHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetGenresByNameQuery, Result<ReadGenresDto>>
{
    public async Task<Result<ReadGenresDto>> Handle(
        GetGenresByNameQuery getGenresByNameQuery,
        CancellationToken cancellationToken)
    {
        var genres = await unitOfWork.Genres.GetByPredicateAsync(
            genre => genre.Name.Contains(getGenresByNameQuery.Name),
            new PageInfo(),
            cancellationToken);
        
        var genresReadDto = new ReadGenresDto(mapper.Map<IEnumerable<ReadGenreDto>>(genres.Item1), genres.Item2);
        
        return ResultBuilder.SuccessResult(genresReadDto);
    }
}