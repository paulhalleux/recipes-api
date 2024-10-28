using Microsoft.EntityFrameworkCore;
using Recipes.Application.Entities;
using Recipes.Infrastructure.Database.Filters;

namespace Recipes.Infrastructure.Database;

public class RecipesDbContext(DbContextOptions<RecipesDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditInterceptor());
    }

    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();
    public DbSet<Resource> Resources => Set<Resource>();
}