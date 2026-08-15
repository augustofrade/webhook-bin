namespace WebhookBin.App.Shared.BinRequests.Dtos;

public record BinRequestPayloadDto(string? ContentType, long? ContentLength, string? Body)
{
    public bool IsEmpty => ContentType == null && ContentLength == null && Body == null;
}