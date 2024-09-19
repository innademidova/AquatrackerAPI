using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Users;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Auth.Commands.SignUp;

public class SignUpCommandHandler: IRequestHandler<SignUpCommand, ErrorOr<Success>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IAuthRepository _authRepository;

    public SignUpCommandHandler(IUsersRepository usersRepository, IAuthRepository authRepository)
    {
        _usersRepository = usersRepository;
        _authRepository = authRepository;
    }

    public async Task<ErrorOr<Success>> Handle(SignUpCommand request, CancellationToken cancellationToken)
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
            PasswordHash = passwordHash
        };

        await _authRepository.Register(user);

        return Result.Success;
    }
}