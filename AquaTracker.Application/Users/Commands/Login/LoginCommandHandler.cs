using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Users.Commands.Login;

public class LoginCommandHandler: IRequestHandler<LoginCommand, ErrorOr<string>>
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IUsersRepository _usersRepository;

    public LoginCommandHandler(IJwtTokenGenerator tokenGenerator, IUsersRepository usersRepository)
    {
        _tokenGenerator = tokenGenerator;
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetUserByEmailAsync(request.Email);

        if (user == null)
        {
            return Error.Failure("Email or password are incorrect");
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Error.Failure("Email or password are incorrect");
        }

        var accessToken = _tokenGenerator.GenerateToken(user);

        return accessToken;
    }
}