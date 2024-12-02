using Application.Common.Dtos.User;
using Application.UseCases.UserCases.Commands.AuthenticationUserCase;
using Application.UseCases.UserCases.Commands.DeleteUserCase;
using Application.UseCases.UserCases.Commands.RegisterUserCase;
using Application.UseCases.UserCases.Commands.UpdateUserCase;
using Application.UseCases.UserCases.Queries.GetAllUsersCase;
using Application.UseCases.UserCases.Queries.GetUserByIdCase;
using AutoMapper;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("users")]
[ApiController]
[Authorize]
public class UserController(
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    IMediator mediator)
    : CustomControllerBase(httpContextAccessor)
{
    /// <summary>
    /// Registration of new user.
    /// </summary>
    /// <param name="registerUserDto">RegisterUserDto which contains new user information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user registration</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto, CancellationToken cancellationToken)
    {
        var registerUserCommand = mapper.Map<RegisterUserCommand>(registerUserDto);
        
        var result = await mediator.Send(registerUserCommand, cancellationToken);
        
        return Ok(result);
    }

    /// <summary>
    /// Authentication of user
    /// </summary>
    /// <param name="authUserDto">AuthUserDto which contains user information for authentication</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user authentication</returns>
    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthUserDto authUserDto, CancellationToken cancellationToken)
    {
        var authenticationUserCommand = mapper.Map<AuthenticationUserCommand>(authUserDto);
        
        var result = await mediator.Send(authenticationUserCommand, cancellationToken);
        
        return Ok(result);
    }

    /// <summary>
    /// User update operation
    /// </summary>
    /// <param name="updateUserDto">UpdateUserDto which contains new information of existed user</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user update operation</returns>
    [HttpPut("update")]
    [Authorize(Policy = Policies.OnlyUserAccess)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var updateUserCommand = mapper.Map<UpdateUserCommand>(updateUserDto);
        updateUserCommand.Id = GetUserId();
        
        var result = await mediator.Send(updateUserCommand, cancellationToken);
        
        return Ok(result);
    }

    /// <summary>
    /// Delete operation of user
    /// </summary>
    /// <param name="deleteUserDto">DeleteUserDto which contains id of user you want to delete</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user delete operation</returns>
    [HttpDelete("delete")]
    [Authorize(Policy = Policies.OnlyUserAccess)]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDto deleteUserDto, CancellationToken cancellationToken)
    {
        var deleteUserCommand = mapper.Map<DeleteUserCommand>(deleteUserDto);
        
        var result = await mediator.Send(deleteUserCommand, cancellationToken);
        
        return Ok(result);
    }

    /// <summary>
    /// Delete operation of user himself
    /// </summary>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user delete operation</returns>
    [HttpDelete("delete/me")]
    [Authorize(Policy = Policies.OnlyUserAccess)]
    public async Task<IActionResult> DeleteUser(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteUserCommand(GetUserId()), cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Update operation of user role
    /// </summary>
    /// <param name="updateUserRoleDto">UpdateUserRoleDto</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of user role update operation</returns>
    [HttpPut("role")]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleDto updateUserRoleDto, CancellationToken cancellationToken)
    {
        var updateUserRoleCommand = mapper.Map<UpdateUserRoleDto>(updateUserRoleDto);
        
        var result = await mediator.Send(updateUserRoleCommand, cancellationToken);
        
        return Ok(result);
    }

    /// <summary>
    /// Get all users operation
    /// </summary>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of get all users operation</returns>
    [HttpGet("all")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllUsersQuery(), cancellationToken);
        
        return Ok(result);
    }

    /// <summary>
    /// Get current user operation
    /// </summary>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of get current user</returns>
    [HttpGet("me")]
    [Authorize(Policy = Policies.EveryoneAccess)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserByIdQuery(GetUserId()), cancellationToken);
        
        return Ok(result);
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
        
        return Ok(result);
    }
}
