using Replication.Subscriptions.Models;
using Replication.Subscriptions.Settings;

namespace Replication.Subscriptions.Interfaces;

public interface ISubscription<TResult>
{
    IAsyncEnumerable<Task<TResult>> Subscribe(SubscriptionSettings settings, CancellationToken cancellationToken);
}