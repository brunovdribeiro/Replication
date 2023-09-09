using Replication.Subscriptions.Settings;

namespace Replication.Subscriptions.Interfaces;

public interface ISubscription
{
    IAsyncEnumerable<object> Subscribe(SubscriptionSettings settings, CancellationToken cancellationToken);
}