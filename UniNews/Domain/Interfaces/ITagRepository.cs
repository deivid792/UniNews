using Uninews.Domain.Entities.Tags;

namespace Uninews.Domain.Interfaces;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllAsync();
    Task<IEnumerable<Tag>> GetByIdsAsync(IEnumerable<Guid> ids);
    Task<Tag?> GetByIdAsync(Guid id);
    Task AddAsync(Tag tag);
    Task UpdateAsync(Tag tag);
    Task DeleteAsync(Tag tag);
}