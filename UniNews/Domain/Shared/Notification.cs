namespace Uninews.Domain.Shared;

public sealed class Notifications
{
    public string Key {get;}
    public string Message {get;}

    public Notifications(string key, string message)
    {
        Key = key;
        Message = message;
    }
}