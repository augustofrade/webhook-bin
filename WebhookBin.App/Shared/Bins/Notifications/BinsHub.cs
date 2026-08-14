using Microsoft.AspNetCore.SignalR;
using WebhookBin.Domain.Bins;

namespace WebhookBin.App.Shared.Bins.Notifications;

public sealed class BinsHub(ILogger<BinsHub> logger) : Hub
{
    public const string HubRoute = "/hub/bins";
    public const string RequestReceived = "RequestReceived";

    public static string HubGroup(Guid binPublicId) =>  $"bin:{binPublicId}";
    
    public async Task JoinBinGroup(Guid binPublicId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, HubGroup(binPublicId));
        logger.LogInformation("Connected to bins hub connection with group {binHubGroup}", HubGroup(binPublicId));
    }

    public async Task LeaveBinGroup(Guid binPublicId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, HubGroup(binPublicId));
    }
    
}