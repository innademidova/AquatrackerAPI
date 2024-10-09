using AquaTracker.Application.Auth.Commands.RefreshToken;
using AquaTracker.Application.Auth.Commands.SignIn;
using AquaTracker.Application.Auth.Commands.SignOut;
using AquaTracker.Application.Auth.Commands.SignUp;
using AquaTracker.Contracts.Auth.Requests;
using AquaTracker.Contracts.Users.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }

    private void SetTokenCookie(HttpResponse response, string token, string tokenName, int daysValid)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.UtcNow.AddDays(daysValid)
        };

        response.Cookies.Append(tokenName, token, cookieOptions);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var command = new SignUpCommand(request.Email, request.Password);
        var result = await _mediator.Send(command);

        return result.Match(
            success => Ok("User registered successfully"),
            errors => Problem(statusCode: 400, detail: string.Join(",", errors.Select(e => e.Code))));
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var command = new SignInCommand(request.Email, request.Password);
        var result = await _mediator.Send(command);

        return result.Match(
            authResponse =>
            {
                SetTokenCookie(Response, authResponse.AccessToken, "accessToken", 1);
                SetTokenCookie(Response, authResponse.RefreshToken, "refreshToken", 7);

                return Ok(new SignInResponse(authResponse.AccessToken));
            },
            errors => Problem(statusCode: 400, detail: string.Join(",", errors.Select(e => e.Code))));
    }

    [Authorize]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized("No refresh token provided.");
        }

        var command = new RefreshTokenCommand(refreshToken);
        var result = await _mediator.Send(command);

        return result.Match(
            authResponse =>
            {
                SetTokenCookie(Response, authResponse.RefreshToken, "refreshToken", 7);

                return Ok(new { AccessToken = authResponse.AccessToken });
            },
            errors => Problem(statusCode: 401, detail: string.Join(",", errors.Select(e => e.Code))));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        var command = new SignOutCommand();
        var result = await _mediator.Send(command);

        return result.Match(
            success =>
            {
                Response.Cookies.Delete("refreshToken");
                Response.Cookies.Delete("accessToken");
                return Ok(success);
            },
            errors => Problem(statusCode: 401, detail: string.Join(",", errors.Select(e => e.Code))));
    }
}