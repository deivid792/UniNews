using Uninews.Application.Shared.Result;

public interface IDeleteTagHandler
{
    Task<Result> Handle(Guid id);
}