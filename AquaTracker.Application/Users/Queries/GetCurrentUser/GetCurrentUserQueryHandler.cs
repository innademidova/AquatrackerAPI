using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Application.Users.DTOs;
using ErrorOr;
using Mapster;
using MediatR;

namespace AquaTracker.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler(IUsersRepository usersRepository)
    : IRequestHandler<GetCurrentUserQuery, ErrorOr<UserDto>>
{
    public async Task<ErrorOr<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await usersRepository.GetCurrentUser();

        return currentUser.Adapt<UserDto>();
    }
}