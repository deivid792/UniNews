using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Ocurrences.Queries.GetAllOcurrences;

public interface IGetAllOcurrencesHandler
{
    Task<Result<IEnumerable<OcurrenceResponseDto>>> HandleAsync();
}