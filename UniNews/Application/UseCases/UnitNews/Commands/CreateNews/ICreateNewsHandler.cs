using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.UnitNews.Commands.CreateNews;

public interface ICreateNewsHandler
{
    Task<Result<NewsResponseDto>> HandleAsync(CreateNewsCommand command);
}