using System;
using FluentResults;
using FocusXp.Application.Interface;
using MediatR;

namespace FocusXp.Application.Command.Handler;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Result<Unit>>
{

    private readonly IUserService _userService;

    public UpdatePasswordCommandHandler(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<Result<Unit>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdatePasswordAsync(request, cancellationToken);
        return result;
    }
}
