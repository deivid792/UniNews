using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class Password : Notifiable
{
    public string? Value { get; }

    private Password(){}

    private Password(string? value) => Value = value;

    public static Password Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
          .Requires()
          .IsNotNullOrWhiteSpace("Password",normalized)
          .MinLength("Password", 6, normalized)
          .MaxLength("Password", 12, normalized)
          .IsStrongPassword(normalized, "Password");

          var password = new Password(normalized);

        if (contract.HasErros)
        {
            password.AddRangeNotification(contract.Erros);
        }
        return password;
    }
}