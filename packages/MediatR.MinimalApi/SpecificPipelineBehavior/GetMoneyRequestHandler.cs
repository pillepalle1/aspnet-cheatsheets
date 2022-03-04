namespace MediatR.MinimalApi.SpecificPipelineBehavior;
public class GetMoneyRequestHandler : IRequestHandler<GetMoneyRequest, GetMoneyResponse>
{
    public async Task<GetMoneyResponse> Handle(GetMoneyRequest request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new GetMoneyResponse()
        {
            Approved = request.Amount < 0.0d
        });
    }
}