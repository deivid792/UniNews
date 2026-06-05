namespace Uninews.Application.UseCases.Commands.Login;

public class LoginCommand
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}