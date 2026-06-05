using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;
using Uninews.Domain.ValueObjects;

namespace Uninews.Application.UseCases.Ocurrences.Commands.UpdateOcurrence;

public class UpdateOcurrenceHandler : IUpdateOcurrenceHandler
{
    private readonly IOcurrenceRepository _repository;

    public UpdateOcurrenceHandler(IOcurrenceRepository repository) => _repository = repository;

    public async Task<Result<OcurrenceResponseDto>> Handle(UpdateOcurrenceCommand command)
    {
        var ocurrence = await _repository.GetByIdAsync(command.Id);
        if (ocurrence == null) return Result<OcurrenceResponseDto>.Fail("Ocorrência não encontrada.");

        var title = Title.Create(command.Title);
        var category = Category.Create(command.Category);
        var description = Description.Create(command.Description);
        var minister = Name.Create(command.Minister);
        var location = Location.Create(command.Location);

        if (title.HasErros)
            return Result<OcurrenceResponseDto>.Fail(title.Erros);

        if (category.HasErros)
            return Result<OcurrenceResponseDto>.Fail(category.Erros);

        if (description.HasErros)
            return Result<OcurrenceResponseDto>.Fail(description.Erros);

        if (minister.HasErros)
            return Result<OcurrenceResponseDto>.Fail(minister.Erros);

        if (location.HasErros)
            return Result<OcurrenceResponseDto>.Fail(location.Erros);

        ocurrence.UpdateOcurrence(title, category, description, minister, command.Date, command.Time, location, command.Link);

        await _repository.UpdateAsync(ocurrence);

        var response = new OcurrenceResponseDto {
            Id = ocurrence.Id.ToString(),
            Title = ocurrence.Title.Value!,
            Category = ocurrence.Category.Value!,
            Description = ocurrence.Description.Value!,
            Minister = ocurrence.Minister.Value!,
            Location = ocurrence.Location.Value!
        };

        return Result<OcurrenceResponseDto>.Success(response);
    }
}