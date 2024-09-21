using AquaTracker.Application.Auth.DTOs;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Auth.Commands.SignIn;

public record SignInCommand(string Email, string Password)
    : IRequest<ErrorOr<AuthResponse>>;