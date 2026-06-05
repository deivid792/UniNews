using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Ocurrences.Commands.UpdateOcurrence;

public interface IUpdateOcurrenceHandler
{
    Task<Result<OcurrenceResponseDto>> Handle(UpdateOcurrenceCommand command);
}