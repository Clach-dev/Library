using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Commands.DeleteGenreCase;

public record DeleteGenreCommand(
    Guid Id)
    : IRequest<Result<byte>>;