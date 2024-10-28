using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Infrastructure.Database.Common;

namespace Recipes.Infrastructure.Database.Repositories;

public class IngredientsRepository(RecipesDbContext context) : BaseRepository<Ingredient>(context), IIngredientsRepository;