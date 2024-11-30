using Application.Utils;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.DeleteAuthorCase;

public record DeleteAuthorCommand(
    Guid Id) 
    : IRequest<Result<byte>>;