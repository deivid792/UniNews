using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Ocurrences.Queries.GetOcurrenceById;

public interface IGetOcurrenceByIdHandler
{
    Task<Result<OcurrenceResponseDto>> HandleAsync(Guid id);
}