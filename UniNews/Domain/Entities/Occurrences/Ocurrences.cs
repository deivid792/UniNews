using Uninews.Domain.Entities.Tags;
using Uninews.Domain.Entities.Users;
using Uninews.Domain.Enum;
using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;

namespace Uninews.Domain.Entities.Ocurrences;

public sealed class Ocurrence: BaseEntity
{
    public Title Title { get; private set; } = default!; //Tem
    public Category Category { get; private set; } = default!; //Tem
    public Description Description { get; private set; } = default!; //Tem
    public Name Minister { get; private set; } = default!; //Deixar
    public DateOnly Date { get; private set; } = default!; //Tem
    public TimeOnly Time { get; private set; } = default!; //Deixar
    public Location Location { get; private set;} = default!; //Deixar
    public User User{ get; private set; } = default!;
    public string? Link { get; private set; } = default!;

    private readonly List<User> _participants = new();
    public IReadOnlyCollection<User> Participants => _participants;

    public List<Tag> Tags { get; set; } = new();

    private Ocurrence(){}

    private Ocurrence(User user, Title title, Category category, Description description, Name minister, DateOnly date,
    TimeOnly time, Location location, string? link ) : base()
    {
        User = user;
        Title = title;
        Category = category;
        Description = description;
        Minister = minister;
        Date = date;
        Time = time;
        Location = location;
        Link = link;
    }

    public static Ocurrence Create(User user, Title title, Category category, Description description, Name minister,
    DateOnly date, TimeOnly time, Location location, string? link)
    {
        var ocurrence = new Ocurrence(user, title, category, description, minister, date, time, location, link);

        if (title.HasErros) 
            ocurrence.AddRangeNotification(title.Erros);

        if(category.HasErros)
            ocurrence.AddRangeNotification(category.Erros);

        if(description.HasErros)
            ocurrence.AddRangeNotification(description.Erros);

        if(minister.HasErros)
            ocurrence.AddRangeNotification(minister.Erros);

        if(location.HasErros)
            ocurrence.AddRangeNotification(location.Erros);

        var contract = new Contract()
            .Requires()
            .CheckPastDate("Ocurrence",date);

        if (contract.HasErros)
            ocurrence.AddRangeNotification(contract.Erros);

        return ocurrence;

    }

    public void UpdateOcurrence(Title title, Category category, Description description, Name minister,
    DateOnly date, TimeOnly time, Location location, string? link )
    {
        Title = title;
        Category = category;
        Description = description;
        Minister = minister;
        Date = date;
        Time = time;
        Location = location;
        Link = link;
    }

    public void AddParticipants(User user)
    {
        _participants.Add(user);
    }

    public void RemoveParticipants(User user)
    {
        _participants.Remove(user);
    }

    public void AddTag(Tag tag)
    {
        Tags.Add(tag);
    }

    public void RemoveTag(Tag tag)
    {
        Tags.Remove(tag);
    }
}