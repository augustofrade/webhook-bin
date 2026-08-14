using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebhookBin.App.Public.Endpoints;
using WebhookBin.App.Shared.Bins.Notifications;
using WebhookBin.Domain.Bins;

namespace WebhookBin.App.Public.Features.BinRequests;

public static class BinRequestListener
{
    public sealed record Response(Guid binId);
    
    private static string[] AllowedHttpMethods =
    [
        HttpMethods.Get,
        HttpMethods.Post,
        HttpMethods.Put,
        HttpMethods.Delete,
        HttpMethods.Patch,
        HttpMethods.Head,
        HttpMethods.Options,
        HttpMethods.Trace
    ];
    
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder builder)
        {
            builder.MapMethods("/i/{binId:guid}", AllowedHttpMethods, Handler);
        }
    }

    public static async Task<Results<Ok<Response>, NotFound>> Handler([FromRoute] Guid binId, BinRequestNotifier binRequestNotifier)
    {
        await binRequestNotifier.NotifyReceivedRequest(binId);
        
        return TypedResults.Ok(new Response(binId));
    }
}