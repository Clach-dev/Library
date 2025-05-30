using Application.Common.Dtos.User;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.UserCases.Commands.UpdateUserCase;

public class UpdateUserHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateUserCommand, Result<ReadUserDto>>
{
    public async Task<Result<ReadUserDto>> Handle(
        UpdateUserCommand updateUserCommand,
        CancellationToken cancellationToken)
    {
        var currentUser = await unitOfWork.Users.GetByIdAsync(updateUserCommand.Id, cancellationToken);
        if (currentUser is null)
        {
            return ResultBuilder.NotFoundResult<ReadUserDto>(ErrorMessages.NotFoundError);
        }

        var existedUser = (await unitOfWork.Users.GetByPredicateAsync(
                user => user.PhoneNumber == updateUserCommand.PhoneNumber,
                new PageInfo(),
                cancellationToken))
            .Item1.FirstOrDefault();

        if (existedUser is not null && existedUser.Id != currentUser.Id)
        {
            return ResultBuilder.ConflictResult<ReadUserDto>(ErrorMessages.ExistingUserLoginError);
        }

        await using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
        
        Uri? newImageUri = null;
        
        var oldImageUri = currentUser.ProfileImage;

        try
        {
            if (updateUserCommand.ProfileImage is not null)
            {
                await using var imageStream = updateUserCommand.ProfileImage.OpenReadStream();
                newImageUri = await unitOfWork.UserImages.UploadFileAsync(imageStream, cancellationToken);

                currentUser.ProfileImage = newImageUri;
            }

            mapper.Map(updateUserCommand, currentUser);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            if (newImageUri is not null && oldImageUri is not null)
            {
                await unitOfWork.UserImages.DeleteFileAsync(oldImageUri, cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);

            var userReadDto = mapper.Map<ReadUserDto>(currentUser);
            return ResultBuilder.SuccessResult(userReadDto);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);

            if (newImageUri is not null)
            {
                await unitOfWork.UserImages.DeleteFileAsync(newImageUri, cancellationToken);
            }
            
            return ResultBuilder.InternalServerErrorResult<ReadUserDto>(ErrorMessages.UserUpdatingFailureError);
        }
    }
}
