using Application.Dtos.Genre;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetGenreByIdCase;

public class GetGenreByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetGenreByIdQuery, Result<GenreReadDto>>
{
    public async Task<Result<GenreReadDto>> Handle(
        GetGenreByIdQuery getGenreByIdQuery,
        CancellationToken cancellationToken)
    {
        var genre = await unitOfWork.Genres.GetByIdAsync(getGenreByIdQuery.Id, cancellationToken);

        if (genre is null)
        {
            ResultBuilder.NotFoundResult<GenreReadDto>(ErrorMessages.GenreIdNotFound);
        }
        
        var genreReadDto = mapper.Map<GenreReadDto>(genre);
        
        return ResultBuilder.SuccessResult(genreReadDto);
    }
}