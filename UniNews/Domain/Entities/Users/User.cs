using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;
using Uninews.Domain.Entities.UnitNews;
using Uninews.Domain.Entities.Ocurrences;
using Uninews.Domain.Entities.Courses;
using Uninews.Domain.Entities.Tags;


namespace Uninews.Domain.Entities.Users;

public sealed class User: BaseEntity
{
    public Name Name { get; private set; } = default!;
    public Email Email{ get; private set; } = default!;
    public Password Password{ get; private set; } = default!;
    public CPF CPF{ get; private set; } = default!;

    public Course? Course{ get; private set; } = default!;

    public List<Ocurrence> Ocurrences { get; set; } = new();
    public List<Role> Roles { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
    public List<News> News { get; set; } = new();
    public List<RefreshToken> RefreshTokens { get; set; } = new();

    private User(){}

    private User(Name name, Email email, Password password, CPF cpf, Course? course) : base()
    {
        Name = name;
        Email = email;
        Password = password;
        CPF= cpf;
        Course = course;
    }

    public static User Create(Name name, Email email, Password password, CPF cpf, Course? course)
    {
        var user = new User(name, email, password, cpf, course);

        if(name.HasErros)
            user.AddRangeNotification(name.Erros);

        if(email.HasErros)
            user.AddRangeNotification(email.Erros);
        
        if(password.HasErros)
            user.AddRangeNotification(password.Erros);
        
        if(cpf.HasErros)
            user.AddRangeNotification(cpf.Erros);

        return user;
    }

    public void UpdateUser(Name name, Email email, Password password, CPF cpf)
    {
        Name = name;
        Email = email;
        Password = password;
        CPF= cpf;
    }

    public void AddOcurrence(Ocurrence occurrence)
    {
        Ocurrences.Add(occurrence);
    }

    public void RemoveOccurrence(Ocurrence occurrence)
    {
        Ocurrences.Remove(occurrence);
    }

    public void AddRole(Role role)
    {
        Roles.Add(role);
    }

    public void RemoveRole(Role role)
    {
        Roles.Remove(role);
    }

    public void AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshTokens.Add(refreshToken);
    }

    public void RemoveRefreshToken(RefreshToken refreshToken)
    {
        RefreshTokens.Remove(refreshToken);
    }

    public void AddNews(News news)
    {
        News.Add(news);
    }

    public void RemoveNews(News news)
    {
        News.Remove(news);
    }

    public void AddTag(Tag tag)
    {
        Tags.Add(tag);
    }

    public void AddTagsList(IEnumerable<Tag> tag)
    {
        Tags.AddRange(tag);
    }

    public void RemoveTags(Tag tag)
    {
        Tags.Remove(tag);
    }

}