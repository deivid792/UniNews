using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;
using Uninews.Domain.Entities.UnitNews;
using Uninews.Domain.Entities.Ocurrences;
using Uninews.Domain.Entities.Courses;


namespace Uninews.Domain.Entities.Users;

public sealed class User: BaseEntity
{
    public Name Name { get; private set; } = default!;
    public Email Email{ get; private set; } = default!;
    public Password Password{ get; private set; } = default!;
    public CPF CPF{ get; private set; } = default!;
    public Registration Registration { get; private set; } = default!;

    private readonly List<Ocurrence> _occurrences = new();
    private readonly List<Role> _roles = new();
    private readonly List<RefreshToken> _refreshToken = new();
    private readonly List<News> _news = new();

    public IReadOnlyCollection<Ocurrence> Ocurrences => _occurrences.AsReadOnly();
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshToken.AsReadOnly();
    public IReadOnlyCollection<News> News=> _news.AsReadOnly();

    public Course Course{ get; set; } = null!;

}