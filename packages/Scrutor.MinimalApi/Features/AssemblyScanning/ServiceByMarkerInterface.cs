namespace Scrutor.MinimalApi.Features.AssemblyScanning;

public interface IServiceMarker
{
    
}

public interface IServiceByMarkerInterface
{
    
}

public class ServiceRegisteredByMarkerInterface : IServiceByMarkerInterface, IServiceMarker
{
    
}