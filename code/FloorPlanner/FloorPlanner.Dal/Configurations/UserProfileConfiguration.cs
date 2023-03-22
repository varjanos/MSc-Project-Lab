using FloorPlanner.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FloorPlanner.Dal.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasIndex(u => new { u.Domain, u.UserName }).IsUnique();

        builder.Property(u => u.Domain).HasMaxLength(UserProfile.DomainMaxLength);
        builder.Property(u => u.UserName).HasMaxLength(UserProfile.UserNameMaxLength);
    }
}