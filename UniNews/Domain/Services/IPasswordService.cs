using Uninews.Domain.ValueObjects;

namespace Uninews.Domain.Services;

    public interface IPasswordService
{
    bool Verify(Password storedPassword, string passwordToCheck);

    Password Hash(string plainPassword);
}
