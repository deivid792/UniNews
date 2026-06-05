using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Uninews.Application.UseCases.Courses.Commands.CreateCourse;
using Uninews.Application.UseCases.Courses.Commands.DeleteCourse;
using Uninews.Application.UseCases.Courses.Commands.UpdateCourse;
using Uninews.Application.UseCases.Courses.Queries.GetAllCourses;
using Uninews.Application.UseCases.Courses.Queries.GetCourseById;

namespace Uninews.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICreateCourseHandler _createCourseHandler;
    private readonly IGetAllCoursesHandler _getAllCoursesHandler;
    private readonly IGetCourseByIdHandler _getCourseByIdHandler;
    private readonly IUpdateCourseHandler _updateCourseHandler;
    private readonly IDeleteCourseHandler _deleteCourseHandler;

    public CoursesController(ICreateCourseHandler createCourseHandler, IGetAllCoursesHandler getAllCoursesHandler,
        IGetCourseByIdHandler getCourseByIdHandler,IUpdateCourseHandler updateCourseHandler,    // Adicionado aqui
        IDeleteCourseHandler deleteCourseHandler)
    {
        _createCourseHandler = createCourseHandler;
        _getAllCoursesHandler = getAllCoursesHandler;
        _getCourseByIdHandler = getCourseByIdHandler;
        _updateCourseHandler = updateCourseHandler;
        _deleteCourseHandler = deleteCourseHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCourseCommand command)
    {
        if (command == null)
        {
            return BadRequest("O corpo da requisição não pode ser vazio.");
        }

        var result = await _createCourseHandler.Handle(command);

        if (result.IsSuccess)
        {
            return StatusCode(201, result.Value);
        }

        return BadRequest(new { errors = result.Errors });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _getAllCoursesHandler.HandleAsync();

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(new { errors = result.Errors });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _getCourseByIdHandler.HandleAsync(id);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(new { errors = result.Errors });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCourseCommand command)
    {
        command.Id = id;
        var result = await _updateCourseHandler.Handle(command);
    
        if (result.IsSuccess) return Ok(result.Value);
        return BadRequest(new { errors = result.Errors });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _deleteCourseHandler.Handle(id);
    
        if (result.IsSuccess) return NoContent();
        return NotFound(new { errors = result.Errors });
    }
}