using Uninews.Domain.Entities.Tags;
using Uninews.Domain.Entities.Users;
using Uninews.Domain.Enum;
using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;

namespace Uninews.Domain.Entities.Courses;

public sealed class Course : BaseEntity
{
    public Name Name { get; private set; } = default!;

    public Tag Tag{ get; private set; } = default!;

    private readonly List<User> _users = new();

    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    private Course(){}

    private Course(Name name) : base()
    {
        Name = name;
    }

    public static Course Create(Name name)
    {
        var course = new Course(name);

        if(name.HasErros)
            course.AddRangeNotification(name.Erros);

        return course;
    }

    public void UpdateCourse(Name name)
    {
        Name = name;
    }

    public void AddStudent(User user)
    {
        _users.Add(user);
    }

    public void RemoveStudent(User user)
    {
        _users.Remove(user);
    }
}