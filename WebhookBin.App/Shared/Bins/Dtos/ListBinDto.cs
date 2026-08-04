using WebhookBin.Domain.Bins;

namespace WebhookBin.App.Shared.Bins.Dtos;

public record ListBinDto(Guid PublicId, string Name, DateTimeOffset CreatedAt, int RequestAmount = 0)
{
    public static ListBinDto FromEntity(Bin bin)
    {
        return new ListBinDto(bin.PublicId.Value, bin.Name, bin.CreatedAt);
    }
}