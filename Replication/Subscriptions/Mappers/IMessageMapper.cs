namespace Replication.Subscriptions.Mappers;

public interface IMessageMapper<TTransaction>
{
    string Entity { get; }
    TTransaction Type { get; }
    Task<object> Map(TTransaction message, CancellationToken cancellationToken);
}