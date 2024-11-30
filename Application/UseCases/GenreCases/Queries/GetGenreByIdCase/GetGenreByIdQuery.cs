using Application.Dtos.Genre;
using Application.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetGenreByIdCase;

public record GetGenreByIdQuery(
    Guid Id)
    : IRequest<Result<GenreReadDto>>;