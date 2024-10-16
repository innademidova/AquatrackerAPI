﻿using AquaTracker.Api.Extensions;
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
    private readonly IWebHostEnvironment _env;

    public AuthController(ISender mediator, IWebHostEnvironment env)
    {
        _mediator = mediator;
        _env = env;
    }

    private void SetTokenCookie(HttpResponse response, string token, string tokenName, int daysValid)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,

            Path = "/",
            Expires = DateTime.UtcNow.AddDays(daysValid)
        };
        if (!_env.IsDevelopment())
        {
            cookieOptions.Domain = ".aquatrack.site";
        }

        response.Cookies.Append(tokenName, token, cookieOptions);
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var command = new SignUpCommand(request.Email, request.Password);
        var result = await _mediator.Send(command);
        return this.ToActionResult(result, _ => Ok("User registered successfully"));
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var command = new SignInCommand(request.Email, request.Password);
        var result = await _mediator.Send(command);

        return this.ToActionResult(result, authResponse =>
        {
            SetTokenCookie(Response, authResponse.AccessToken, "accessToken", 1);
            SetTokenCookie(Response, authResponse.RefreshToken, "refreshToken", 7);

            return Ok(new SignInResponse(authResponse.AccessToken));
        });
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

        return this.ToActionResult(result, authResponse =>
        {
            SetTokenCookie(Response, authResponse.RefreshToken, "refreshToken", 7);

            return Ok(new { authResponse.AccessToken });
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        var command = new SignOutCommand();
        var result = await _mediator.Send(command);

        return this.ToActionResult(result, success =>
        {
            Response.Cookies.Delete("refreshToken");
            Response.Cookies.Delete("accessToken");
            return Ok(success);
        });
    }
}