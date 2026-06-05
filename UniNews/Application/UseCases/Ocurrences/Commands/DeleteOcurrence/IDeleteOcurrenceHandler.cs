using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Ocurrences.Commands.DeleteOcurrence;

public interface IDeleteOcurrenceHandler
{
    Task<Result> Handle(Guid id);
}