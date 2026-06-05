using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.News.Queries.GetAllNews;

public sealed class GetAllNewsHandler : IGetAllNewsHandler
{
    private readonly INewsRepository _repository;

    public GetAllNewsHandler(INewsRepository repository) => _repository = repository;

    public async Task<Result<IEnumerable<NewsResponseDto>>> HandleAsync()
    {
        var newsList = await _repository.GetAllAsync();

        var dtos = newsList.Select(n => new NewsResponseDto
        {
            Id = n.Id.ToString(),
            Title = n.Title.Value!,
            Description = n.Description.Value!,
            Date = n.Date,
            Time = n.Time,
            Link = n.Link
        });

        return Result<IEnumerable<NewsResponseDto>>.Success(dtos);
    }
}