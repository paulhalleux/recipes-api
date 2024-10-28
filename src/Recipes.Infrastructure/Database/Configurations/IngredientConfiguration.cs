using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Application.Entities;

namespace Recipes.Infrastructure.Database.Configurations;

public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Unit).HasConversion<int>();
        
        builder.HasOne(x => x.Image).WithOne().HasForeignKey<Ingredient>(x => x.ImageId);
        
        builder.Property(x => x.Created).IsRequired();
        builder.Property(x => x.LastModified).IsRequired();
    }
}