namespace MediatR.MinimalApi.SpecificPipelineBehavior;
public class GetMoneyBehavior : IPipelineBehavior<GetMoneyRequest, GetMoneyResponse>
{
    private readonly ILogger<GetMoneyBehavior> _logger;

    public GetMoneyBehavior(ILogger<GetMoneyBehavior> logger)
    {
        _logger = logger;
    }

    public async Task<GetMoneyResponse> Handle(GetMoneyRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<GetMoneyResponse> next)
    {
        _logger.LogInformation($"{nameof(GetMoneyBehavior)}: Detected request for USD {request.Amount}");

        var result = await next();

        var approved = result.Approved ? "" : "not ";
        _logger.LogCritical($"{nameof(GetMoneyBehavior)}: Request for USD {request.Amount} was {approved}approved");

        return result;
    }
}