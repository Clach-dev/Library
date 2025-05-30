using Application.Common.Dtos.Author;
using Application.Common.Utils;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepositories;
using MediatR;

namespace Application.UseCases.AuthorCases.Queries.GetAllAuthorsCase;

public class GetAllAuthorsHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllAuthorsQuery ,Result<ReadAuthorsDto>>
{
    public async Task<Result<ReadAuthorsDto>> Handle(
        GetAllAuthorsQuery getAllAuthorsQuery,
        CancellationToken cancellationToken)
    {
        var authors = await unitOfWork.Authors.GetAllAsync(mapper.Map<PageInfo>(getAllAuthorsQuery.PageInfoDto), cancellationToken);
  
        var authorsReadDtos = mapper.Map<ReadAuthorsDto>(authors);
        
        return ResultBuilder.SuccessResult(authorsReadDtos);
    }
}