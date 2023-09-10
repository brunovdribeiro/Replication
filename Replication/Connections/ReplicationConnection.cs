using Npgsql.Replication;
using Npgsql.Replication.PgOutput;
using Npgsql.Replication.PgOutput.Messages;
using NpgsqlTypes;

namespace Replication.Connections;

public class ReplicationConnection : IReplicationConnection
{
    private readonly LogicalReplicationConnection _connection;

    public ReplicationConnection(string connectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(connectionString));

        _connection = new LogicalReplicationConnection(connectionString);
    }

    public async Task OpenAsync(CancellationToken cancellationToken)
    {
        await _connection.Open(cancellationToken);
    }

    public IAsyncEnumerable<PgOutputReplicationMessage> StartAsync(
        PgOutputReplicationSlot slot,
        PgOutputReplicationOptions options,
        CancellationToken cancellationToken)
    {
        return _connection.StartReplication(slot, options, cancellationToken);
    }

    public void SetReplicationStatus(NpgsqlLogSequenceNumber lastAppliedAndFlushedLsn)
    {
        _connection.SetReplicationStatus(lastAppliedAndFlushedLsn);
    }

    public async Task SendStatusUpdateAsync(CancellationToken cancellationToken)
    {
        await _connection.SendStatusUpdate(cancellationToken);
    }
}