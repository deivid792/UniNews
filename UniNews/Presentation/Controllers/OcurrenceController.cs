using Microsoft.AspNetCore.Mvc;
using Uninews.Application.UseCases.Ocurrences.Commands.CreateOcurrences;
using Uninews.Application.UseCases.Ocurrences.Commands.DeleteOcurrence;
using Uninews.Application.UseCases.Ocurrences.Commands.UpdateOcurrence;
using Uninews.Application.UseCases.Ocurrences.Queries.GetAllOcurrences;
using Uninews.Application.UseCases.Ocurrences.Queries.GetOcurrenceById;

namespace Uninews.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OcurrenceController : ControllerBase
{
    private readonly ICreateOcurrenceHandler _createOcurrenceHandler;
    private readonly IUpdateOcurrenceHandler _updateOcurrenceHandler;
    private readonly IDeleteOcurrenceHandler _deleteOcurrenceHandler;
    private readonly IGetAllOcurrencesHandler _getAllOcurrencesHandler;
    private readonly IGetOcurrenceByIdHandler _getOcurrenceByIdHandler;
    

    public OcurrenceController(ICreateOcurrenceHandler createOcurrenceHandler, IUpdateOcurrenceHandler updateOcurrenceHandler,
    IDeleteOcurrenceHandler deleteOcurrenceHandler, IGetAllOcurrencesHandler getAllOcurrencesHandler, IGetOcurrenceByIdHandler getOcurrenceByIdHandler)
    {
        _createOcurrenceHandler = createOcurrenceHandler;
        _updateOcurrenceHandler = updateOcurrenceHandler;
        _deleteOcurrenceHandler = deleteOcurrenceHandler;
        _getAllOcurrencesHandler = getAllOcurrencesHandler;
        _getOcurrenceByIdHandler = getOcurrenceByIdHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOcurrenceCommand command)
    {
        if (command == null)
        {
            return BadRequest("O corpo da requisição não pode ser vazio.");
        }

        var result = await _createOcurrenceHandler.HandleAsync(command);

        if (result.IsSuccess)
        {
            return StatusCode(201, result.Value);
        }

        return BadRequest(new { errors = result.Errors });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateOcurrenceCommand command)
    {
        command.Id = id;
        var result = await _updateOcurrenceHandler.Handle(command);
        if (result.IsSuccess) return Ok(result.Value);
        return BadRequest(new { errors = result.Errors });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _deleteOcurrenceHandler.Handle(id);

        if (result.IsSuccess)
            return NoContent(); // 204 Sucesso na deleção

        return NotFound(new { errors = result.Errors }); // 404 caso não exista
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _getAllOcurrencesHandler.HandleAsync();

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(new { errors = result.Errors });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _getOcurrenceByIdHandler.HandleAsync(id);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(new { errors = result.Errors });
    }
}