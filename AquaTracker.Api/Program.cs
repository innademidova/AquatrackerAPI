using AquaTracker.Application;
using AquaTracker.Infrastructure;
using AquaTracker.Infrastructure.Common.Persistence;
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
        .WithOrigins("https://zealous-rock-05b8ec81e.5.azurestaticapps.net", "http://localhost:5173")
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

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseExceptionHandler();
app.MapControllers();
app.UseSetupUserClaimsMiddleware();

app.Run();