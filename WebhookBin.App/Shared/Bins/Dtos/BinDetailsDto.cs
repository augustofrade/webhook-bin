using WebhookBin.App.Shared.BinRequests.Dtos;
using WebhookBin.Domain.Bins;

namespace WebhookBin.App.Shared.Bins.Dtos;

public record BinDetailsDto(Guid PublicId, string Name, List<ListBinRequestDto> Requests);