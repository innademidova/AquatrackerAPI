using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Users;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Users.Commands.Register;

public class RegisterCommandHandler: IRequestHandler<RegisterCommand, ErrorOr<Success>>
{
    private readonly IUsersRepository _usersRepository;

    public RegisterCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<Success>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _usersRepository.GetUserByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return Error.Failure("User with this email already exists.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PasswordHash = passwordHash
        };

        await _usersRepository.Register(user);

        return Result.Success;
    }
}