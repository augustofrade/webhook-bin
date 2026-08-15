namespace WebhookBin.App.Shared.BinRequests.Dtos;

public record BinRequestPayloadDto(string? ContentType, long? ContentLength, string? Body)
{
    public bool IsEmpty =>  ContentLength == 0 && Body == null;
}