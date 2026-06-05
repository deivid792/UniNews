using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.News.Commands.UpdateNews;

public interface IUpdateNewsHandler
{
    Task<Result<NewsResponseDto>> Handle(UpdateNewsCommand command);
}