using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Ocurrences.Queries.GetOcurrenceById;

public sealed class GetOcurrenceByIdHandler : IGetOcurrenceByIdHandler
{
    private readonly IOcurrenceRepository _repository;

    public GetOcurrenceByIdHandler(IOcurrenceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<OcurrenceResponseDto>> HandleAsync(Guid id)
    {
        var o = await _repository.GetByIdAsync(id);

        if (o == null)
        {
            return Result<OcurrenceResponseDto>.Fail("Ocorrência não encontrada.");
        }

        var dto = new OcurrenceResponseDto
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
        };

        return Result<OcurrenceResponseDto>.Success(dto);
    }
}