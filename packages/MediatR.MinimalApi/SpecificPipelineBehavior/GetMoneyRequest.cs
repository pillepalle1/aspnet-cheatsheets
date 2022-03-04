namespace MediatR.MinimalApi.SpecificPipelineBehavior;
public class GetMoneyRequest : IRequest<GetMoneyResponse>
{
    public double Amount { get; set; }
}