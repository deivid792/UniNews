namespace Uninews.Application.UseCases.UnitNews.Commands.CreateNews;

public sealed class CreateNewsCommand
{
    public Guid UserId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Link { get; private set; }
    public List<Guid> TagIds { get; private set; }

    public CreateNewsCommand(Guid userId, string title, string description, List<Guid> tagIds, string link )
    {
        UserId = userId;
        Title = title;
        Description = description;
        Link = link;
        TagIds = tagIds ?? new List<Guid>();
    }
}