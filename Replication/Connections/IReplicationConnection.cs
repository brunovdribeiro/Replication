﻿using Npgsql.Replication.PgOutput;
using Npgsql.Replication.PgOutput.Messages;
using NpgsqlTypes;

namespace Replication.Connections;

public interface IReplicationConnection
{
    Task OpenAsync(CancellationToken cancellationToken);

    IAsyncEnumerable<PgOutputReplicationMessage> StartAsync(PgOutputReplicationSlot slot,
        PgOutputReplicationOptions options, CancellationToken cancellationToken);

    void SetReplicationStatus(NpgsqlLogSequenceNumber lastAppliedAndFlushedLsn);

    Task SendStatusUpdateAsync(CancellationToken cancellationToken);
}