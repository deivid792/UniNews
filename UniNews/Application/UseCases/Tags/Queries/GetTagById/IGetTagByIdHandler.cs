using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Tags.Queries.GetTagById;

public interface IGetTagByIdHandler
{
    Task<Result<TagResponseDto>> HandleAsync(Guid id);
}