namespace Scrutor.MinimalApi.Features.ServiceDecorating;

public class ExampleServiceImpl : IExampleService
{
    public string GetGuid()
        => Guid.NewGuid().ToString();
}