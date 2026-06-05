using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;
using Uninews.Domain.ValueObjects;

namespace Uninews.Application.UseCases.News.Commands.UpdateNews;

public class UpdateNewsHandler : IUpdateNewsHandler
{
    private readonly INewsRepository _repository;

    public UpdateNewsHandler(INewsRepository repository) => _repository = repository;

    public async Task<Result<NewsResponseDto>> Handle(UpdateNewsCommand command)
    {
        var news = await _repository.GetByIdAsync(command.Id);
        if (news == null) return Result<NewsResponseDto>.Fail("Notícia não encontrada.");

        var title = Title.Create(command.Title);
        var description = Description.Create(command.Description);

        if (title.HasErros) return Result<NewsResponseDto>.Fail(title.Erros);
        if (description.HasErros) return Result<NewsResponseDto>.Fail(description.Erros);

        news.UpdateNews(title, description, command.Link);
        await _repository.UpdateAsync(news);

        var response = new NewsResponseDto {
            Id = news.Id.ToString(),
            Title = news.Title.Value!,
            Description = news.Description.Value!,
            Link = news.Link,
            Tags = news.Tags.Select(t => t.Name.Value).ToList()!
        };

        return Result<NewsResponseDto>.Success(response);
    }
}