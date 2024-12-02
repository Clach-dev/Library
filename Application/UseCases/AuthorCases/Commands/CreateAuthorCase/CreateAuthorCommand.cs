using Application.Common.Dtos.Author;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.CreateAuthorCase;

public record CreateAuthorCommand(
    string LastName,
    string FirstName,
    string? MiddleName,
    string? Description)
    : IRequest<Result<ReadAuthorDto>>;