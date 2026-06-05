using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Tags.Commands.CreateTags;

public interface ICreateTagsHandler
{
    public Task<Result<TagResponseDto>> Handle(CreateTagsCommand createTagsCommand);
}