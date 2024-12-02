using Application.Dtos.Genre;
using Application.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetGenresByNameCase;

public record GetGenresByNameQuery(
    string Name)
    : IRequest<Result<IEnumerable<ReadGenreDto>>>;