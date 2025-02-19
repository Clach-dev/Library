using Application.Common.Dtos;
using Application.Common.Dtos.Genre;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetAllGenresCase;

public record GetAllGenresQuery(
    PageInfoDto PageInfoDto)
    : PageInfoDto, IRequest<Result<IEnumerable<ReadGenreDto>>>;