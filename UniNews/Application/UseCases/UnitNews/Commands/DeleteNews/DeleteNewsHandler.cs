using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.News.Commands.DeleteNews;

public sealed class DeleteNewsHandler : IDeleteNewsHandler
{
    private readonly INewsRepository _repository;

    public DeleteNewsHandler(INewsRepository repository) => _repository = repository;

    public async Task<Result> Handle(Guid id)
    {
        var news = await _repository.GetByIdAsync(id);
        
        if (news == null)
            return Result.Fail("Notícia não encontrada.");

        await _repository.DeleteAsync(news);
        
        return Result.Success();
    }
}