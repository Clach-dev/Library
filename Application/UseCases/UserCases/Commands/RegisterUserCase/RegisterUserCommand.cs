using Application.Common.Dtos.User;
using Application.Common.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.UserCases.Commands.RegisterUserCase;

public record RegisterUserCommand(
    string PhoneNumber,
    string Password,
    string LastName,
    string FirstName,
    string? MiddleName,
    DateTime BirthDate,
    IFormFile? ProfileImage)
    : IRequest<Result<ReadUserDto>>;