using Replication.Subscriptions.Models;
using Replication.Subscriptions.Settings;

namespace Replication.Subscriptions.Interfaces;

public interface ISubscription
{
    IAsyncEnumerable<Task<Tracking>> Subscribe(SubscriptionSettings settings, CancellationToken cancellationToken);
}