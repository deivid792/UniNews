using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;

namespace Uninews.Domain.Entities.Users;

public sealed class Role: BaseEntity
{
    public RoleName Name { get; private set; } = default!;
    public Description Description { get; private set; } = default!;

    private Role(){}

    private readonly List<User> _users = new();

    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    private Role(RoleName name, Description description)
    {
        Name = name;
        Description = description;
    }

    public static Role Create(RoleName name, Description description)
    {
        var role = new Role(name, description);

        if (name.HasErros)
            role.AddRangeNotification(name.Erros);
        
        if(description.HasErros)
            role.AddRangeNotification(description.Erros);

        return role;
    }

    public void UpdateRole(RoleName name, Description description)
    {
        Name = name;
        Description = description;
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void RemoveUser(User user)
    {
        _users.Remove(user);
    }

}
