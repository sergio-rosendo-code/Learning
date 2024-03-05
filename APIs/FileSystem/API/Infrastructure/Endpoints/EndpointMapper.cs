namespace API.Infrastructure.Endpoints;

public static class EndpointMapper
{
    public static void MapEndpoints(this WebApplication app)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
        var endpointClasses = assemblies
            .Distinct()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IEndpoint).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .ToList();
        
        endpointClasses.ForEach(endpointClass =>
        {
            var endpoint = Activator.CreateInstance(endpointClass) as IEndpoint;
            endpoint?.Map(app);
        });
    }
}