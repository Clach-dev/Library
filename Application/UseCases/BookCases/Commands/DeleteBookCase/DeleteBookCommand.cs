using Application.Utils;
using MediatR;

namespace Application.UseCases.BookCases.Commands.DeleteBookCase;

public record DeleteBookCommand(
    Guid Id)
    : IRequest<Result<byte>>;