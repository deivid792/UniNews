using Uninews.Application.Shared.Result;

namespace Uninews.Application.UseCases.Courses.Commands.DeleteCourse;

public interface IDeleteCourseHandler
{
    Task<Result> Handle(Guid id);
}