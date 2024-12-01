using Application.Dtos.Genre;
using Application.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.UpdateGenreCase;

public record UpdateGenreCommand(
    Guid Id,
    string? Name,
    string? Description)
    : IRequest<Result<ReadGenreDto>>;