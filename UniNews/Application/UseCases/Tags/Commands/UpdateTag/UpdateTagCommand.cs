namespace Uninews.Application.UseCases.Tags.Commands.UpdateTag;
public class UpdateTagCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}