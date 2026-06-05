using Uninews.Domain.Interfaces;
using Uninews.Domain.Entities.UnitNews;
using Uninews.Infrastructure.Context;
using Uninews.Domain.Entities.Ocurrences;
using Microsoft.EntityFrameworkCore;

namespace Uninews.Infrastructure.Repositories;

public class OcurrenceRepository : IOcurrenceRepository
{
    private readonly AppDbContext _context;

    public OcurrenceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Ocurrence ocurrence)
    {
        foreach(var tag in ocurrence.Tags)
        {
            _context.Entry(tag).State = EntityState.Unchanged;
        }

        foreach(var participant in ocurrence.Participants)
        {
            _context.Entry(participant).State = EntityState.Unchanged;
        }

        await _context.Set<Ocurrence>().AddAsync(ocurrence);
        await _context.SaveChangesAsync();
    }

    public async Task<Ocurrence?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Ocurrence>()
            .Include(o => o.Tags)
            .Include(o => o.Participants)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Ocurrence>> GetAllAsync()
    {
        return await _context.Set<Ocurrence>()
            .Include(o => o.Tags)
            .Include(o => o.Participants)
            .ToListAsync();
    }

    public async Task UpdateAsync(Ocurrence ocurrence)
    {
        _context.Set<Ocurrence>().Update(ocurrence);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Ocurrence ocurrence)
    {
        _context.Set<Ocurrence>().Remove(ocurrence);
        await _context.SaveChangesAsync();
    }
}