using Application.Common.Dtos.Genre;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetAllGenresCase;

public class GetAllGenresHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetAllGenresQuery, Result<IEnumerable<ReadGenreDto>>>
{
    public async Task<Result<IEnumerable<ReadGenreDto>>> Handle(
        GetAllGenresQuery getAllGenresQuery,
        CancellationToken cancellationToken)
    {
        var genres = await unitOfWork.Genres.GetAllAsync(mapper.Map<PageInfo>(getAllGenresQuery),cancellationToken);
        
        var genresReadDto = mapper.Map<IEnumerable<ReadGenreDto>>(genres);
        
        return ResultBuilder.SuccessResult(genresReadDto);
    }
}