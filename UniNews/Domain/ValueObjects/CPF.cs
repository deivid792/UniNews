using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class CPF : Notifiable
{
    public string? Valeu { get; }

    private CPF(){}

    private CPF(string? valeu) => Valeu = valeu;

    public static CPF Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("CPF", normalized)
            .MinLength("CPF", 11, normalized)
            .MaxLength("CPF", 11, normalized);

        var cpf = new CPF(normalized);

        if (contract.HasErros)
        {
            cpf.AddRangeNotification(contract.Erros);
        }

        return cpf;
    }
}