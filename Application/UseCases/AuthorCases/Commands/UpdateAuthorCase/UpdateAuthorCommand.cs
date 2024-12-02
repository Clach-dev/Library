using Application.Common.Dtos.Author;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.UpdateAuthorCase;

public record UpdateAuthorCommand(
    Guid Id,
    string? LastName,
    string? FirstName,
    string? MiddleName,
    string? Description)
    : IRequest<Result<ReadAuthorDto>>;