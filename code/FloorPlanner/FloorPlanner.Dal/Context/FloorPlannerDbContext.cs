using FloorPlanner.Dal.Configurations;
using FloorPlanner.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanner.Dal.Context;

public class FloorPlannerDbContext : DbContext
{
    public FloorPlannerDbContext(DbContextOptions<FloorPlannerDbContext> options) : base(options){}

    public virtual DbSet<UserProfile> UserProfiles { get; set; }
    public virtual DbSet<Plan> Plans { get; set; }
    public virtual DbSet<Floor> Floors { get; set; }
    public virtual DbSet<Furniture> Furnitures { get; set; }
    public virtual DbSet<BuildingObject> BuildingObjects { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new PlanConfiguration());
        modelBuilder.ApplyConfiguration(new FloorConfiguration());
        modelBuilder.ApplyConfiguration(new FurnitureConfiguration());
        modelBuilder.ApplyConfiguration(new BuildingObjectConfiguration());
    }
}