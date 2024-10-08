using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Users.Queries.GetUsersCount;

public class GetUsersCountQueryHandler: IRequestHandler<GetUsersCountQuery, ErrorOr<int>>
{
    private readonly IAppDbContext _dbContext;

    public GetUsersCountQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<int>> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.CountAsync(cancellationToken);
    }
}