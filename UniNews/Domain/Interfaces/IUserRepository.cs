using Uninews.Domain.Entities.Users;

namespace Uninews.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task<User?>GetByIdAsync(Guid Id);
    Task<User?>GetByEmailAsync(string Email);
    Task DeleteAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();

}