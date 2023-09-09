using System.Collections;
using Replication.Subscriptions.Mappers;

namespace Replication.Subscriptions.Settings;

public record SubscriptionSettings
(
    string Slot,
    string Publication
);