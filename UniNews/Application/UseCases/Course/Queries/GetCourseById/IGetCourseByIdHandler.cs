using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Courses.Queries.GetCourseById;

public interface IGetCourseByIdHandler
{
    Task<Result<CourseResponseDto>> HandleAsync(Guid id);
}