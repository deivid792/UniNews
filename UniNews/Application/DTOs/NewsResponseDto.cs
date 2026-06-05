namespace Uninews.Application.DTOs;

public class NewsResponseDto
{
    public string Id { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public string? Link { get; set; }
    public List<string> Tags { get; set; } = new();
}