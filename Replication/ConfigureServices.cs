using Microsoft.Extensions.DependencyInjection;
using Replication.Connections;
using Replication.Subscriptions;
using Replication.Subscriptions.Interfaces;

namespace Replication;

public static class ConfigureServices
{
    public static IServiceCollection AddReplicationServices(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddSingleton<IReplicationConnection>(new ReplicationConnection(connectionString));
        services.AddScoped(typeof(ISubscription<>), typeof(PgOutputSubscription<>));

        return services;
    }
}