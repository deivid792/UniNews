using Uninews.Domain.Shared;
using Uninews.Domain.ValueObjects;

namespace Uninews.Domain.Entities.Users;

public sealed class RefreshToken: BaseEntity
{
    public string Token {get; set;} = null!;
    public DateTime ExpiryDate {get; set;}

    public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public RefreshToken(string token, DateTime expiryDate)
        {
            Id = new Guid();
            Token = token;
            ExpiryDate = expiryDate;
        }
}