namespace Recipes.Endpoints;

public interface IEndpoint
{
    void Map(IEndpointRouteBuilder endpoints);
}