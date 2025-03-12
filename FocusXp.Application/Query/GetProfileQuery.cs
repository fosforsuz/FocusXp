using FluentResults;
using FocusXp.Application.Dto;
using MediatR;

namespace FocusXp.Application.Query;

public class GetProfileQuery : IRequest<Result<UserDto>>
{
    public Guid UserId { get; set; }
}