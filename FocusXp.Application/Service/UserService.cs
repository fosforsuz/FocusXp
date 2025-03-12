using FluentResults;
using FocusXp.Application.Command;
using FocusXp.Application.Dto;
using FocusXp.Application.Interface;
using FocusXp.Domain.Constant;
using FocusXp.Domain.Entities;
using FocusXp.Domain.Extensions;
using FocusXp.Infrastructure.Interfaces.Data;
using MediatR;

namespace FocusXp.Application.Service;

public class UserService : BaseService, IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<UserDto>> GetUserProfileAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetSingleAsync(
            predicate: u => u.Id == userId && !u.IsDeleted,
            selector: u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Fullname = u.Fullname,
                Level = u.Level,
                XpPoints = u.XpPoints,
                Role = u.Role.GetRoleString(),
                CreatedAt = u.CreatedAt,
                ModifiedAt = u.UpdatedAt
            },
            cancellationToken: cancellationToken
        );

        return user is null
            ? Result.Fail<UserDto>(ErrorMessages.NotFound.UserNotFound)
            : Result.Ok(user);
    }

    public async Task<Result<User>> CreateUserAsync(CreateUserCommand createUser,
        CancellationToken cancellationToken)
    {
        var userExistResult = await CheckUserExist(createUser.Email, createUser.Username, cancellationToken);
        if (userExistResult.IsFailed)
            return Result.Fail<User>(userExistResult.Errors);

        var user = CreateUserCommandToEntity(createUser);
        return await CreateUserAsync(user, cancellationToken);
    }

    public async Task<Result<Unit>> UpdatePasswordAsync(UpdatePasswordCommand command, CancellationToken cancellationToken)
    {
        var userResult = await GetUserById(command.UserId, cancellationToken);
        if (userResult.IsFailed)
            return Result.Fail<Unit>(userResult.Errors);

        var user = userResult.Value;

        if (!user.CheckPassword(command.OldPassword))
            return Result.Fail<Unit>(ErrorMessages.Invalid.OldPassword);

        if (command.NewPassword != command.ConfirmPassword)
            return Result.Fail<Unit>(ErrorMessages.Invalid.PasswordNotMatch);

        user.UpdatePassword(command.NewPassword);

        return await UpdateUserAsync(user, cancellationToken);
    }

    public async Task<Result<Unit>> UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var userExistResult = await CheckUserExists(command.UserId, command.Email, command.Username, cancellationToken);
        if (userExistResult.IsFailed)
            return Result.Fail<Unit>(userExistResult.Errors);

        var userResult = await GetUserById(command.UserId, cancellationToken);
        if (userResult.IsFailed)
            return Result.Fail<Unit>(userResult.Errors);

        var user = userResult.Value;
        user.UpdateUser(command.Username, command.Email, command.Fullname);

        return await UpdateUserAsync(user, cancellationToken);
    }



    private async Task<Result> CheckUserExist(string email, string username, CancellationToken cancellationToken)
    {
        var result = new Result();

        var isEmailExist = await _unitOfWork.Users.AnyAsync(
            user => user.Email == email && !user.IsDeleted,
            cancellationToken
        );

        if (isEmailExist)
            result.Errors.Add(new Error(ErrorMessages.Exists.EmailExists));

        var isUsernameExist = await _unitOfWork.Users.AnyAsync(
            user => user.Username == username && !user.IsDeleted,
            cancellationToken
        );

        if (isUsernameExist)
            result.Errors.Add(new Error(ErrorMessages.Exists.UsernameExists));

        return result.Errors.Any()
            ? Result.Fail(result.Errors)
            : Result.Ok();
    }

    private async Task<Result> CheckUserExists(Guid userId, string email, string username, CancellationToken cancellationToken)
    {
        var result = new Result();

        var isEmailExist = await _unitOfWork.Users.AnyAsync(
            user => user.Email == email && user.Id != userId && !user.IsDeleted,
            cancellationToken
        );

        if (isEmailExist)
            result.Errors.Add(new Error(ErrorMessages.Exists.EmailExists));

        var isUsernameExist = await _unitOfWork.Users.AnyAsync(
            user => user.Username == username && user.Id != userId && !user.IsDeleted,
            cancellationToken
        );

        if (isUsernameExist)
            result.Errors.Add(new Error(ErrorMessages.Exists.UsernameExists));

        return result.Errors.Any()
            ? Result.Fail(result.Errors)
            : Result.Ok();
    }

    private async Task<Result<User>> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetSingleAsync(
            predicate: u => u.Id == userId && !u.IsDeleted,
            cancellationToken: cancellationToken
        );

        return user is null
            ? Result.Fail<User>(ErrorMessages.NotFound.UserNotFound)
            : Result.Ok(user);
    }

    private async Task<Result<User>> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        return await ExecuteInTransactionAsync(async () =>
        {
            await _unitOfWork.Users.AddAsync(user, cancellationToken);
            return Result.Ok(user);
        }, cancellationToken);
    }

    private async Task<Result<Unit>> UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        return await ExecuteInTransactionAsync(async () =>
        {
            await _unitOfWork.Users.UpdateAsync(user, cancellationToken);
            return Result.Ok(Unit.Value);
        }, cancellationToken);
    }

    private static User CreateUserCommandToEntity(CreateUserCommand createUser)
    {
        return User.CreateNewUser(createUser.Username, password: createUser.Password,
            email: createUser.Email, fullname: createUser.Fullname);
    }
}