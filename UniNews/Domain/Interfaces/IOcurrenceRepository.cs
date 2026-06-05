using Uninews.Domain.Entities.Ocurrences;

namespace Uninews.Domain.Interfaces;

public interface IOcurrenceRepository
{
    Task AddAsync(Ocurrence ocurrence);
    Task<Ocurrence?> GetByIdAsync(Guid id);
    Task<IEnumerable<Ocurrence>> GetAllAsync();
    Task UpdateAsync(Ocurrence ocurrence);
    Task DeleteAsync(Ocurrence ocurrence);
}