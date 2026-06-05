using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Courses.Queries.GetAllCourses;

public interface IGetAllCoursesHandler
{
    Task<Result<IEnumerable<CourseResponseDto>>> HandleAsync();
}