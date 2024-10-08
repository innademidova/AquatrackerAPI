using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Users.Queries.GetUsersCount;

public record GetUsersCountQuery(): IRequest<ErrorOr<int>>;