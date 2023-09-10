using Npgsql.Replication.PgOutput.Messages;
using Replication.Subscriptions.Mappers.Enuns;
using Replication.Subscriptions.Models;

namespace Replication.Subscriptions.Mappers;

public interface IMessageMapper
{
    string Entity { get; }
    MapperType Type { get; }
    Task<Tracking> Map(PgOutputReplicationMessage message, CancellationToken cancellationToken);
}