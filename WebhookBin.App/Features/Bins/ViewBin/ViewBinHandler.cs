using Microsoft.EntityFrameworkCore;
using WebhookBin.App.Shared.Bins.Dtos;
using WebhookBin.App.Shared.Queries;
using WebhookBin.Domain.Bins;
using WebhookBin.Domain.Common;
using WebhookBin.Infrastructure.Persistence;

namespace WebhookBin.App.Features.Bins.ViewBin;

public class ViewBinHandler(ApplicationDbContext dbContext) : IQueryHandler<ViewBinQuery, BinDetailsDto>
{
    public async Task<Result<BinDetailsDto>> Handle(ViewBinQuery query, CancellationToken ct = default)
    {
        var bin = await dbContext.Bins
            .AsNoTracking()
            .Where(b => b.PublicId == query.PublicId)
            .Select(b => new BinDetailsDto(b.PublicId.Value,
                b.Name,
                b.Requests.Select(br => new ListBinRequestDto(br.Origin.Raw, br.Method, br.ReceivedAt))
                    .ToList()
            ))
            .FirstOrDefaultAsync(ct);
        
        if (bin is null)
            return BinErrors.NotFound;


        return bin;
    }
}