using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Users.Queries.GetAllUsers;

public interface IGetAllUsersHandler
{
    Task<Result<IEnumerable<UserResponseDto>>> HandleAsync();
}