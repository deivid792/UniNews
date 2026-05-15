using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;

namespace Uninews.Domain.Entities.Users;

public sealed class Role: BaseEntity
{
    public RoleName Name { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
}
