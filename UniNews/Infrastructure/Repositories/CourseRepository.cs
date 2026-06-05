using Microsoft.EntityFrameworkCore;
using Uninews.Domain.Entities.Courses;
using Uninews.Domain.Interfaces;
using Uninews.Infrastructure.Context;

namespace Uninews.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Course course)
    {
        await _context.Set<Course>().AddAsync(course);
        await _context.SaveChangesAsync();
    }

    public async Task<Course?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Course>()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Set<Course>()
            .ToListAsync();
    }

    public async Task UpdateAsync(Course course)
    {
        _context.Set<Course>().Update(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Course course)
    {
        _context.Set<Course>().Remove(course);
        await _context.SaveChangesAsync();
    }
}