using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.News.Commands.DeleteNews;

public interface IDeleteNewsHandler
{
    Task<Result> Handle(Guid id);
}