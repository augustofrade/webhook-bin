namespace WebhookBin.Domain.Common;

public class Entity
{
    public DateTimeOffset CreatedAt { get; private init; } =  DateTimeOffset.UtcNow;
    public DateTimeOffset ModifiedAt { get; set; }
}