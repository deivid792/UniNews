using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.UnitNews.Queries.GetNewsById;

public interface IGetNewsByIdHandler
{
    Task<Result<NewsResponseDto>> HandleAsync(Guid id);
}