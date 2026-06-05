using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Courses.Commands.CreateCourse;

public interface ICreateCourseHandler
{
    Task<Result<CourseResponseDto>> Handle(CreateCourseCommand command);
}