using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Users.Queries.GetAllUsers;

public sealed class GetAllUsersHandler : IGetAllUsersHandler
{
    private readonly IUserRepository _repository;

    public GetAllUsersHandler(IUserRepository repository) => _repository = repository;

    public async Task<Result<IEnumerable<UserResponseDto>>> HandleAsync()
    {
        var users = await _repository.GetAllAsync();

        var response = users.Select(u => new UserResponseDto
        {
            ID = u.Id.ToString(),
            Name = u.Name.Value!,
            Email = u.Email.Value!,
            CPF = u.CPF.Value
        });

        return Result<IEnumerable<UserResponseDto>>.Success(response);
    }
}