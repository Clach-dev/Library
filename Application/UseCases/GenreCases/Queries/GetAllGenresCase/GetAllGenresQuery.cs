using Application.Dtos;
using Application.Dtos.Genre;
using Application.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetAllGenresCase;

public record GetAllGenresQuery
    : PageInfo, IRequest<Result<IEnumerable<ReadGenreDto>>>;