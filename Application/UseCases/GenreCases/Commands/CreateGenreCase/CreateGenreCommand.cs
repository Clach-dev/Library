using Application.Common.Dtos.Genre;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.CreateGenreCase;

public record CreateGenreCommand(
    string Name,
    string? Description)
    : IRequest<Result<ReadGenreDto>>;