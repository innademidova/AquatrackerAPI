using AquaTracker.Application.Auth.Commands.SignIn;
using AquaTracker.Application.Auth.Commands.SignUp;
using AquaTracker.Contracts.Users.Requests;
using AquaTracker.Contracts.Users.Responses;
using MediatR;
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
    
    private void SetTokenCookie(HttpResponse response, string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true, // Защищает от XSS, куки недоступны для JavaScript
            Secure = true,   // Куки будут передаваться только по HTTPS
            SameSite = SameSiteMode.None, // Для поддержки кросс-доменных запросов (если это нужно)
            Expires = DateTime.UtcNow.AddMinutes(30) // Время жизни куки
        };

        response.Cookies.Append("token", token, cookieOptions);
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
            accessToken =>
            {
                SetTokenCookie(Response, result.Value);
                return Ok(new SignInResponse(result.Value));
            },
            errors => Problem(statusCode: 400, detail: string.Join(",", errors.Select(e => e.Code))));
    }
}