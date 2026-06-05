namespace Uninews.Application.DTOs;

public class OcurrenceResponseDto
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Minister { get; set; } = default!;
    public DateOnly Date { get; set; } = default!;
    public TimeOnly Time { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string? Link { get; set; }
    public List<string> Tags { get; set; } = new();
}