using Application.Dtos.Author;
using Application.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.UpdateAuthorCase;

public record UpdateAuthorCommand(
    Guid Id,
    string? LastName,
    string? FirstName,
    string? MiddleName,
    string? Description)
    : IRequest<Result<ReadAuthorDto>>;