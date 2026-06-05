namespace Uninews.Application.UseCases.Ocurrences.Commands.UpdateOcurrence;

public class UpdateOcurrenceCommand
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Minister { get; set; } = default!;
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string Location { get; set; } = default!;
    public string? Link { get; set; }
}