using AquaTracker.Application.Users.DTOs;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<ErrorOr<UserDto>>;