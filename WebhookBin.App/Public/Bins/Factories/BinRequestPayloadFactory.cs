using System.Text;
using WebhookBin.Domain.BinRequests;

namespace WebhookBin.App.Public.Bins.Factories;

public static class BinRequestPayloadFactory
{
    public static async Task<BinRequestPayload> Create(HttpContext httpContext)
    {
        using var reader = new StreamReader(
            httpContext.Request.Body,
            Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        
        if (string.IsNullOrWhiteSpace(body))
            return BinRequestPayload.CreateEmpty();
        
        var contentType = httpContext.Request.ContentType;
        var contentLength = httpContext.Request.ContentLength;
        
        return BinRequestPayload.Create(contentType, contentLength, body);
    }
}