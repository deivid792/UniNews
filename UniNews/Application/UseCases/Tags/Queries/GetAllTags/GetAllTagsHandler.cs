using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Entities.Tags;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Tags.Queries.GetAllTags;

public sealed class GetAllTagsHandler : IGetAllTagsHandler
{
    private readonly ITagRepository _tagRepository;

    public GetAllTagsHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Result<IEnumerable<TagResponseDto>>> HandleAsync()
    {
        var tags = await _tagRepository.GetAllAsync();

        if (tags == null || !tags.Any())
        {
            return Result<IEnumerable<TagResponseDto>>.Fail("Nenhuma tag cadastrada no sistema.");
        }

        var response = tags.Select(t => new TagResponseDto()
        {
            Id = t.Id.ToString(),
            Name = t.Name.Value ?? string.Empty,
            Description = t.Description.Value ?? string.Empty,
            Courses = t.Courses?.Select(c => c.Name?.Value ?? "").ToList() ?? new List<string>()
        }).ToList();

        return Result<IEnumerable<TagResponseDto>>.Success(response);
    }
}