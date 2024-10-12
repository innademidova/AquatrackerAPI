using AquaTracker.Application;
using AquaTracker.Infrastructure;
using AquaTracker.Infrastructure.Common.Persistence;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Default")!);
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
{
    policy.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("https://www.aquatrack.site", "https://aquatrack.site", "http://localhost:5173")
        .AllowCredentials();
}));


var app = builder.Build();
await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AquaTrackerDbContext>();
    var migrations = await dbContext.Database.GetPendingMigrationsAsync();
    if (migrations.Any())
    {
        await dbContext.Database.MigrateAsync();
    }
}

app.MapGet("/health", (IConfiguration configuration) => new
{
    ImageTag = configuration["CurrentImageTag"]
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext context) =>
{
    Exception? exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

    if (exception is null)
    {
        //handling, logging

        return Results.Problem();
    }

    if (exception is FluentValidation.ValidationException validationException)
    {
        var validationErrors = validationException.Errors
            .Select(e => new { e.PropertyName, e.ErrorMessage });

        return Results.ValidationProblem(validationErrors.ToDictionary(
            e => e.PropertyName,
            e => new[] { e.ErrorMessage })
        );
    }

    return Results.Problem();
});
app.MapControllers();
app.UseSetupUserClaimsMiddleware();

app.Run();