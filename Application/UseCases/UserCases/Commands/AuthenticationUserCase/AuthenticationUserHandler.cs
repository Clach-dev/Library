using Application.Algorithms;
using Application.Dtos.Token;
using Application.Interfaces.IAlgorithms;
using Application.Interfaces.IRepositories;
using Application.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Commands.AuthenticationUserCase;

public class AuthenticationUserHandler(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    TokensGenerator tokensGenerator)
    : IRequestHandler<AuthenticationUserCommand, Result<TokenReadDto>>
{
    public async Task<Result<TokenReadDto>> Handle(
        AuthenticationUserCommand authenticationUserCommand,
        CancellationToken cancellationToken)
    {
        var user = (await unitOfWork
            .Users
            .GetByPredicateAsync(user => user.Login == authenticationUserCommand.Login, cancellationToken))
            .FirstOrDefault();

        if (user is null)
        {
            return ResultBuilder.NotFoundResult<TokenReadDto>(ErrorMessages.NotFoundError);
        }

        if (!passwordHasher.VerifyHashedPassword(user.Password, authenticationUserCommand.Password))
        {
            return ResultBuilder.UnauthorizedResult<TokenReadDto>(ErrorMessages.WrongPasswordError);
        }
        
        var accessToken = tokensGenerator.GenerateAccessToken(user);
        var refreshToken = tokensGenerator.GenerateRefreshToken(user);

        await unitOfWork.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var tokenReadDto = new TokenReadDto(accessToken, refreshToken.Token.ToString());
        return ResultBuilder.CreatedResult(tokenReadDto);
    }
}   