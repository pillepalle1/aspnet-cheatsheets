namespace MediatR.MinimalApi.Notification;
public class QueryNotification : INotification
{
    public string? Query { get; set; }
}