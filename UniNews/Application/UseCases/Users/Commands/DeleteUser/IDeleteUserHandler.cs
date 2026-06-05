using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Users.Commands.DeleteUser;

public interface IDeleteUserHandler
{
    Task<Result> Handle(Guid id);
}