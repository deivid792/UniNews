namespace Uninews.Application.UseCases.Users.Commands.UpdateUser;

public class UpdateUserCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string CPF { get; set; } = default!;
}