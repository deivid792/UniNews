using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class Description : Notifiable
{
    public string? Value {get; }

    private Description(){}

    private Description(string? value) => Value = value;

    public static Description Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("Description", normalized)
            .MinLength("Description", 4, normalized)
            .MaxLength("Description", 12, normalized);

        var description = new Description(normalized);

        if (contract.HasErros)
        {
            description.AddRangeNotification(contract.Erros);
        }

        return description;
    }
    
}