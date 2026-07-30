namespace WebhookBin.Domain.BinRequests;

public class BinRequestOrigin
{
    public BinRequestScheme Scheme { get; private init; } = BinRequestScheme.Http;
    public string RemoteIp { get; set; }  = string.Empty;
    public string Host { get; private init; } = string.Empty;
    public string Raw { get; private init; } = string.Empty;
    
    private BinRequestOrigin() { }

    public static BinRequestOrigin Create(string rawOrigin, string remoteIp)
    {
        var uri = new Uri(rawOrigin);
        
        return new BinRequestOrigin()
        {
            Scheme = Enum.Parse<BinRequestScheme>(uri.Scheme),
            RemoteIp = remoteIp,
            Host = uri.Host,
            Raw = rawOrigin
        };
    }
}