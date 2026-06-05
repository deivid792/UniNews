using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Ocurrences.Queries.GetAllOcurrences;

public sealed class GetAllOcurrencesHandler : IGetAllOcurrencesHandler
{
    private readonly IOcurrenceRepository _repository;

    public GetAllOcurrencesHandler(IOcurrenceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<OcurrenceResponseDto>>> HandleAsync()
    {
        var ocurrences = await _repository.GetAllAsync();

        if (ocurrences == null || !ocurrences.Any())
        {
            return Result<IEnumerable<OcurrenceResponseDto>>.Fail("Nenhuma ocorrência encontrada.");
        }

        var response = ocurrences.Select(o => new OcurrenceResponseDto
        {
            Id = o.Id.ToString(),
            Title = o.Title.Value!,
            Category = o.Category.Value!,
            Description = o.Description.Value!,
            Minister = o.Minister.Value!,
            Date = o.Date,
            Time = o.Time,
            Location = o.Location.Value!,
            Link = o.Link
        });

        return Result<IEnumerable<OcurrenceResponseDto>>.Success(response);
    }
}