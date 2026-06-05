using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Entities.Courses;
using Uninews.Domain.Interfaces;
using Uninews.Domain.ValueObjects;

namespace Uninews.Application.UseCases.Courses.Commands.CreateCourse;

public class CreateCourseHandler : ICreateCourseHandler
{
    private readonly ICourseRepository _repository;

    public CreateCourseHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CourseResponseDto>> Handle(CreateCourseCommand command)
    {
        var name = Name.Create(command.Name);
        
        if (name.HasErros)
            return Result<CourseResponseDto>.Fail(name.Erros);

        var course = Course.Create(name);

        if (course.HasErros)
            return Result<CourseResponseDto>.Fail(course.Erros);

        await _repository.AddAsync(course);

        var response = new CourseResponseDto
        {
            Id = course.Id.ToString(),
            Name = course.Name.Value!
        };

        return Result<CourseResponseDto>.Success(response);
    }
}