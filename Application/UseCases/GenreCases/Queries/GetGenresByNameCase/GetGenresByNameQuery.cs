using Application.Common.Dtos;
using Application.Common.Dtos.Genre;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetGenresByNameCase;

public record GetGenresByNameQuery(
    string Name,
    PageInfoDto PageInfoDto)
    : IRequest<Result<IEnumerable<ReadGenreDto>>>;