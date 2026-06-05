using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Users.Commands.UpdateUser;

public interface IUpdateUserHandler
{
    Task<Result<UserResponseDto>> Handle(UpdateUserCommand command);
}