using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Users;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AquaTracker.Application.Auth.Commands.SignUp;

public class SignUpCommandHandler: IRequestHandler<SignUpCommand, ErrorOr<Success>>
{
    private readonly IAppDbContext _dbContext;

    public SignUpCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Success>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (existingUser != null)
        {
            return Error.Failure("User with this email already exists.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash
        };

        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.CommitChangesAsync();

        return Result.Success;
    }
}