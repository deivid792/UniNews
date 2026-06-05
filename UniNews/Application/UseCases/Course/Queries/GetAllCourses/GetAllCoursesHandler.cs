using Uninews.Application.DTOs;
using Uninews.Application.Shared.Result;
using Uninews.Domain.Interfaces;

namespace Uninews.Application.UseCases.Courses.Queries.GetAllCourses;

public sealed class GetAllCoursesHandler : IGetAllCoursesHandler
{
    private readonly ICourseRepository _repository;

    public GetAllCoursesHandler(ICourseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<CourseResponseDto>>> HandleAsync()
    {
        var courses = await _repository.GetAllAsync();

        if (courses == null || !courses.Any())
        {
            return Result<IEnumerable<CourseResponseDto>>.Fail("Nenhum curso cadastrado no sistema.");
        }

        var response = courses.Select(c => new CourseResponseDto 
        {
         Id = c.Id.ToString(),
            Name = c.Name.Value!
        }).ToList();

        return Result<IEnumerable<CourseResponseDto>>.Success(response);

    }
}