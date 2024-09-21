using AquaTracker.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace AquaTracker.Application.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Success>>
{
    private readonly IUsersRepository _usersRepository;

    public UpdateUserCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _usersRepository.UpdateUser(request, cancellationToken);

        return Result.Success;
    }
}