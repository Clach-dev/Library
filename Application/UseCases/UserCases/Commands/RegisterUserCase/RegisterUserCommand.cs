using Application.Dtos.User;
using Application.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Commands.RegisterUserCase;

public class RegisterUserCommand : IRequest<Result<UserReadDto>>
{
    public string Login { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    
    public string? MiddleName { get; set; }
    
    public DateTime BirthDate { get; set; }
}