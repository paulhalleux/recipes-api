using System.Reflection;

namespace Recipes.Endpoints;

public static class EndpointsExtension
{
    public static void AddEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IEndpoint)));

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type) as IEndpoint;
            instance?.Map(endpointRouteBuilder);
        }
    }
}