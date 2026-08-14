namespace WebhookBin.Domain.BinRequests;

public class BinRequestSource
{
    public BinRequestScheme? Scheme { get; private init; }
    public string RemoteIp { get; set; }  = string.Empty;
    public string? UserAgent { get; private init; }
    public string? Host { get; private init; }
    public string? Raw { get; private init; }
    
    private BinRequestSource() { }

    public static BinRequestSource Create(string remoteIp, string? userAgent = null, string? rawSource = null)
    {
        if (string.IsNullOrWhiteSpace(rawSource) ||
            !Uri.TryCreate(rawSource, UriKind.Absolute, out var uri))
        {
            return new BinRequestSource
            {
                RemoteIp = remoteIp,
                UserAgent =  userAgent,
                Raw = rawSource
            };
        }
        
        return new BinRequestSource()
        {
            Scheme = Enum.Parse<BinRequestScheme>(uri.Scheme, ignoreCase: true),
            RemoteIp = remoteIp,
            Host = uri.Host,
            UserAgent = userAgent,
            Raw = rawSource
        };
    }
}