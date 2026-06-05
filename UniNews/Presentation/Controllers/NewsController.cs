using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Uninews.Application.UseCases.News.Commands.DeleteNews;
using Uninews.Application.UseCases.News.Commands.UpdateNews;
using Uninews.Application.UseCases.News.Queries.GetAllNews;
using Uninews.Application.UseCases.UnitNews.Commands.CreateNews;
using Uninews.Application.UseCases.UnitNews.Queries.GetNewsById;

namespace Uninews.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly ICreateNewsHandler _createNewsHandler;
    private readonly IUpdateNewsHandler _updateNewsHandler;
    private readonly IDeleteNewsHandler _deleteNewsHandler;
    private readonly IGetAllNewsHandler _getAllNewsHandler;
    private readonly IGetNewsByIdHandler _getNewsByIdHandler;
    

    public NewsController(ICreateNewsHandler createNewsHandler, IUpdateNewsHandler updateNewsHandler, IDeleteNewsHandler deleteNewsHandler,
        IGetAllNewsHandler getAllNewsHandler,
        IGetNewsByIdHandler getNewsByIdHandler)
    {
        _createNewsHandler = createNewsHandler;
        _updateNewsHandler = updateNewsHandler;
        _deleteNewsHandler = deleteNewsHandler;
        _getAllNewsHandler = getAllNewsHandler;
        _getNewsByIdHandler = getNewsByIdHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateNewsCommand command)
    {
        if (command == null)
        {
            return BadRequest("O corpo da requisição não pode ser vazio.");
        }

        var result = await _createNewsHandler.HandleAsync(command);

        if (result.IsSuccess)
        {
            return StatusCode(201);
        }

        return BadRequest(new { errors = result.Errors });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateNewsCommand command)
    {
        command.Id = id;
        var result = await _updateNewsHandler.Handle(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _getAllNewsHandler.HandleAsync();
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _getNewsByIdHandler.HandleAsync(id);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _deleteNewsHandler.Handle(id);
        return result.IsSuccess ? NoContent() : NotFound(result.Errors);
    }


}