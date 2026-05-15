using System.Text.RegularExpressions;

namespace Uninews.Domain.Shared;

public sealed class Contract : Notifiable
{
    public Contract Requires()
        => this;

    public Contract IsNotNull(object? value, string key)
    {
        if(value == null)
        AddNotification(key, "O valor não pode ser nulo");

        return this;
    }

    public Contract IsNotNullOrWhiteSpace(string key, string? value)
    {
        if(string.IsNullOrWhiteSpace(value))
        AddNotification(key, "O valor não pode ser nulo ou ter espaços em branco.");

        return this;
    }

    public Contract MinLength(string? value, int min, string key)
    {
        if(!string.IsNullOrEmpty(value) && value.Length < min)
            AddNotification(key, $"A quantidade mínima de caracteres é {min}");

            return this;
    }

    public Contract MaxLength(string? value, int max, string key)
    {
        if(!string.IsNullOrEmpty(value) && value.Length > max)
            AddNotification(key, $"A quantidade máxima de caracteres é {max}");

            return this;
    }

    public Contract IsStrongPassword(string value, string key)
    {
        var regex = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*(),.?"":{}|<>]).+$";

        if(!string.IsNullOrEmpty(value) && value.Length <= 20 && !Regex.IsMatch(value, regex))
           AddNotification(key,"A senha deve conter maiúsculas, minúsculas, números e caracteres especiais.");

        return this;
    }
}