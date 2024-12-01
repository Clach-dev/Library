using Application.Dtos.Genre;
using Application.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.CreateGenreCase;

public record CreateGenreCommand(
    string Name,
    string? Description)
    : IRequest<Result<ReadGenreDto>>;