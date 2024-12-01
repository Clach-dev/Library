using Application.Dtos.Author;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.AuthorCases.Commands.CreateAuthorCase;

public class CreateAuthorHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : 
    IRequestHandler<CreateAuthorCommand, Result<ReadAuthorDto>>
{
    public async Task<Result<ReadAuthorDto>> Handle(CreateAuthorCommand createAuthorCommand, CancellationToken cancellationToken)
    {
        var author = mapper.Map<Author>(createAuthorCommand);
        
        await unitOfWork.Authors.CreateAsync(author, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var authorReadDto = mapper.Map<ReadAuthorDto>(author);
        
        return ResultBuilder.CreatedResult(authorReadDto);
    }
}