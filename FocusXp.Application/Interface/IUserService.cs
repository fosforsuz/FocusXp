using FluentResults;
using FocusXp.Application.Command;
using FocusXp.Application.Dto;
using FocusXp.Domain.Entities;
using MediatR;

namespace FocusXp.Application.Interface;

public interface IUserService
{
    Task<Result<UserDto>> GetUserProfileAsync(Guid userId, CancellationToken cancellationToken);

    Task<Result<User>> CreateUserAsync(CreateUserCommand createUser,
        CancellationToken cancellationToken);
    Task<Result<Unit>> UpdatePasswordAsync(UpdatePasswordCommand command, CancellationToken cancellationToken);
    Task<Result<Unit>> UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken);
}