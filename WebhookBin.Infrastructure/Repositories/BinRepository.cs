using Microsoft.EntityFrameworkCore;
using WebhookBin.Domain.Bins;
using WebhookBin.Infrastructure.Persistence;

namespace WebhookBin.Infrastructure.Repositories;

public interface IBinRepository
{
    Task<int?> GetBinId(BinPublicId binPublicId, CancellationToken ct = default);
}

public class BinRepository(ApplicationDbContext dbContext) : IBinRepository
{
    public Task<int?> GetBinId(BinPublicId binPublicId, CancellationToken ct = default)
    {
        return dbContext.Bins
            .Where(bin => bin.PublicId == binPublicId)
            .Select(bin => (int?)EF.Property<int>(bin, "Id"))
            .FirstOrDefaultAsync(ct);
    }
}