using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Entities.Tags;

namespace Uninews.Application.UseCases.Tags.Queries.GetAllTags;

public interface IGetAllTagsHandler
{
    Task<Result<IEnumerable<TagResponseDto>>> HandleAsync();
}