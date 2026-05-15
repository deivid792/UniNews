using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class Name : Notifiable
{
    public string? Value { get; }

    private Name() {}
    private Name(string? value) => Value = value;

    public static Name Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("Name",normalized)
            .MinLength(normalized,2,"Name")
            .MaxLength(normalized,100,"name");

        var name = new Name(normalized);

        if (contract.HasErros)
        {
            name.AddRangeNotification(contract.Erros);
        }
        return name;
    }
}