namespace Uninews.Domain.Shared;

public abstract class Notifiable
{
    private readonly List<Notifications> _notification = new();

    public bool HasErros => _notification.Any();

    public IReadOnlyCollection<Notifications> Erros => _notification;

    public void AddNotification(string key, string message)
    {
        _notification.Add(new Notifications(key, message));
    }
    public void AddRangeNotification(IEnumerable<Notifications> notifications)
    {
        _notification.AddRange(notifications);
    }
    public void Clear()
    {
        _notification.Clear();
    }


}