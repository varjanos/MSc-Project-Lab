using FloorPlanner.Dal.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanner.Dal.Configurations;

public class FurnitureConfiguration : IEntityTypeConfiguration<Furniture>
{
    public void Configure(EntityTypeBuilder<Furniture> builder)
    {
        builder.ToTable(nameof(Furniture));

        builder.HasKey(furniture => furniture.Id).HasName($"PK_{nameof(Furniture)}");

        builder.HasOne(furniture => furniture.Floor)
            .WithMany(floor => floor.Furnitures)
            .HasForeignKey(furniture => furniture.FloorId)
            .HasConstraintName($"Fk_{nameof(Floor)}_{nameof(Furniture)}");
    }
}