using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Users.Commands.DeleteUser;

public sealed class DeleteUserHandler : IDeleteUserHandler
{
    private readonly IUserRepository _repository;

    public DeleteUserHandler(IUserRepository repository) => _repository = repository;

    public async Task<Result> Handle(Guid id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            return Result.Fail("Usuário não encontrado.");

        await _repository.DeleteAsync(user);

        return Result.Success();
    }
}