using Npgsql.Replication;
using Npgsql.Replication.PgOutput;
using Npgsql.Replication.PgOutput.Messages;
using Replication.Connections;
using Replication.Subscriptions.Interfaces;
using Replication.Subscriptions.Mappers;
using Replication.Subscriptions.Models;
using Replication.Subscriptions.Settings;

namespace Replication.Subscriptions;

public class PgOutputSubscription : ISubscription
{
    private readonly IReplicationConnection _replicationConnection;
    private readonly IEnumerable<IMessageMapper> _mappers;

    public PgOutputSubscription(
        IReplicationConnection replicationConnection,
        IEnumerable<IMessageMapper> mappers)
    {
        _replicationConnection = replicationConnection;
        _mappers = mappers;
    }

    public async IAsyncEnumerable<Task<Tracking>> Subscribe(SubscriptionSettings settings, CancellationToken cancellationToken)
    {
        await _replicationConnection.OpenAsync(cancellationToken);

        var slot = new PgOutputReplicationSlot(settings.Slot);

        var tableName = string.Empty;

        await foreach (var message in
                       _replicationConnection.StartAsync(slot, new PgOutputReplicationOptions(settings.Publication, 1),
                           cancellationToken))
        {
            switch (message)
            {
                case BeginMessage:
                    await UpdateReplicationStatus(message, cancellationToken);
                    continue;
                case RelationMessage relationMessage:
                    tableName = relationMessage.RelationName;
                    await UpdateReplicationStatus(message, cancellationToken);
                    continue;
                case CommitMessage:
                    await UpdateReplicationStatus(message, cancellationToken);
                    continue;
            }


            var messageType = message.GetType().Name;

            var mapper = _mappers.FirstOrDefault(mapper =>
                mapper.Entity == tableName &&
                mapper.Type.ToString() == messageType);

            if (mapper is null)
            {
                continue;
            }

            yield return mapper.Map(message, cancellationToken);

            await UpdateReplicationStatus(message, cancellationToken);
        }
    }

    private async Task UpdateReplicationStatus(PgOutputReplicationMessage message, CancellationToken cancellationToken)
    {
        _replicationConnection.SetReplicationStatus(message.WalEnd);
        await _replicationConnection.SendStatusUpdateAsync(cancellationToken);
    }
}