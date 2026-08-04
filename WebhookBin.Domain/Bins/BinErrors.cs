using WebhookBin.Domain.Common;

namespace WebhookBin.Domain.Bins;

public static class BinErrors
{
    public static Error NotFound => new Error("Bin.NotFound", "Bin Not Found");
}