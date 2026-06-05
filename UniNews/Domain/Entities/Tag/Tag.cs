using Uninews.Domain.Entities.Courses;
using Uninews.Domain.Entities.Ocurrences;
using Uninews.Domain.Entities.UnitNews;
using Uninews.Domain.Entities.Users;
using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;

namespace Uninews.Domain.Entities.Tags;

public class Tag : BaseEntity
{
    public Name Name { get; private set; } = default!;
    public Description Description { get; private set;} = default!;

    private readonly List<Course> _courses = new();
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

    public List<Ocurrence> Ocurrences { get; set; } = new();
    public List<User> Users { get; set; } = new();

    private readonly List<News> _news = new();
    public IReadOnlyCollection<News> News => _news.AsReadOnly();
    private Tag(){}

    private Tag(Name name, Description description, List<Course> courses) : base()
    {
        Name = name;
        Description = description;
        _courses = courses;
    }

    public static Tag Create(Name name, Description description, List<Course> courses)
    {
        var tag = new Tag(name, description, courses);

        if(name.HasErros)
            tag.AddRangeNotification(name.Erros);
        
        if(description.HasErros)
            tag.AddRangeNotification(description.Erros);
        
        if (courses == null || !courses.Any())
            tag.AddNotification("Tag", "A lista de cursos não pode estar vazia");

        return tag;
    }

    public void UpdateTag(Name name, Description description)
    {
        Name = name;
        Description = description;
    }

    public void AddCourses(Course course)
    {
        _courses.Add(course);
    }

    public void RemoveCourses(Course course)
    {
        _courses.Remove(course);
    }

    public void AddOcurrence(Ocurrence occurrence)
    {
        Ocurrences.Add(occurrence);
    }

    public void RemoveOccurrence(Ocurrence occurrence)
    {
        Ocurrences.Remove(occurrence);
    }

    public void AddNews(News news)
    {
        _news.Add(news);
    }

    public void RemoveNews(News news)
    {
        _news.Remove(news);
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void RemoveUser(User user)
    {
        Users.Remove(user);
    }

}