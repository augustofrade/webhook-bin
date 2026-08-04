using WebhookBin.Domain.BinRequests;

namespace WebhookBin.App.Shared.Bins.Dtos;

public record ListBinRequestDto(string RawOrigin, BinRequestMethod Method, DateTimeOffset ReceivedAt);