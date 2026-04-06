namespace Uninews.Domain.Services;

    public interface IAuthService
{
    Task<string> GenerateTokenAsync(Guid userId, string email, IEnumerable<string> roles);
}
