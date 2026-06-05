using System.Diagnostics.Metrics;
using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Entities.Courses;
using Uninews.Domain.Entities.Tags;
using Uninews.Domain.Interfaces;
using Uninews.Domain.ValueObjects;

namespace Uninews.Application.UseCases.Tags.Commands.CreateTags;

public class CreateTagsHandler : ICreateTagsHandler
{
    private readonly ITagRepository _tagRepository;

    public CreateTagsHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Result<TagResponseDto>> Handle(CreateTagsCommand command)
    {
        var name = Name.Create(command.Name);
        if (name.HasErros)
            return Result<TagResponseDto>.Fail(name.Erros);

        var description = Description.Create(command.Description);
        if (description.HasErros)
            return Result<TagResponseDto>.Fail(description.Erros);

        var courses = command.Courses
            .Select(c => Course.Create(Name.Create(c)))
            .ToList();

        var tag = Tag.Create(name, description, courses);

        if (tag.HasErros)
        {
            return Result<TagResponseDto>.Fail(tag.Erros);
        }

        await _tagRepository.AddAsync(tag);

        var response = new TagResponseDto()
        {
            Id = tag.Id.ToString(),
            Name = tag.Name.Value!,
            Description = tag.Description.Value!,
            Courses = tag.Courses.Select(c => c.Name.Value).ToList()!
        };

        return Result<TagResponseDto>.Success(response);
    }
}