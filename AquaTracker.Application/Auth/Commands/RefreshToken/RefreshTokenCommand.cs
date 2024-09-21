using AquaTracker.Application.Auth.DTOs;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<ErrorOr<AuthResponse>>;