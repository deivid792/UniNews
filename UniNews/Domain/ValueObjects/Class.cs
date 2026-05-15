using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class Class : Notifiable
{
    public string? Value { get; }

    private Class(){}

    private Class(string? value) => Value = value;

    public static Class Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("Class", normalized)
            .MinLength("Class", 3, normalized)
            .MaxLength("Class", 9, normalized);

        var newClass = new Class(normalized);

        if (contract.HasErros)
        {
            newClass.AddRangeNotification(contract.Erros);
        }

        return newClass;
    }
}

