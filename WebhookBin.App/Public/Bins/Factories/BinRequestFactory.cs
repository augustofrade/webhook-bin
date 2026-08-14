using WebhookBin.Domain.BinRequests;

namespace WebhookBin.App.Public.Bins.Factories;

public static class BinRequestFactory
{
    public static async Task<BinRequest> Create(HttpContext httpContext, DateTimeOffset receivedAt)
    {
        var source = BinRequestSourceFactory.Create(httpContext);
        var method = Enum.Parse<BinRequestMethod>(httpContext.Request.Method, ignoreCase: true);
        var payload = await BinRequestPayloadFactory.Create(httpContext);
        var queryString = httpContext.Request.QueryString.ToString();
        
        return BinRequest.Create(method, receivedAt, queryString, source, payload);
    }
}