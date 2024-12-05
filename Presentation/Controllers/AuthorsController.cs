using Application.Common.Dtos;
using Application.Common.Dtos.Author;
using Application.UseCases.AuthorCases.Commands.CreateAuthorCase;
using Application.UseCases.AuthorCases.Commands.DeleteAuthorCase;
using Application.UseCases.AuthorCases.Commands.UpdateAuthorCase;
using Application.UseCases.AuthorCases.Queries.GetAllAuthorsCase;
using Application.UseCases.AuthorCases.Queries.GetAuthorByIdCase;
using Application.UseCases.AuthorCases.Queries.GetAuthorsByNameCase;
using AutoMapper;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthorsController(
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    IMediator mediator
) : CustomControllerBase(httpContextAccessor)
{
    /// <summary>
    /// Get all authors operation
    /// </summary>
    /// <param name="pageInfo">PageInfo which contains number of current page and number of items per page</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with authors information</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAuthors(
        [FromQuery] PageInfo pageInfo,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllAuthorsQuery(pageInfo), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Get author by id operation
    /// </summary>
    /// <param name="authorId">Guid identifier of author</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with author information</returns>
    [HttpGet("{authorId}")]
    public async Task<IActionResult> GetAuthorById(
        Guid authorId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAuthorByIdQuery(authorId), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Get authors by name operation
    /// </summary>
    /// <param name="getAuthorsByNameQuery">GetAuthorsByNameQuery which contains author full name</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with filtered authors information</returns>
    [HttpGet]
    public async Task<IActionResult> GetAuthorsByName(
        [FromBody] GetAuthorsByNameQuery getAuthorsByNameQuery,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(getAuthorsByNameQuery, cancellationToken);
        return Result(result);
    }
    /// <summary>
    /// Author creation operation
    /// </summary>
    /// <param name="createAuthorDto">CreateAuthorDto which contains author information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with created author information</returns>
    [HttpPost]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> CreateAuthor(
        [FromBody] CreateAuthorDto createAuthorDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<CreateAuthorCommand>(createAuthorDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Author update operation
    /// </summary>
    /// <param name="updateAuthorDto">UpdateAuthorDto which contains author update information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with updated author information</returns>
    [HttpPut]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> UpdateAuthor(
        [FromBody] UpdateAuthorDto updateAuthorDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<UpdateAuthorCommand>(updateAuthorDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Author delete operation
    /// </summary>
    /// <param name="deleteAuthorDto">DeleteAuthorDto which contains id of author to delete</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with status code of delete operation</returns>
    [HttpDelete]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> DeleteAuthor(
        [FromBody] DeleteAuthorDto deleteAuthorDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<DeleteAuthorCommand>(deleteAuthorDto), cancellationToken);
        return Result(result);
    }
}