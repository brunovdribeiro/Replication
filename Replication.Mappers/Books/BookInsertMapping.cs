using Npgsql.Replication.PgOutput.Messages;
using Replication.Subscriptions.Mappers;
using Replication.Subscriptions.Mappers.Enuns;
using Replication.Subscriptions.Models;

namespace Replication.Mappers.Books;

public class BookInsertMapping : BaseMapping, IMessageMapper
{
    public string Entity => "Book";
    public MapperType Type => MapperType.InsertMessage;

    public async Task<Tracking> Map(PgOutputReplicationMessage message, CancellationToken cancellationToken)
    {
        if (message is null)
        {
            throw new Exception();
        }

        var insertMessage = message as InsertMessage;

        var identifier = await GetIdentifier(insertMessage!.NewRow, insertMessage.Relation, cancellationToken);

        return new Tracking(identifier, Entity, "Insert");
    }
}