using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Application.UseCases.Courses.Commands.UpdateCourse;
using Uninews.Domain.Interfaces;
using Uninews.Domain.ValueObjects;

namespace Uninews.Application.UseCases.Courses.Commands.DeleteCourse;

public class DeleteCourseHandler : IDeleteCourseHandler
{
    private readonly ICourseRepository _repository;

    public DeleteCourseHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result> Handle(Guid id)
    {
        var course = await _repository.GetByIdAsync(id);
        if (course == null) return Result.Fail("Curso não encontrado.");

        await _repository.DeleteAsync(course);
        return Result.Success();
    }
}