namespace Uninews.Application.UseCases.Users;

public class CreateUserCommand
{
    public string Name { get; set; } = default!;
    public string CPF { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? Course { get; set; } = default!;
}