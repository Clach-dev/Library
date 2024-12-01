using Application.Dtos.Author;
using Application.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.CreateAuthorCase;

public record CreateAuthorCommand(
    string LastName,
    string FirstName,
    string MiddleName,
    string Description)
    : IRequest<Result<ReadAuthorDto>>;