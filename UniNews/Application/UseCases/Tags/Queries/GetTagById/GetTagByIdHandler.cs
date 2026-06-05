using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Tags.Queries.GetTagById;

public sealed class GetTagByIdHandler : IGetTagByIdHandler
{
    private readonly ITagRepository _repository;

    public GetTagByIdHandler(ITagRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<TagResponseDto>> HandleAsync(Guid id)
    {
        var tag = await _repository.GetByIdAsync(id);

        if (tag == null)
        {
            return Result<TagResponseDto>.Fail("Tag não encontrada.");
        }

        var dto = new TagResponseDto
        {
            Id = tag.Id.ToString(),
            Name = tag.Name.Value!,
            Description = tag.Description.Value!
        };

        return Result<TagResponseDto>.Success(dto);
    }
}