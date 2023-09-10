namespace Replication.Subscriptions.Models;

public class Tracking
{
    public Guid Id { get; private set; }
    public string Identifier { get; private set; }
    public string Entity { get; private set; }
    public string Action { get; private set; }
    public DateOnly CreatedDate { get; private set; }
    public TimeOnly CreatedTime { get; private set; }

    public Tracking(string identifier, string entity, string action)
    {
        var dateTime = DateTime.UtcNow;
        Id = Guid.NewGuid();
        Identifier = identifier;
        Entity = entity;
        Action = action;
        CreatedDate = DateOnly.FromDateTime(dateTime);
        CreatedTime = TimeOnly.FromDateTime(dateTime);
    }
}