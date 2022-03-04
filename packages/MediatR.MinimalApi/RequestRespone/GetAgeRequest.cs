namespace MediatR.MinimalApi.RequestResponse;
public class GetAgeRequest : IRequest<int>
{
    public string? Name { get; set; }
}