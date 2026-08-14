using Microsoft.EntityFrameworkCore;
using WebhookBin.App.Shared.Bins.Dtos;
using WebhookBin.App.Shared.Queries;
using WebhookBin.Domain.Common;
using WebhookBin.Infrastructure.Persistence;

namespace WebhookBin.App.Features.Bins.ListBins;

public class ListBinsHandler(ApplicationDbContext dbContext) : IQueryHandler<ListBinsQuery, List<ListBinDto>>
{
    public async Task<Result<List<ListBinDto>>> Handle(ListBinsQuery query, CancellationToken ct = default)
    {
        var bins = await dbContext.Bins
            .Select(bin => new ListBinDto(bin.PublicId.Value, bin.Name, bin.CreatedAt, bin.Requests.Count))
            .ToListAsync(ct);
        
        return bins;
    }
}