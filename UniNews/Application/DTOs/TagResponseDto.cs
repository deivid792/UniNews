namespace Uninews.Application.DTOs;

public class TagResponseDto
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public List<string> Courses { get; set; } = new();
}