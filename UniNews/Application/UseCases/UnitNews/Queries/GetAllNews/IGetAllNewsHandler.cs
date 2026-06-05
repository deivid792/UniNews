using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.News.Queries.GetAllNews;

public interface IGetAllNewsHandler
{
    Task<Result<IEnumerable<NewsResponseDto>>> HandleAsync();
}