using System;
using FluentResults;
using FocusXp.Application.Interface;
using MediatR;

namespace FocusXp.Application.Command.Handler;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Unit>>
{

    private readonly IUserService _userService;

    public UpdateUserCommandHandler(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<Result<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateUserAsync(request, cancellationToken);
        return result;
    }
}
