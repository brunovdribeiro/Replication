using Npgsql.Replication.PgOutput.Messages;
using Replication.Subscriptions.Mappers.Enuns;

namespace Replication.Subscriptions.Mappers;

public interface IMessageMapper<TResult>
{
    string Entity { get; }
    MapperType Type { get; }
    Task<TResult> Map(PgOutputReplicationMessage message, CancellationToken cancellationToken);
}