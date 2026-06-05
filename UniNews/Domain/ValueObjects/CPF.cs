using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class CPF : Notifiable
{
    public string? Value { get; }

    private CPF(){}

    private CPF(string? value) => Value = value;

    public static CPF Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("CPF", normalized);

        var cpf = new CPF(normalized);

        if (contract.HasErros)
        {
            cpf.AddRangeNotification(contract.Erros);
        }

        return cpf;
    }
}