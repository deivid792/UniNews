using Uninews.Domain.Enum;
using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;

namespace Uninews.Domain.Entities.Ocurrences;

public sealed class Ocurrence: BaseEntity
{
    public Title Title { get; private set; } = default!;
    public Area Area { get; private set; } = default!;
    public Category Category { get; private set; } = default!;
    public Description Description { get; private set; } = default!;
    public Name Minister { get; private set; } = default!;
    public DateOnly Date { get; private set; } = default!;
    public TimeOnly Time { get; private set; } = default!;
    public Location Location { get; private set;} = default!;
    public int Vacancies { get; private set; } = default!;
}