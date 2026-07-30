using WebhookBin.Domain.Bins;

namespace WebhookBin.Domain.BinRequests;

public class BinRequest
{
    public BinRequestMethod Method { get; private init; } =  BinRequestMethod.Get;
    public DateTimeOffset ReceivedAt { get; private init; } =  DateTimeOffset.UtcNow;
    public BinRequestOrigin Origin { get; private init; }
    public BinRequestPayload Payload { get; private init; }
    
    public string? QueryString { get; private init; }
    public string? UserAgent { get; private init; }
    
    private BinRequest() { }

    public static BinRequest Create(BinRequestMethod method, DateTimeOffset receivedAt, string? queryString, string userAgent,
        BinRequestOrigin origin, BinRequestPayload payload)
    {
        return new BinRequest
        {
            Origin = origin,
            Payload = payload,
            Method = method,
            ReceivedAt = receivedAt,
            QueryString = queryString,
            UserAgent = userAgent,
        };
    }
}