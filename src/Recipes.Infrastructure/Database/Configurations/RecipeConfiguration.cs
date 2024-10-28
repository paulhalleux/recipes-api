using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Application.Entities;

namespace Recipes.Infrastructure.Database.Configurations;

public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        
        builder.HasOne(x => x.Image).WithOne().HasForeignKey<Ingredient>(x => x.ImageId);
        builder.HasMany(x => x.Ingredients).WithOne(x => x.Recipe).HasForeignKey(x => x.RecipeId);
        
        builder.Property(x => x.Created).IsRequired();
        builder.Property(x => x.LastModified).IsRequired();
    }
}