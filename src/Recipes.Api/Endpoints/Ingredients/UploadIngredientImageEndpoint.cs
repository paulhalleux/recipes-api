using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Recipes.Application.Entities;
using Recipes.Application.Enums;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Application.Interfaces.Services;

namespace Recipes.Endpoints.Ingredients;

public class UploadIngredientImageEndpoint : IEndpoint
{
    private const string ResourceFolder = "ingredients";
    
    public void Map(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/ingredients/{id:guid}/image", Handler)
            .Accepts<IFormFile>("multipart/form-data")
            .WithSummary("Uploads an image for an ingredient.")
            .WithTags("Ingredients")
            .DisableAntiforgery();
    }

    [Consumes("multipart/form-data")]
    [IgnoreAntiforgeryToken]
    private static async Task<Results<NotFound, Ok>> Handler(
        [FromForm] IFormFile file,
        [FromRoute] Guid id,
        [FromServices] IIngredientsRepository ingredientsRepository,
        [FromServices] IResourcesRepository resourcesRepository,
        [FromServices] IFileService fileService)
    {
        var ingredient = await ingredientsRepository.GetByIdAsync(id);
        if (ingredient == null)
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
        ingredient.SetImage(resourceId);
        
        await resourcesRepository.SaveChangesAsync();

        return TypedResults.Ok();
    }
}