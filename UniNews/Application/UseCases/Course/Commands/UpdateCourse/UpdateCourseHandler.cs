using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;
using Uninews.Domain.ValueObjects;

namespace Uninews.Application.UseCases.Courses.Commands.UpdateCourse;

public class UpdateCourseHandler : IUpdateCourseHandler
{
    private readonly ICourseRepository _repository;

    public UpdateCourseHandler(ICourseRepository repository) => _repository = repository;

    public async Task<Result<CourseResponseDto>> Handle(UpdateCourseCommand command)
    {
        var course = await _repository.GetByIdAsync(command.Id);
        if (course == null) return Result<CourseResponseDto>.Fail("Curso não encontrado.");

        var name = Name.Create(command.Name);
        if (name.HasErros) return Result<CourseResponseDto>.Fail(name.Erros);

        course.UpdateCourse(name);
        await _repository.UpdateAsync(course);

        var response = new CourseResponseDto() {
            Id = course.Id.ToString(),
            Name = course.Name.Value!
        };

        return Result<CourseResponseDto>.Success(response);
    }
}