using WebhookBin.Domain.Common;

namespace WebhookBin.App.Shared.Queries;

public interface IQueryHandler<in TQuery> where TQuery : class
{
    Task<Result> Handle(TQuery query, CancellationToken ct = default);
}

public interface IQueryHandler<in TQuery, TResult> where TQuery : class
{
    Task<Result<TResult>> Handle(TQuery query, CancellationToken ct = default);
}