using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql.Replication.PgOutput;
using Npgsql.Replication.PgOutput.Messages;

namespace Replication.Connections
{
    public interface IReplicationConnection
    {
        Task OpenAsync(CancellationToken cancellationToken);

        IAsyncEnumerable<PgOutputReplicationMessage> StartAsync(PgOutputReplicationSlot slot,
            PgOutputReplicationOptions options, CancellationToken cancellationToken);
    }
}