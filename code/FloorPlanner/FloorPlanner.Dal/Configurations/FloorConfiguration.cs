using FloorPlanner.Dal.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanner.Dal.Configurations;

public class FloorConfiguration : IEntityTypeConfiguration<Floor>
{
    public void Configure(EntityTypeBuilder<Floor> builder)
    {
        builder.ToTable(nameof(Floor));

        builder.HasKey(floor => floor.Id).HasName($"PK_{nameof(Floor)}");

        builder.HasOne(floor => floor.Plan)
            .WithMany(plan => plan.Floors)
            .HasForeignKey(floor => floor.PlanId)
            .HasConstraintName($"Fk_{nameof(Plan)}_{nameof(Floor)}");
    }
}