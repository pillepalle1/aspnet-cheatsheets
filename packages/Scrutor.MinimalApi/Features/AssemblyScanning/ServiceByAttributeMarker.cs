namespace Scrutor.MinimalApi.Features.AssemblyScanning;

public interface IServiceByAttributeMarker
{
    
}

[ServiceMarker]
public class ServiceRegisteredByAttributeMarker : IServiceByAttributeMarker
{
    
}

[AttributeUsage(AttributeTargets.Class)]
public class ServiceMarkerAttribute : Attribute
{
    
}