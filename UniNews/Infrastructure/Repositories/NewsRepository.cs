using Microsoft.EntityFrameworkCore;
using Uninews.Domain.Entities.UnitNews;
using Uninews.Domain.Interfaces;
using Uninews.Infrastructure.Context;

namespace Uninews.Infrastructure.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly AppDbContext _context;

    public NewsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(News news)
    {
        foreach(var tag in news.Tags)
        {
        _context.Entry(tag).State = EntityState.Unchanged;
        }
    
        await _context.Set<News>().AddAsync(news);
        await _context.SaveChangesAsync();
    }

    public async Task<News?> GetByIdAsync(Guid id)
    {
        return await _context.Set<News>()
            .Include(n => n.Tags)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<IEnumerable<News>> GetAllAsync()
    {
        return await _context.Set<News>()
            .Include(n => n.Tags)
            .ToListAsync();
    }

    public async Task UpdateAsync(News news)
    {
        _context.Set<News>().Update(news);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(News news)
    {
        _context.Set<News>().Remove(news);
        await _context.SaveChangesAsync();
    }
}