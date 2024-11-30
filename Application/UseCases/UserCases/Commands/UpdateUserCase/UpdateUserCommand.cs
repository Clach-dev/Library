using Application.Dtos.User;
using Application.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Commands.UpdateUserCase;

public class UpdateUserCommand : IRequest<UserReadDto>, IRequest<Result<UserReadDto>>
{
    public Guid Id { get; set; }
    
    public string? Login { get; init; }
    
    public string? Password { get; init; }
    
    public string? LastName { get; init; }
    
    public string? FirstName { get; init; }
    
    public string? MiddleName { get; init; }
    
    public DateTime? BirthDate { get; init; }
}