using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Application.Interfaces.Services;

namespace Recipes.Endpoints.Resources;

public class GetResourceFileByIdEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/resources/{id:guid}/file", Handler)
            .WithName("GetResourceFileById")
            .WithSummary("Retrieves a resource file by its id")
            .WithTags("Resources")
            .Accepts<FileResult>("*/*");
    }

    private static async Task<IResult> Handler(Guid id, IResourcesRepository resourcesRepository,
        IFileService fileService)
    {
        var resource = await resourcesRepository.GetByIdAsync(id);
        if (resource == null)
        {
            return TypedResults.NotFound();
        }

        var file = fileService.GetFile(resource.Name, resource.Path);
        return TypedResults.File(file, resource.ContentType, resource.Name);
    }
}