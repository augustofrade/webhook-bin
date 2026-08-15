using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebhookBin.App.Public.Bins.Factories;
using WebhookBin.App.Public.Endpoints;
using WebhookBin.App.Shared.BinRequests.Dtos;
using WebhookBin.App.Shared.Bins.Notifications;
using WebhookBin.Domain.BinRequests;
using WebhookBin.Domain.Bins;
using WebhookBin.Infrastructure.Persistence;
using WebhookBin.Infrastructure.Repositories;

namespace WebhookBin.App.Public.Bins;

public static class BinRequestListener
{
    public sealed record Response(Guid BinPublicId, BinRequest BinRequest);
    
    private static readonly string[] AllowedHttpMethods =
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
            builder.MapMethods("/i/{binPublicId:guid}", AllowedHttpMethods, Handler);
        }
    }

    public static async Task<Results<Ok<Response>, NotFound>> Handler([FromRoute] Guid binPublicId,
        ApplicationDbContext dbContext,
        IBinRepository binRepository,
        HttpContext httpContext,
        BinRequestNotifier binRequestNotifier,
        CancellationToken ct = default)
    {
        var binRequest = await BinRequestFactory.Create(httpContext, DateTimeOffset.UtcNow);

        var binId = await binRepository.GetBinId(new BinPublicId(binPublicId), ct);
        
        if (binId == null)
            return TypedResults.NotFound();
        
        await dbContext.BinRequests.AddAsync(binRequest, ct);
        dbContext.Entry(binRequest).Property("BinId").CurrentValue = binId;
        await dbContext.SaveChangesAsync(ct);

        var requestDto = new ListBinRequestDto(binRequest.Method, binRequest.ReceivedAt, binRequest.QueryString,
            new BinRequestSourceDto(binRequest.Source.RemoteIp, binRequest.Source.UserAgent, binRequest.Source.Raw),
            new BinRequestPayloadDto(binRequest.Payload.ContentType, binRequest.Payload.ContentLength, binRequest.Payload.Body));
        
        await binRequestNotifier.NotifyReceivedRequest(binPublicId, requestDto, ct);
        
        return TypedResults.Ok(new Response(binPublicId, binRequest));
    }

    
}