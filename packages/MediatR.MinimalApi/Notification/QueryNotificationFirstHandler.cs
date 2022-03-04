namespace MediatR.MinimalApi.Notification;
internal class QueryNotificationFirstHandler : INotificationHandler<QueryNotification>
{
    private readonly ILogger<QueryNotificationFirstHandler> _logger;

    public QueryNotificationFirstHandler(ILogger<QueryNotificationFirstHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(QueryNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(QueryNotificationFirstHandler)} saw: {notification.Query}");

        return Task.CompletedTask;
    }
}