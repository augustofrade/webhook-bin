using WebhookBin.Domain.BinRequests;

namespace WebhookBin.App.Public.Bins.Factories;

public static class BinRequestPayloadFactory
{
    public static BinRequestPayload Create(HttpContext httpContext)
    {
        var body = httpContext.Request.Body.ToString();
        if (string.IsNullOrWhiteSpace(body))
            return BinRequestPayload.CreateEmpty();
        
        var contentType = httpContext.Request.ContentType;
        var contentLength = httpContext.Request.ContentLength;
        
        return BinRequestPayload.Create(contentType, contentLength, body);
    }
}