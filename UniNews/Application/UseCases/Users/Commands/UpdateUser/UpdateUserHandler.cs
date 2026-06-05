using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;
using Uninews.Domain.ValueObjects;

namespace Uninews.Application.UseCases.Users.Commands.UpdateUser;

public class UpdateUserHandler : IUpdateUserHandler
{
    private readonly IUserRepository _repository;

    public UpdateUserHandler(IUserRepository repository) => _repository = repository;

    public async Task<Result<UserResponseDto>> Handle(UpdateUserCommand command)
    {
        var user = await _repository.GetByIdAsync(command.Id);
        if (user == null) 
            return Result<UserResponseDto>.Fail("Usuário não encontrado.");

        var name = Name.Create(command.Name);
        var email = Email.Create(command.Email);
        var password = Password.Create(command.Password);
        var cpf = CPF.Create(command.CPF);

        if (name.HasErros) return Result<UserResponseDto>.Fail(name.Erros);
        if (email.HasErros) return Result<UserResponseDto>.Fail(email.Erros);
        if (password.HasErros) return Result<UserResponseDto>.Fail(password.Erros);
        if (cpf.HasErros) return Result<UserResponseDto>.Fail(cpf.Erros);

        user.UpdateUser(name, email, password, cpf);
        
        await _repository.UpdateAsync(user);

        var response = new UserResponseDto
    {
        ID = user.Id.ToString(),
        Name = user.Name.Value!,
        Email = user.Email.Value!,
        CPF = user.CPF.Value
    };
        return Result<UserResponseDto>.Success(response);
    }
}