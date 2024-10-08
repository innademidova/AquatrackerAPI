using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Application.Users.DTOs;
using ErrorOr;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler
    : IRequestHandler<GetCurrentUserQuery, ErrorOr<UserDto>>
{
    private readonly IAppDbContext _dbContext;
    private readonly ICurrentUser _currentUser;

    public GetCurrentUserQueryHandler(IAppDbContext dbContext, ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
    }

    public async Task<ErrorOr<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _dbContext.Users.FirstAsync(u => u.Id == _currentUser.Id, cancellationToken);

        return currentUser.Adapt<UserDto>();
    }
}