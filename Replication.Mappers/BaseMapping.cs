using Npgsql.Replication.PgOutput;
using Npgsql.Replication.PgOutput.Messages;

namespace Replication.Mappers;

public class BaseMapping
{
    internal static async Task<string> GetIdentifier(
        ReplicationTuple message,
        RelationMessage relation,
        CancellationToken cancellationToken)
    {
        var columnIndex = 0;
        var identifier = string.Empty;

        await foreach (var value in message)
        {
            var columnName = relation.Columns[columnIndex].ColumnName;

            identifier = columnName switch
            {
                "Id" => await value.Get<string>(cancellationToken),
                _ => string.Empty
            };

            if (!string.IsNullOrWhiteSpace(identifier)) break;

            columnIndex++;
        }

        return identifier;
    }
}