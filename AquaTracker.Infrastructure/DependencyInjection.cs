using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Infrastructure.Common.Persistence;
using AquaTracker.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AquaTracker.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IAppDbContext, AquaTrackerDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<AquaTrackerDbContext>());
        return services;
    }
    
    public static IApplicationBuilder UseSetupUserClaimsMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<SetupUserClaimsMiddleware>();
        return app;
    }
}