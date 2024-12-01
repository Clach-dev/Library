using Application.Dtos.User;
using Application.Interfaces.IAlgorithms;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.UserCases.Commands.RegisterUserCase;

public class RegisterUserHandler(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IMapper mapper)
    : IRequestHandler<RegisterUserCommand, Result<ReadUserDto>>
{
    public async Task<Result<ReadUserDto>> Handle(
        RegisterUserCommand registerUserCommand,
        CancellationToken cancellationToken)
    {
        var existingUser = (await unitOfWork
            .Users
            .GetByPredicateAsync(user => user.Login == registerUserCommand.Login, cancellationToken))
            .FirstOrDefault();
        
        if (existingUser is not null)
        {
            return ResultBuilder.ConflictResult<ReadUserDto>(ErrorMessages.ExistingUserLoginError);
        }

        var newUser = mapper.Map<User>(registerUserCommand);
        
        newUser.Password = passwordHasher.HashPassword(registerUserCommand.Password);
        
        await unitOfWork.Users.CreateAsync(newUser, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var userReadDto = mapper.Map<ReadUserDto>(newUser);
        return ResultBuilder.CreatedResult(userReadDto);
    }
}