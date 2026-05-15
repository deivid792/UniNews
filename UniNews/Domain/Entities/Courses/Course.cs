using Uninews.Domain.Entities.Users;
using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;

namespace Uninews.Domain.Entities.Courses;

public sealed class Course : BaseEntity
{
    public Name Name { get; private set; } = default!;
    public Period Period { get; private set; } = default!;
    public Class StudentClass { get; private set; } = default!;

    private readonly List<User> _users = new();

    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    private Course(Name name, Period period, Class studentClass){
        Name = name;
        Period = period;
        StudentClass = studentClass;
    }

    public static Course Create(Name name, Period period, Class studentClass)
    {
        var course = new Course(name, period, studentClass);

        if(name.HasErros)
            course.AddRangeNotification(name.Erros);

        if(period.HasErros)
            course.AddRangeNotification(period.Erros);
        
        if(studentClass.HasErros)
            course.AddRangeNotification(studentClass.Erros);

        return course;
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