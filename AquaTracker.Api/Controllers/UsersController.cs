using AquaTracker.Application.Users.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController: ControllerBase
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
}