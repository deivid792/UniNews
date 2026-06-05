using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Entities.Courses;
using Uninews.Domain.Entities.Users;
using Uninews.Domain.Interfaces;
using Uninews.Domain.Services;
using Uninews.Domain.ValueObjects;
using Uninews.Infrastructure.Services;


namespace Uninews.Application.UseCases.Users.Commands.CreateUser;

public class CreateUserHandler : IcreateUserHandler
{
    private readonly IUserRepository _UserRepository;
    private readonly IPasswordService _PasswordService;

    public CreateUserHandler(IUserRepository userRepository, IPasswordService passwordService)
    {
        _UserRepository = userRepository;
         _PasswordService = passwordService;
    }

    public async Task<Result<UserResponseDto>> Handle(CreateUserCommand command)
    {
        var name = Name.Create(command.Name);
        if(name.HasErros)
            return Result<UserResponseDto>.Fail(name.Erros);
        
        var cpf = CPF.Create(command.CPF);
        if(cpf.HasErros)
            return Result<UserResponseDto>.Fail(cpf.Erros);

        var email = Email.Create(command.Email);
        if(email.HasErros)
            return Result<UserResponseDto>.Fail(email.Erros);
        
        var password = Password.Create(command.Password);
        if(password.HasErros)
            return Result<UserResponseDto>.Fail(password.Erros);
            var hashResult = _PasswordService.Hash(password.Value!);
            password = hashResult;

        Course? course = null;
        if (!string.IsNullOrWhiteSpace(command.Course))
        {
        var nameCourse = Name.Create(command.Course);
        var courseCreate = Course.Create(nameCourse);
        if(nameCourse.HasErros)
            return Result<UserResponseDto>.Fail(courseCreate.Name.Erros);
        }

        var user = User.Create(
            name,
            email,
            password,
            cpf,
            course
        );
        
        if (user.HasErros)
            return Result<UserResponseDto>.Fail(user.Erros);

        await _UserRepository.AddAsync(user);

        var response = new UserResponseDto()
        {
            ID = user.Id.ToString(),
            Name = name.Value!,
            Email = email.Value!
        };

        return Result<UserResponseDto>.Success(response);
    }

}