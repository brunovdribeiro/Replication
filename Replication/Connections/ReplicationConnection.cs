using Npgsql.Replication;
using Npgsql.Replication.PgOutput;
using Npgsql.Replication.PgOutput.Messages;

namespace Replication.Connections;

public class ReplicationConnection : IReplicationConnection
{
    private readonly LogicalReplicationConnection _connection;

    public ReplicationConnection(string connectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(connectionString));

        _connection = new LogicalReplicationConnection(connectionString);
    }

    public async Task OpenAsync(CancellationToken cancellationToken) =>
        await _connection.Open(cancellationToken);

    public IAsyncEnumerable<PgOutputReplicationMessage> StartAsync(
        PgOutputReplicationSlot slot,
        PgOutputReplicationOptions options,
        CancellationToken cancellationToken) =>
        _connection.StartReplication(slot, options, cancellationToken);
}