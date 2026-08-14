using WebhookBin.Domain.BinRequests;

namespace WebhookBin.App.Shared.Bins.Dtos;

public record ListBinRequestDto(string? RawSource, BinRequestMethod Method, DateTimeOffset ReceivedAt);