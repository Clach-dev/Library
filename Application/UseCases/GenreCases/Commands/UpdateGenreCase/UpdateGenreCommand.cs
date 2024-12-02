using Application.Common.Dtos.Genre;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.UpdateGenreCase;

public record UpdateGenreCommand(
    Guid Id,
    string? Name,
    string? Description)
    : IRequest<Result<ReadGenreDto>>;