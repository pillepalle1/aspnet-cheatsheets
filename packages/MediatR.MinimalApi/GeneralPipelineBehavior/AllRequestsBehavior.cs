namespace MediatR.MinimalApi.GeneralPipelineBehavior;
public class AllRequestsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : MediatR.IRequest<TResponse>
{
    private readonly ILogger<AllRequestsBehavior<TRequest, TResponse>> _logger;

    public AllRequestsBehavior(ILogger<AllRequestsBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogInformation($"{nameof(AllRequestsBehavior<TRequest, TResponse>)} is pre-processing stuff");

        var result = await next();

        _logger.LogInformation($"{nameof(AllRequestsBehavior<TRequest, TResponse>)} is post-processing stuff");

        return result;
    }
}