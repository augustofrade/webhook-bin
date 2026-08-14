using WebhookBin.Domain.BinRequests;

namespace WebhookBin.App.Public.Bins.Factories;

public static class BinRequestSourceFactory
{
    public static BinRequestSource Create(HttpContext httpContext)
    {
        var remoteIp =
            httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()?.Split(',').FirstOrDefault()?.Trim()
            ?? httpContext.Request.Headers["X-Real-IP"].FirstOrDefault()
            ?? httpContext.Connection.RemoteIpAddress?.ToString()
            ?? string.Empty;

        var rawSource =
            httpContext.Request.Headers.Origin.FirstOrDefault()
            ?? httpContext.Request.Headers.Referer.FirstOrDefault();

        return BinRequestSource.Create(remoteIp, rawSource);
    }
}