using Application.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Commands.DeleteUserCase;

public class DeleteUserCommand : IRequest<Result<byte>>
{
    public Guid Id;
}