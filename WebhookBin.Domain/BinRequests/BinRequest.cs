using WebhookBin.Domain.Bins;

namespace WebhookBin.Domain.BinRequests;

public class BinRequest
{
    public BinRequestMethod Method { get; private init; } =  BinRequestMethod.Get;
    public DateTimeOffset ReceivedAt { get; private init; } =  DateTimeOffset.UtcNow;
    public BinRequestSource Source { get; private init; }
    public BinRequestPayload Payload { get; private init; }
    
    public string? QueryString { get; private init; }
    
    private BinRequest() { }

    public static BinRequest Create(BinRequestMethod method, DateTimeOffset receivedAt, string? queryString,
        BinRequestSource source, BinRequestPayload payload)
    {
        return new BinRequest
        {
            Source = source,
            Payload = payload,
            Method = method,
            ReceivedAt = receivedAt,
            QueryString = queryString,
        };
    }
}