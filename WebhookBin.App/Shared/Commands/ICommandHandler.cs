using WebhookBin.Domain.Common;

namespace WebhookBin.App.Shared.Commands;

public interface ICommandHandler<in TCommand> where TCommand : class
{
    Task<Result> Handle(TCommand command, CancellationToken ct = default);
}

public interface ICommandHandler<in TCommand, TResult> where TCommand : class
{
    Task<Result<TResult>> Handle(TCommand command, CancellationToken ct = default);
}