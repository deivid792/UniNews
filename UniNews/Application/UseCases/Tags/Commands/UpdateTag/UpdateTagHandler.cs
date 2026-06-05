using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;
using Uninews.Domain.ValueObjects;

namespace Uninews.Application.UseCases.Tags.Commands.UpdateTag;

public class UpdateTagHandler : IUpdateTagHandler
{
    private readonly ITagRepository _repository;

    public UpdateTagHandler(ITagRepository repository) => _repository = repository;

    public async Task<Result<TagResponseDto>> Handle(UpdateTagCommand command)
    {
        var tag = await _repository.GetByIdAsync(command.Id);
        if (tag == null) return Result<TagResponseDto>.Fail("Tag não encontrada.");

        var name = Name.Create(command.Name);
        var description = Description.Create(command.Description);

        if (name.HasErros) return Result<TagResponseDto>.Fail(name.Erros);
        if (description.HasErros) return Result<TagResponseDto>.Fail(description.Erros);

        tag.UpdateTag(name, description);
        await _repository.UpdateAsync(tag);

        var response = new TagResponseDto {
            Id = tag.Id.ToString(),
            Name = tag.Name.Value!,
            Description = tag.Description.Value!
        };

        return Result<TagResponseDto>.Success(response);
    }
}