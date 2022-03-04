using Scrutor.MinimalApi.Features.AssemblyScanning;
using Scrutor.MinimalApi.Features.AssemblyScanning.DedicatedServiceNamespace;
using Scrutor.MinimalApi.Features.ServiceDecorating;

var builder = WebApplication.CreateBuilder(args);

// feature: assembly scanning
builder.Services.Scan(selector =>
{
    selector.FromCallingAssembly()

        // register services inside namespace 
        // downsides: difficult to coerce that only what you intend to import is imported
        .AddClasses(f => f.InNamespaces(typeof(IServiceByNamespace).Namespace!))
        .AsImplementedInterfaces()
        .WithTransientLifetime()

        // register services that follow naming convention Service : IService
        // downsides: not everything that has a matching interface name is a service
        .AddClasses()
        .AsMatchingInterface()
        .WithSingletonLifetime()

        // register services that are marked with an attribute
        // downsides: class knows about DI
        .AddClasses(f => f.WithAttribute<ServiceMarkerAttribute>())
        .AsImplementedInterfaces()
        .WithSingletonLifetime()
        
        // register services that 'implement' an interface marker
        // downsides: class knows about DI; registered even as marker interface implementation
        .AddClasses(f => f.AssignableTo<IServiceMarker>())
        .AsImplementedInterfaces()
        .WithTransientLifetime();
});

// feature: service decoration
builder.Services.AddTransient<IExampleService, ExampleServiceImpl>();
builder.Services.Decorate<IExampleService, ExampleServiceDeco>();

var app = builder.Build();

app.MapGet("/deco", (IExampleService exampleService) 
    => Results.Ok(exampleService.GetGuid()));

app.MapGet("/services", () =>
{
    // please do not shoot me for accessing  builder.Services  within the scope of the request handler
    var registeredServices = new
    {
        ByNamespace = builder.Services.ToServiceDtos<IServiceByNamespace>(),
        ByConvention = builder.Services.ToServiceDtos<IServiceByConvention>(),
        ByAttributeMarkers = builder.Services.ToServiceDtos<IServiceByAttributeMarker>(),
        ByMarkerInterface = builder.Services.ToServiceDtos<IServiceByMarkerInterface>()
    };

    return Results.Ok(registeredServices);
});

app.Run();


// ------------------------------------------------------------------------------------------------
// Nothing of interest here
internal static class HelperExtensions
{
    internal static List<object> ToServiceDtos<TService>(this IServiceCollection serviceCollection)
    {
        var t = typeof(TService);
        
        return serviceCollection
            .Where(s => s.ServiceType == t)
            .Select(s => new
            {
                Lifetime = s.Lifetime.ToString(),
                Implementation = s.ImplementationType?.ToString() ?? "<unspecified>"
            })
            .ToList<object>();
    }
}