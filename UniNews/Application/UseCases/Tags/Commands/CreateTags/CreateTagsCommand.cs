namespace Uninews.Application.UseCases.Tags.Commands.CreateTags;

public class CreateTagsCommand
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set;} = default!;
    public List<string> Courses { get; private set; } = default!;
}