using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Application.UseCases.Courses.Commands.UpdateCourse;

namespace Uninews.Application.UseCases.Courses.Commands.UpdateCourse;

public interface IUpdateCourseHandler
{
    Task<Result<CourseResponseDto>> Handle(UpdateCourseCommand command);
}