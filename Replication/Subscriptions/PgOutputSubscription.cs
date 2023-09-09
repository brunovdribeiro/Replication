using Npgsql.Replication;
using Npgsql.Replication.PgOutput;
using Npgsql.Replication.PgOutput.Messages;
using Replication.Connections;
using Replication.Subscriptions.Interfaces;
using Replication.Subscriptions.Settings;

namespace Replication.Subscriptions;

public class PgOutputSubscription : ISubscription
{
    private readonly IReplicationConnection _replicationConnection;

    public PgOutputSubscription(IReplicationConnection replicationConnection)
    {
        _replicationConnection = replicationConnection;
    }

    public async IAsyncEnumerable<object> Subscribe(SubscriptionSettings settings, CancellationToken cancellationToken)
    {
        await _replicationConnection.OpenAsync(cancellationToken);

        var slot = new PgOutputReplicationSlot(settings.Slot);


        await using var conn = new LogicalReplicationConnection("<connection_string>");

        await foreach (var message in
                       _replicationConnection.StartAsync(slot, new PgOutputReplicationOptions(settings.Publication, 1),
                           cancellationToken))
        {
            if (message is InsertMessage insertMessage)
            {
                yield return null;
            }

            conn.SetReplicationStatus(message.WalEnd);
            await conn.SendStatusUpdate(cancellationToken);
        }
    }
}