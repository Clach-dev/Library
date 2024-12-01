using Application.Dtos.User;
using Application.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Commands.RegisterUserCase;

public record RegisterUserCommand(
    string Login,
    string Password,
    string LastName,
    string FirstName,
    string? MiddleName,
    DateTime BirthDate)
    : IRequest<Result<ReadUserDto>>;