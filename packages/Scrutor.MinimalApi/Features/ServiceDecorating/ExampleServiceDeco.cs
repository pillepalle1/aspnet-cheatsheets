namespace Scrutor.MinimalApi.Features.ServiceDecorating;

public class ExampleServiceDeco : IExampleService
{
    private readonly IExampleService _exampleServiceBaseImpl;

    public ExampleServiceDeco(IExampleService exampleServiceBaseImpl)
    {
        _exampleServiceBaseImpl = exampleServiceBaseImpl;
    }

    public string GetGuid()
    {
        return $"(decorated) {_exampleServiceBaseImpl.GetGuid()}";
    }
}