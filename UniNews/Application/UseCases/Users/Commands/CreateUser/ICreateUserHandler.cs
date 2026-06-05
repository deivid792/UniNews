using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Users.Commands.CreateUser;

public interface IcreateUserHandler
{
    Task<Result<UserResponseDto>> Handle(CreateUserCommand createUserCommand);
}