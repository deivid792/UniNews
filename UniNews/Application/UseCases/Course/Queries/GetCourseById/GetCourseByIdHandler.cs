using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Courses.Queries.GetCourseById;

public sealed class GetCourseByIdHandler : IGetCourseByIdHandler
{
    private readonly ICourseRepository _repository;

    public GetCourseByIdHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CourseResponseDto>> HandleAsync(Guid id)
    {
        var course = await _repository.GetByIdAsync(id);

        if (course == null)
        {
            return Result<CourseResponseDto>.Fail("Curso não encontrado.");
        }

        var response = new CourseResponseDto()
        {
            Id = course.Id.ToString(),
            Name = course.Name.Value!
        };

        return Result<CourseResponseDto>.Success(response);
    }
}