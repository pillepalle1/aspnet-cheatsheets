namespace MediatR.MinimalApi.Notification;
internal class QueryNotificationSecondHandler : INotificationHandler<QueryNotification>
{
    private readonly ILogger<QueryNotificationSecondHandler> _logger;

    public QueryNotificationSecondHandler(ILogger<QueryNotificationSecondHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(QueryNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{nameof(QueryNotificationSecondHandler)} saw: {notification.Query}");

        return Task.CompletedTask;
    }
}