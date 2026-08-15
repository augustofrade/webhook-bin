using WebhookBin.Domain.BinRequests;

namespace WebhookBin.App.Shared.BinRequests.Dtos;

public record ListBinRequestDto(
    BinRequestMethod Method,
    DateTimeOffset ReceivedAt,
    string? QueryString,
    BinRequestSourceDto Source,
    BinRequestPayloadDto Payload);