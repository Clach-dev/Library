using Application.Common.Dtos.User;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Commands.UpdateUserCase;

public class UpdateUserCommand : IRequest<ReadUserDto>, IRequest<Result<ReadUserDto>>
{
    public Guid Id { get; set; }
    
    public string? Login { get; init; }
    
    public string? Password { get; init; }
    
    public string? LastName { get; init; }
    
    public string? FirstName { get; init; }
    
    public string? MiddleName { get; init; }
    
    public DateTime? BirthDate { get; init; }
}