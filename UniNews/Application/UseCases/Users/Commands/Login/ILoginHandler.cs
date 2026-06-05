using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Commands.Login;

public interface ILoginHandler
{
    Task<Result<UserResponseDto>> Handle(LoginCommand command);
}