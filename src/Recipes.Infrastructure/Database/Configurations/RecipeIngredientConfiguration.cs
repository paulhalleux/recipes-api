using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Application.Entities;

namespace Recipes.Infrastructure.Database.Configurations;

public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
{
    public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Quantity).IsRequired();
        
        builder.HasOne(x => x.Ingredient).WithMany().HasForeignKey(x => x.IngredientId);
        builder.HasOne(x => x.Recipe).WithMany(x => x.Ingredients).HasForeignKey(x => x.RecipeId);
        
        builder.Property(x => x.Created).IsRequired();
        builder.Property(x => x.LastModified).IsRequired();
    }
}