using Recipes.Application.Entities;
using Recipes.Application.Interfaces.Repositories;
using Recipes.Infrastructure.Database.Common;

namespace Recipes.Infrastructure.Database.Repositories;

public class RecipeIngredientsRepository(RecipesDbContext context) : BaseRepository<RecipeIngredient>(context), IRecipeIngredientsRepository;