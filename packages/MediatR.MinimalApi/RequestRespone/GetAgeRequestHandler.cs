namespace MediatR.MinimalApi.RequestResponse;
public class GetAgeRequestHandler : IRequestHandler<GetAgeRequest, int>
{
    public async Task<int> Handle(GetAgeRequest request, CancellationToken cancellationToken)
    {
        return await Task.FromResult<int>(42);
    }
}