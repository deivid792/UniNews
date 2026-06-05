using Uninews.Domain.Entities.Tags;
using Uninews.Domain.Entities.Users;
using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;

namespace Uninews.Domain.Entities.UnitNews;

public sealed class News: BaseEntity
{
    public Title Title { get; private set; } = default!;
    public DateOnly Date { get; private set; } = default!;
    public TimeOnly Time { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public string? Link { get; private set; } = default!;

    public User User{ get; private set; } = default!;

    private readonly List<Tag> _tags = new();
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

    private News(){}

    private News(User user, Title title, Description description, string? link) : base()
    {
        User = user;
        Title = title;
        Date = DateOnly.FromDateTime(DateTime.UtcNow);
        Time = TimeOnly.FromDateTime(DateTime.UtcNow);
        Description = description;
        Link = link;
    }

    public static News Create(User user, Title title, Description description, string? link)
    {
        var news = new News(user, title, description, link);

        if(title.HasErros)
            news.AddRangeNotification(title.Erros);

        if(description.HasErros)
            news.AddRangeNotification(description.Erros);

        return news;
    }

    public void UpdateNews(Title title, Description description, string? link)
    {
        Title = title;
        Description = description;
        Link = link;
    }

    public void AddTag(Tag tag)
    {
        _tags.Add(tag);
    }

    public void AddTagsList(IEnumerable<Tag> tag)
    {
        _tags.AddRange(tag);
    }

    public void RemoveTags(Tag tag)
    {
        _tags.Remove(tag);
    }

}