using Application.Dtos.Genre;
using Application.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetGenreByNameCase;

public record GetGenreByNameQuery(
    string Name)
    : IRequest<Result<GenreReadDto>>;