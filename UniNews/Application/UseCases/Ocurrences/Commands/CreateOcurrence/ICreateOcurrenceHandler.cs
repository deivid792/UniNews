using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Ocurrences.Commands.CreateOcurrences;
public interface ICreateOcurrenceHandler
{
    Task<Result<OcurrenceResponseDto>> HandleAsync(CreateOcurrenceCommand command);
}