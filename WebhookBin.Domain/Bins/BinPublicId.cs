namespace WebhookBin.Domain.Bins;

public record BinPublicId(Guid Value)
{
    public static BinPublicId New()
    {
        return new BinPublicId(Guid.NewGuid());
    }
}