using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class Location : Notifiable
{
    public string? Value {get; }

    private Location(){}

    private Location(string? value) => Value = value;

    public static Location Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("Location", normalized)
            .MinLength("Location", 5, normalized)
            .MaxLength("Location", 20, normalized);

        var location = new Location(normalized);

        if (contract.HasErros)
        {
            location.AddRangeNotification(contract.Erros);
        }
        
        return location;
    }
}