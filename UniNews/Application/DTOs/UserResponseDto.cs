namespace Uninews.Application.DTOs;
public class UserResponseDto
{
    public string ID {get; set;} = null!;
    public string Name {get; set;} = null!;
    public string Email {get; set;} = null!;
    public string? CPF { get; set; } = default!;
}