using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

public class DeleteTagHandler : IDeleteTagHandler
{
    private readonly ITagRepository _repository;

    public DeleteTagHandler(ITagRepository repository) => _repository = repository;

    public async Task<Result> Handle(Guid id)
    {
        var tag = await _repository.GetByIdAsync(id);
        if (tag == null) return Result.Fail("Tag não encontrada.");

        await _repository.DeleteAsync(tag);
        return Result.Success();
    }
}