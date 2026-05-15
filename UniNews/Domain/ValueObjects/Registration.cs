using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class Registration : Notifiable
{
    public string?  Value { get;  }

    private Registration(){}

    private Registration(string? value) => Value = value;

    public static Registration Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("Registration",normalized);

        var registration = new Registration(normalized);

        if (contract.HasErros)
        {
            registration.AddRangeNotification(contract.Erros);
        }

        return registration;
    }
}