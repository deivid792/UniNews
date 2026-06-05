using Microsoft.EntityFrameworkCore;
using Uninews.Domain.Entities.Tags;
using Uninews.Domain.Interfaces;
using Uninews.Infrastructure.Context;

namespace Uninews.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        return await _context.Set<Tag>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Tag>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        if (ids == null || !ids.Any())
        {
            return Enumerable.Empty<Tag>();
        }

        return await _context.Set<Tag>()
            .Where(t => ids.Contains(t.Id))
            .ToListAsync();
    }

    public async Task<Tag?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Tag>()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(Tag tag)
    {
        await _context.Set<Tag>().AddAsync(tag);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tag tag)
    {
        _context.Set<Tag>().Update(tag);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tag tag)
    {
        _context.Set<Tag>().Remove(tag);
        await _context.SaveChangesAsync();
    }
}