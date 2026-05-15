using Uninews.Domain.Shared;

namespace Uninews.Domain.Enum;

public sealed class Area : Notifiable
{
    public string? Value { get; }


    private Area(){}
    private Area(String? value) => Value = value;
    
    public static Area Create(String? value)
    {
        string normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("Area", normalized)
            .MinLength("Area", 3, normalized)
            .MaxLength("Area", 10, normalized);

        var area = new Area(normalized);

        if (contract.HasErros)
        {
            area.AddRangeNotification(contract.Erros);
        }

        return area;

    }
}