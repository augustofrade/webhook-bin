using WebhookBin.App.Shared.Commands;
using WebhookBin.Domain.Bins;
using WebhookBin.Domain.Common;
using WebhookBin.Infrastructure.Persistence;

namespace WebhookBin.App.Features.Bins.CreateBin;

public class CreateBinHandler(ApplicationDbContext dbContext) : ICommandHandler<CreateBinCommand>
{
    public async Task<Result> Handle(CreateBinCommand command, CancellationToken ct = default)
    {
        await dbContext.Bins.AddAsync(Bin.Create(command.BinName), ct);
        await dbContext.SaveChangesAsync(ct);
        
        return Result.Success();
    }
}