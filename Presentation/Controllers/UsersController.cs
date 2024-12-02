using Application.Common.Dtos;
using Application.Common.Dtos.User;
using Application.UseCases.UserCases.Commands.AuthenticationUserCase;
using Application.UseCases.UserCases.Commands.DeleteUserCase;
using Application.UseCases.UserCases.Commands.RegisterUserCase;
using Application.UseCases.UserCases.Commands.UpdateUserCase;
using Application.UseCases.UserCases.Commands.UpdateUserRoleCase;
using Application.UseCases.UserCases.Queries.GetAllUsersCase;
using Application.UseCases.UserCases.Queries.GetUserByIdCase;
using AutoMapper;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController(
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    IMediator mediator)
    : CustomControllerBase(httpContextAccessor)
{
    /// <summary>
    /// Registration of new user
    /// </summary>
    /// <param name="registerUserDto">RegisterUserDto which contains new user information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user registration</returns>
    [HttpPost("registration")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<RegisterUserCommand>(registerUserDto), cancellationToken);
        
        return Ok(result);
    }

    /// <summary>
    /// Authentication of user
    /// </summary>
    /// <param name="authUserDto">AuthUserDto which contains user information for authentication</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user authentication</returns>
    [HttpPost("authentication")]
    [AllowAnonymous]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthUserDto authUserDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<AuthenticationUserCommand>(authUserDto), cancellationToken);
        
        return Result(result);
    }

    /// <summary>
    /// User update operation
    /// </summary>
    /// <param name="updateUserDto">UpdateUserDto which contains new information of existed user</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user update operation</returns>
    [HttpPut]//
    [Authorize(Policy = Policies.OnlyUserAccess)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var updateUserCommand = mapper.Map<UpdateUserCommand>(updateUserDto);
        updateUserCommand.Id = GetUserId();
        
        var result = await mediator.Send(updateUserCommand, cancellationToken);
        
        return Result(result);
    }

    /// <summary>
    /// Delete operation of user
    /// </summary>
    /// <param name="deleteUserDto">DeleteUserDto which contains id of user you want to delete</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user delete operation</returns>
    [HttpDelete]
    [Authorize(Policy = Policies.OnlyUserAccess)]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDto deleteUserDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<DeleteUserCommand>(deleteUserDto), cancellationToken);
        
        return Result(result);
    }

    /// <summary>
    /// Delete operation of user himself
    /// </summary>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user delete operation</returns>
    [HttpDelete("myself")]
    [Authorize(Policy = Policies.OnlyUserAccess)]
    public async Task<IActionResult> DeleteUser(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteUserCommand(GetUserId()), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Update operation of user role
    /// </summary>
    /// <param name="updateUserRoleDto">UpdateUserRoleDto</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user role update operation</returns>
    [HttpPut("roles")]//
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleDto updateUserRoleDto, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<UpdateUserRoleCommand>(updateUserRoleDto), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Get all users operation
    /// </summary>
    /// <param name="pageInfo">PageInfo which contains number of current page and number of items per page</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of get all users operation</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllUsers([FromQuery]PageInfo pageInfo, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllUsersQuery(pageInfo), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Get user by id operation
    /// </summary>
    /// <param name="userId">Guid identifier of user</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of get user by id operation</returns>
    [HttpGet("{userId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetUserById([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserByIdQuery(userId), cancellationToken);
        return Result(result);
    }
}
