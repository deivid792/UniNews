using Microsoft.AspNetCore.Mvc;
using Uninews.Application.UseCases.Tags.Commands.CreateTags;
using Uninews.Application.UseCases.Tags.Commands.UpdateTag;
using Uninews.Application.UseCases.Tags.Queries.GetAllTags;
using Uninews.Application.UseCases.Tags.Queries.GetTagById;

namespace Uninews.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private readonly IGetAllTagsHandler _getAllTagsHandler;
    private readonly ICreateTagsHandler _createTagsHandler;
    private readonly IUpdateTagHandler _updateTagHandler;
    private readonly IDeleteTagHandler _deleteTagHandler;
    private readonly IGetTagByIdHandler _getTagByIdHandler;

    public TagsController(IGetAllTagsHandler getAllTagsHandler, IGetTagByIdHandler getTagByIdHandler, ICreateTagsHandler createTagsHandler, IUpdateTagHandler update,
        IDeleteTagHandler delete)
    {
        _getAllTagsHandler = getAllTagsHandler;
        _createTagsHandler = createTagsHandler;
        _updateTagHandler = update;
        _deleteTagHandler = delete;
        _getTagByIdHandler = getTagByIdHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _getAllTagsHandler.HandleAsync();

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(new { errors = result.Value });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateTagsCommand command)
    {
        if (command == null)
        {
            return BadRequest("O corpo da requisição não pode ser vazio.");
        }

        var result = await _createTagsHandler.Handle(command);

        if (result.IsSuccess)
        {
            return StatusCode(201, result.Value);
        }

        return BadRequest(new { errors = result.Errors });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateTagCommand command)
    {
        command.Id = id;
        var result = await _updateTagHandler.Handle(command);
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _deleteTagHandler.Handle(id);
        return result.IsSuccess ? NoContent() : NotFound(result.Errors);
    }
}