using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;
using Uninews.Domain.Services;
using Uninews.Domain.ValueObjects;

namespace Uninews.Application.UseCases.Commands.Login;

public class LoginHandler : ILoginHandler
{
    private readonly IUserRepository _UserRepository;
    private readonly IPasswordService _PasswordService;

    public LoginHandler(IUserRepository userRepository, IPasswordService passwordService)
    {
        _UserRepository = userRepository;
        _PasswordService = passwordService;
    }

    public async Task<Result<UserResponseDto>> Handle(LoginCommand command)
    {
        var user = await _UserRepository.GetByEmailAsync(command.Email);
        if(user == null)
           return Result<UserResponseDto>.Fail("Credenciais inválidas.");

        var ok = _PasswordService.Verify(user.Password, command.Password);
        if (!ok)
            return Result<UserResponseDto>.Fail("Credenciais inválidas.");

        var response = new UserResponseDto()
        {
            Name = user.Name.Value!,
            Email = user.Email.Value!,
            ID = user.Id.ToString()
        };

        return Result<UserResponseDto>.Success(response);
    }
}