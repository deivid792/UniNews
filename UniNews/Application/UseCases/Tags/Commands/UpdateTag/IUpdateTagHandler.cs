using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Tags.Commands.UpdateTag;

public interface IUpdateTagHandler
{
    Task<Result<TagResponseDto>> Handle(UpdateTagCommand command);
}