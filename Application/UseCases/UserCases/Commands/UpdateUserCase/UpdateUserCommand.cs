using Application.Dtos.User;
using Application.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Commands.UpdateUserCase;

public class UpdateUserCommand : IRequest<UserReadDto>, IRequest<Result<UserReadDto>>
{
    public Guid Id { get; set; }
    
    public string? Login { get; set; }
    
    public string? Password { get; set; }
    
    public string? LastName { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public DateTime? BirthDate { get; set; }
}