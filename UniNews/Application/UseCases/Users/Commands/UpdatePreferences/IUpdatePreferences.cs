using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Users.Commands.UpdatePreferences;

public interface IUpdatePreferencesHandler
{
    Task<Result> HandleAsync(UpdatePreferencesCommand command);
}