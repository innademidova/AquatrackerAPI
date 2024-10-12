using AquaTracker.Api.Extensions;
using AquaTracker.Application.Users.Commands;
using AquaTracker.Application.Users.Queries.GetCurrentUser;
using AquaTracker.Application.Users.Queries.GetUsersCount;
using AquaTracker.Contracts.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var query = new GetCurrentUserQuery();
        var result = await _mediator.Send(query);

        return result.Match(
            currentUser => Ok(result.Value),
            errors => Problem());
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
    {
        var command = new UpdateUserCommand(request.Name, request.Email, request.Gender, request.DailyWaterGoal,
            request.Weight,
            request.ActiveTime);
        var result = await _mediator.Send(command);

        return this.ToActionResult(result, _ => Ok("User was updated successfully"));
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetUsersCount()
    {
        var query = new GetUsersCountQuery();
        var result = await _mediator.Send(query);

        return this.ToActionResult(result, count => Ok(count));
    }
}