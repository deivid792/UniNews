using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class RoleName : Notifiable
{
    public string? Value { get; }

    private RoleName(){}

    private RoleName(string? value) => Value = value;

    public static RoleName Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("RoleName",normalized)
            .MinLength("RoleName", 3, normalized)
            .MaxLength("RoleName", 10, normalized);

        var roleName = new RoleName(normalized);

        if (contract.HasErros)
        {
            roleName.AddRangeNotification(contract.Erros);
        }

        return roleName;
    }
}