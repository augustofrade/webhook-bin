using WebhookBin.Domain.Bins;

namespace WebhookBin.App.Features.Bins.ViewBin;

public record ViewBinQuery(BinPublicId PublicId)
{
    public ViewBinQuery(Guid publicId) : this(new BinPublicId(publicId)) { }
}