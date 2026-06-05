using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Ocurrences.Commands.DeleteOcurrence;

public class DeleteOcurrenceHandler : IDeleteOcurrenceHandler
{
    private readonly IOcurrenceRepository _repository;

    public DeleteOcurrenceHandler(IOcurrenceRepository repository) => _repository = repository;

    public async Task<Result> Handle(Guid id)
    {
        var ocurrence = await _repository.GetByIdAsync(id);
        if (ocurrence == null) return Result.Fail("Ocorrência não encontrada.");

        await _repository.DeleteAsync(ocurrence);
        return Result.Success();
    }
}