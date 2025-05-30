using Application.Common.Dtos.User;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IAlgorithms;
using Domain.Interfaces.IRepositories;
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
        var existingUser = (await unitOfWork.Users.GetByPredicateAsync(
            user => user.PhoneNumber == registerUserCommand.PhoneNumber,
            new PageInfo(),
            cancellationToken))
            .Item1.FirstOrDefault();

        if (existingUser is not null)
        {
            return ResultBuilder.ConflictResult<ReadUserDto>(ErrorMessages.ExistingUserLoginError);
        }

        Uri? uploadedImageUri = null;

        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            if (registerUserCommand.ProfileImage is not null)
            {
                await using var imageStream = registerUserCommand.ProfileImage.OpenReadStream();
                uploadedImageUri = await unitOfWork.UserImages.UploadFileAsync(imageStream, cancellationToken);
            }

            var newUser = mapper.Map<User>(registerUserCommand);

            if (uploadedImageUri is not null)
                newUser.ProfileImage = uploadedImageUri;

            newUser.Password = passwordHasher.HashPassword(registerUserCommand.Password);

            await unitOfWork.Users.CreateAsync(newUser, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var userReadDto = mapper.Map<ReadUserDto>(newUser);
            return ResultBuilder.CreatedResult(userReadDto);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);

            if (uploadedImageUri is not null)
            {
                await unitOfWork.UserImages.DeleteFileAsync(uploadedImageUri, cancellationToken);
            }

            return ResultBuilder.InternalServerErrorResult<ReadUserDto>(ErrorMessages.UserRegistrationFailureError);
        }
    }
}
