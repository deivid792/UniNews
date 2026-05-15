using Uninews.Domain.Entities.Users;
using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class Email : Notifiable
{
    public string? Value{ get; }

    private Email(){}

    private Email(string? value) => Value = value;

    public static Email Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("Email",normalized);

        var email = new Email(normalized);

        if (contract.HasErros)
        {
            email.AddRangeNotification(contract.Erros);
        }
        return email;
    }
}
