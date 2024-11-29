using Application.Dtos.Token;
using Application.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Commands.AuthenticationUserCase;

public class AuthenticationUserCommand : IRequest<Result<TokenReadDto>>
{
    public string Login { get; set; } = string.Empty;
    
    public string Password { get; set; } = string.Empty;
}