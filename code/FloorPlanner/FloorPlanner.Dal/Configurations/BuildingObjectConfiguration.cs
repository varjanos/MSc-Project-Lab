using FloorPlanner.Dal.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanner.Dal.Configurations;

public class BuildingObjectConfiguration : IEntityTypeConfiguration<BuildingObject>
{
    public void Configure(EntityTypeBuilder<BuildingObject> builder)
    {
        builder.ToTable(nameof(BuildingObject));

        builder.HasKey(bo => bo.Id).HasName($"PK_{nameof(BuildingObject)}");

        builder.HasOne(bo => bo.Floor)
            .WithMany(floor => floor.BuildingObjects)
            .HasForeignKey(bo => bo.FloorId)
            .HasConstraintName($"Fk_{nameof(Floor)}_{nameof(BuildingObject)}");
    }
}