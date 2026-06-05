namespace Uninews.Application.UseCases.News.Commands.UpdateNews;

public class UpdateNewsCommand
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? Link { get; set; } = default!;
}