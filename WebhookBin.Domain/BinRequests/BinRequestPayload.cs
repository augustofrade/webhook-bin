namespace WebhookBin.Domain.BinRequests;

public class BinRequestPayload
{
    public string? ContentType { get; private init; }
    public long? ContentLength { get; private init; }
    public string? Body { get; private init; }
    
    private BinRequestPayload() {  }

    public static BinRequestPayload Create(string? contentType, long? contentLength, string? body)
    {
        return new BinRequestPayload
        {
            ContentLength = contentLength,
            ContentType = contentType,
            Body = body
        };
    }

    public static BinRequestPayload CreateEmpty()
    {
        return new BinRequestPayload();
    }
    
    public bool IsEmpty =>  ContentLength == 0 && Body == null;
}