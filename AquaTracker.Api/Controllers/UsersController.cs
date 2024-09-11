using AquaTracker.Application.Users.Commands.Login;
using AquaTracker.Application.Users.Commands.Register;
using AquaTracker.Application.Users.DTOs;
using AquaTracker.Contracts.Users.Requests;
using AquaTracker.Contracts.Users.Responses;
using MediatR;
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        var result = await _mediator.Send(command);

        return result.Match(
            success => Ok("User registered successfully"),
            errors => Problem(statusCode: 400, detail: string.Join(",", errors.Select(e => e.Code))));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _mediator.Send(command);
        
        return result.Match(
            success => Ok(new LoginResponse(result.Value)),
            errors => Problem(statusCode: 400, detail: string.Join(",", errors.Select(e => e.Code))));
    }
}