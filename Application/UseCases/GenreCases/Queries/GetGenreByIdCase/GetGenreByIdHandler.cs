using Application.Common.Dtos.Genre;
using Application.Common.Interfaces.IRepositories;
using Application.Common.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetGenreByIdCase;

public class GetGenreByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetGenreByIdQuery, Result<ReadGenreDto>>
{
    public async Task<Result<ReadGenreDto>> Handle(
        GetGenreByIdQuery getGenreByIdQuery,
        CancellationToken cancellationToken)
    {
        var genre = await unitOfWork.Genres.GetByIdAsync(getGenreByIdQuery.Id, cancellationToken);
        if (genre is null)
        {
            ResultBuilder.NotFoundResult<ReadGenreDto>(ErrorMessages.GenreIdNotFound);
        }
        
        var genreReadDto = mapper.Map<ReadGenreDto>(genre);
        
        return ResultBuilder.SuccessResult(genreReadDto);
    }
}