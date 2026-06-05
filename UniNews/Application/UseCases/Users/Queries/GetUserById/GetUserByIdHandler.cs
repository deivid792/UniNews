using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Users.Queries.GetUserById;

public sealed class GetUserByIdHandler : IGetUserByIdHandler
{
    private readonly IUserRepository _repository;

    public GetUserByIdHandler(IUserRepository repository) => _repository = repository;

    public async Task<Result<UserResponseDto>> HandleAsync(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            return Result<UserResponseDto>.Fail("Usuário não encontrado.");

        var response = new UserResponseDto
        {
            ID = user.Id.ToString(),
            Name = user.Name.Value!,
            Email = user.Email.Value!,
            CPF = user.CPF.Value
        };

        return Result<UserResponseDto>.Success(response);
    }
}