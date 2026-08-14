using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebhookBin.App.Public.Bins.Factories;
using WebhookBin.App.Public.Endpoints;
using WebhookBin.App.Shared.Bins.Notifications;
using WebhookBin.Domain.BinRequests;

namespace WebhookBin.App.Public.Bins;

public static class BinRequestListener
{
    public sealed record Response(Guid BinId, BinRequest BinRequest);
    
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

    public static async Task<Results<Ok<Response>, NotFound>> Handler([FromRoute] Guid binId, HttpContext httpContext, BinRequestNotifier binRequestNotifier)
    {
        var binRequest = BinRequestFactory.Create(httpContext, DateTimeOffset.UtcNow);
        
        await binRequestNotifier.NotifyReceivedRequest(binId);
        
        return TypedResults.Ok(new Response(binId, binRequest));
    }

    
}