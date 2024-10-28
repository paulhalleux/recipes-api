using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Application.Entities;

namespace Recipes.Infrastructure.Database.Configurations;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Type).IsRequired().HasConversion<int>();
        
        builder.Property(x => x.Created).IsRequired();
        builder.Property(x => x.LastModified).IsRequired();
    }
}