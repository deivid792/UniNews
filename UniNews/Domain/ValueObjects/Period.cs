using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class Period : Notifiable
{
    public string? Value{ get; }

    private Period(){}

    private Period(string? value) => Value = value;

    public static Period create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("Period", normalized)
            .MaxLength("Period", 2, normalized);

        var period = new Period(normalized);

        if (contract.HasErros)
        {
            period.AddRangeNotification(contract.Erros);
        }

        return period;
    }
}