using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Infrastructure.Database.Common;

namespace Recipes.Infrastructure.Database.Repositories;

public class ResourcesRepository(RecipesDbContext context) : BaseRepository<Resource>(context), IResourcesRepository;