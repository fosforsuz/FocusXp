using FluentResults;
using FocusXp.Application.Dto;
using FocusXp.Application.Interface;
using MediatR;

namespace FocusXp.Application.Query.Handler;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Result<UserDto>>
{
    private readonly IUserService _userService;

    public GetProfileQueryHandler(IUserService userService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<Result<UserDto>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserProfileAsync(request.UserId, cancellationToken);
        return user;
    }
}