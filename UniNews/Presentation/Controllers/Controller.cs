using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Uninews.Application.UseCases.Commands.Login;
using Uninews.Application.UseCases.Users;
using Uninews.Application.UseCases.Users.Commands.CreateUser;
using Uninews.Application.UseCases.Users.Commands.DeleteUser;
using Uninews.Application.UseCases.Users.Commands.UpdatePreferences;
using Uninews.Application.UseCases.Users.Commands.UpdateUser;
using Uninews.Application.UseCases.Users.Queries.GetAllUsers;
using Uninews.Application.UseCases.Users.Queries.GetUserById;

namespace Uninews.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IcreateUserHandler _createUserHandler;
    private readonly ILoginHandler _loginHandler;
    private readonly IUpdatePreferencesHandler _updatePreferencesHandler;
    private readonly IUpdateUserHandler _updateUserHandler;
    private readonly IDeleteUserHandler _deleteUserHandler;
    private readonly IGetAllUsersHandler _getAllUsersHandler;
    private readonly IGetUserByIdHandler _getUserByIdHandler;

    public UsersController(IcreateUserHandler createUserHandler, ILoginHandler loginHandler, IUpdatePreferencesHandler updatePreferencesHandler,
    IUpdateUserHandler updateUserHandler, IDeleteUserHandler deleteUserHandler, IGetAllUsersHandler getAllUsersHandler, IGetUserByIdHandler getUserByIdHandler)
    {
        _createUserHandler = createUserHandler;
        _loginHandler = loginHandler;
        _updatePreferencesHandler = updatePreferencesHandler;
        _updateUserHandler = updateUserHandler;
        _deleteUserHandler = deleteUserHandler;
        _getAllUsersHandler = getAllUsersHandler;
        _getUserByIdHandler = getUserByIdHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserCommand command)
    {
        if (command == null)
        {
            return BadRequest("O corpo da requisição não pode ser vazio.");
        }

        var result = await _createUserHandler.Handle(command);

        if (result.IsSuccess)
        {
            return StatusCode(201, result.Value); 
        }

        return BadRequest(new { errors = result.Errors });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
    {
        if (command == null) return BadRequest("O corpo da requisição não pode ser vazio.");

        var result = await _loginHandler.Handle(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(new { errors = result.Errors });
    }

    [HttpPut("preferences")]
    public async Task<IActionResult> UpdatePreferencesAsync([FromBody] UpdatePreferencesCommand command)
    {
        if (command == null) return BadRequest("O corpo da requisição não pode ser vazio.");

        var result = await _updatePreferencesHandler.HandleAsync(command);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return BadRequest(new { errors = result.Errors });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateUserCommand command)
    {
        command.Id = id;
        var result = await _updateUserHandler.Handle(command);

        if (result.IsSuccess)
            return Ok(result.Value); 

        return BadRequest(new { errors = result.Errors });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _getAllUsersHandler.HandleAsync();
    
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(new { errors = result.Errors });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _deleteUserHandler.Handle(id);

        if (result.IsSuccess)
            return NoContent();

        return NotFound(new { errors = result.Errors });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _getUserByIdHandler.HandleAsync(id);
    
        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(new { errors = result.Errors });
    }
}