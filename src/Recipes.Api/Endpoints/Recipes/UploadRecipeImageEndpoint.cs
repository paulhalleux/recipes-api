using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Entities;
using Recipes.Application.Enums;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Application.Interfaces.Services;

namespace Recipes.Endpoints.Recipes;

public class UploadRecipeImageEndpoint : IEndpoint
{
    private const string ResourceFolder = "recipes";
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/recipes/{id:guid}/image", Handler)
            .Accepts<IFormFile>("multipart/form-data")
            .WithName("UploadRecipeImage")
            .WithSummary("Uploads an image for a recipe.")
            .WithTags("Recipes")
            .DisableAntiforgery();
    }

    [Consumes("multipart/form-data")]
    [IgnoreAntiforgeryToken]
    private static async Task<Results<NotFound, Ok>> Handler(
        IFormFile file,
        [FromRoute] Guid id,
        [FromServices] IRecipesRepository recipesRepository,
        [FromServices] IResourcesRepository resourcesRepository,
        [FromServices] IFileService fileService)
    {
        var recipe = await recipesRepository.GetByIdAsync(id);
        if (recipe == null)
        {
            return TypedResults.NotFound();
        }

        var resourceId = Guid.NewGuid();
        var resourceFileName = $"{resourceId}-{file.FileName}";

        var resource = new Resource(
            resourceId,
            resourceFileName,
            ResourceFolder,
            ResourceType.Image,
            file.ContentType
        );
        
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        fileService.SaveFile(memoryStream.ToArray(), resourceFileName, ResourceFolder);
        resourcesRepository.Add(resource);
        recipe.SetImage(resourceId);
        
        await resourcesRepository.SaveChangesAsync();

        return TypedResults.Ok();
    }
}