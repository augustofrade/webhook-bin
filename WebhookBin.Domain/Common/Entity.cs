namespace WebhookBin.Domain.Common;

public class Entity
{
    public DateTime CreatedAt { get; private init; } =  DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; }
}