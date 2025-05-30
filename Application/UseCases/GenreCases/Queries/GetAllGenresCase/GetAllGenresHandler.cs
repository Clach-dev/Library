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
    : IRequestHandler<GetAllGenresQuery, Result<ReadGenresDto>>
{
    public async Task<Result<ReadGenresDto>> Handle(
        GetAllGenresQuery getAllGenresQuery,
        CancellationToken cancellationToken)
    {
        var genres = await unitOfWork.Genres.GetAllAsync(mapper.Map<PageInfo>(getAllGenresQuery.PageInfoDto),cancellationToken);
        
        var genresReadDto = mapper.Map<ReadGenresDto>(genres);
        
        return ResultBuilder.SuccessResult(genresReadDto);
    }
}