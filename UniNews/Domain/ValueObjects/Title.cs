using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class Title : Notifiable
{
    public string? Value { get; }

    private Title(){}

    private Title(string? value) => Value = value;

    public static Title Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("Title",normalized)
            .MinLength("Title", 3, normalized)
            .MaxLength("Title", 10, normalized);

        var title = new Title(normalized);

        if (contract.HasErros)
        {
            title.AddRangeNotification(contract.Erros);
        }

        return title;
    }
}