using Uninews.Domain.Entities.UnitNews;

namespace Uninews.Domain.Interfaces;

public interface INewsRepository{
    Task AddAsync(News news);
    Task<News?> GetByIdAsync(Guid id);
    Task<IEnumerable<News>> GetAllAsync();
    Task UpdateAsync(News news);
    Task DeleteAsync(News news);
}