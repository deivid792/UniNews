using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.UnitNews.Queries.GetNewsById;

public sealed class GetNewsByIdHandler : IGetNewsByIdHandler
{
    private readonly INewsRepository _repository;

    public GetNewsByIdHandler(INewsRepository repository) => _repository = repository;

    public async Task<Result<NewsResponseDto>> HandleAsync(Guid id)
    {
        var news = await _repository.GetByIdAsync(id);

        if (news == null)
            return Result<NewsResponseDto>.Fail("Notícia não encontrada.");

        var response = new NewsResponseDto
        {
            Id = news.Id.ToString(),
            Title = news.Title.Value!,
            Description = news.Description.Value!,
            Date = news.Date,
            Time = news.Time,
            Link = news.Link,
            Tags = news.Tags.Select(t => t.Name.Value).ToList()!
        };

        return Result<NewsResponseDto>.Success(response);
    }
}