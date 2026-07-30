using WebhookBin.Domain.Common;

namespace WebhookBin.Domain.Bins;

public class Bin : Entity
{
    public BinPublicId PublicId { get; private set; } = BinPublicId.New();
    
    private Bin() { }
}