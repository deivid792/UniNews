using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Uninews.Domain.Entities.Users;
using Uninews.Domain.Interfaces;
using Uninews.Infrastructure.Context; // Ajuste para o namespace correto do seu DbContext

namespace Uninews.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Set<User>().AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Set<User>().Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Set<User>()
            .Include(u => u.Tags)
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    public async Task<IEnumerable<User>> GetAllAsync()
{
    return await _context.Set<User>()
        .Include(u => u.Roles)
        .Include(u => u.Tags)
        .ToListAsync();
}

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Set<User>()
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Email.Value == email); // Acessa o Value do seu Value Object Email
    }
    public async Task DeleteAsync(User user)
    {
        _context.Set<User>().Remove(user);
        await _context.SaveChangesAsync();
    }
}