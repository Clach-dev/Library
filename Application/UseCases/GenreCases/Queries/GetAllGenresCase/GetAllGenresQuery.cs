using Application.Common.Dtos;
using Application.Common.Dtos.Genre;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetAllGenresCase;

public record GetAllGenresQuery
    : PageInfo, IRequest<Result<IEnumerable<ReadGenreDto>>>;