using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Users.Queries.GetUserById;

public interface IGetUserByIdHandler
{
    Task<Result<UserResponseDto>> HandleAsync(Guid id);
}