namespace WebhookBin.App.Shared.BinRequests.Dtos;

public record BinRequestSourceDto(string RemoteIp, string? UserAgent = null, string? RawSource = null);