using Uninews.Domain.Shared;

namespace Uninews.Domain.ValueObjects;

public sealed class Category : Notifiable
{
    public string? Value { get; }

    private Category(){}

    private Category (string value) => Value = value;

    public static Category Create(string? value)
    {
        var normalized = value?.Trim() ?? string.Empty;

        var contract = new Contract()
            .Requires()
            .IsNotNullOrWhiteSpace("Category",normalized)
            .MinLength("Category", 3, normalized)
            .MaxLength("Category", 100, normalized);

        var category = new Category(normalized);

        if (contract.HasErros)
        {
            category.AddRangeNotification(contract.Erros);
        }

        return category;
    }
}