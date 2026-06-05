using Uninews.Domain.Services;
using Uninews.Domain.ValueObjects;

namespace Uninews.Infrastructure.Services;

public class PasswordService : IPasswordService
    {
        public Password Hash(string plainPassword)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            var passwordHash = Password.Create(hash);
            passwordHash.Clear();
            return passwordHash;
        }

        public bool Verify(Password storedPassword, string passwordToCheck)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToCheck, storedPassword.Value);
        }
    }