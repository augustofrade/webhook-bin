using WebhookBin.Domain.BinRequests;
using WebhookBin.Domain.Common;

namespace WebhookBin.Domain.Bins;

public class Bin : Entity
{
    public BinPublicId PublicId { get; private set; } = BinPublicId.New();
    public string Name { get; private init; }

    private readonly List<BinRequest> _requests = [];

    public IReadOnlyCollection<BinRequest> Requests => _requests;

    private Bin() { }

    public static Bin Create(string name)
    {
        return new Bin
        {
            Name = name,
        };
    }
}