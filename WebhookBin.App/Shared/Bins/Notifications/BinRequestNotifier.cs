using Microsoft.AspNetCore.SignalR;
using WebhookBin.Domain.Bins;

namespace WebhookBin.App.Shared.Bins.Notifications;

/// <summary>
/// Server-side notifier for received Bin Requests
/// </summary>
/// <param name="hubContext"></param>
public class BinRequestNotifier(IHubContext<BinsHub> hubContext, ILogger<BinRequestNotifier> logger)
{
    public async Task NotifyReceivedRequest(Guid binPublicId, CancellationToken ct = default)
    {
        var group = BinsHub.HubGroup(binPublicId);
        
        logger.LogInformation("Notifying new request to group {binHubGroup}", group);
        
        await hubContext.Clients.Group(group)
            .SendAsync(BinsHub.RequestReceived, binPublicId, cancellationToken: ct);
    }
}