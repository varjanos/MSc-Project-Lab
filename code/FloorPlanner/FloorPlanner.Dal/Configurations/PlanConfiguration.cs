using FloorPlanner.Dal.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanner.Dal.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable(nameof(Plan));

        builder.HasKey(plan => plan.Id).HasName($"PK_{nameof(Plan)}");

        builder.HasOne(plan => plan.UserProfile)
            .WithMany(up => up.Plans)
            .HasForeignKey(plan => plan.UserProfileId)
            .HasConstraintName($"Fk_{nameof(UserProfile)}_{nameof(Plan)}");
    }
}