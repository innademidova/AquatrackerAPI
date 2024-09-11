using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Infrastructure.Common.Persistence;
using AquaTracker.Infrastructure.Users.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AquaTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AquaTrackerDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}