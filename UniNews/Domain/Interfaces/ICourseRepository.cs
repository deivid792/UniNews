using Uninews.Domain.Entities.Courses;

namespace Uninews.Domain.Interfaces;

public interface ICourseRepository
{
    Task AddAsync(Course course);
    Task<Course?> GetByIdAsync(Guid id);
    Task<IEnumerable<Course>> GetAllAsync();
    Task UpdateAsync(Course course);
    Task DeleteAsync(Course course);
}