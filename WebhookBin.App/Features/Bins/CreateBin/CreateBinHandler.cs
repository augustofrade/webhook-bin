using WebhookBin.App.Shared.Bins.Dtos;
using WebhookBin.App.Shared.Commands;
using WebhookBin.Domain.Bins;
using WebhookBin.Domain.Common;
using WebhookBin.Infrastructure.Persistence;

namespace WebhookBin.App.Features.Bins.CreateBin;

public class CreateBinHandler(ApplicationDbContext dbContext) : ICommandHandler<CreateBinCommand, ListBinDto>
{
    public async Task<Result<ListBinDto>> Handle(CreateBinCommand command, CancellationToken ct = default)
    {
        var newBin = Bin.Create(command.BinName);
        await dbContext.Bins.AddAsync(newBin, ct);
        await dbContext.SaveChangesAsync(ct);

        return ListBinDto.FromEntity(newBin);
    }
}