namespace Uninews.Domain.Shared;

public abstract class BaseEntity: Notifiable
{
    public Guid Id;

    protected BaseEntity() => Id = Guid.NewGuid();
    
}