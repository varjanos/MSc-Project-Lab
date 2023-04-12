using FloorPlanner.Dal.Entities.AbstractClasses;

namespace FloorPlanner.Dal.Entities;

public class Floor : Position
{
    public int Id { get; set; }
    public int PlanId { get; set; }

    public int Width { get; set; }
    public int Length { get; set; }
    public DateTimeOffset LastModificationTime { get; set; }

    public virtual Plan Plan { get; set; }
    public virtual ICollection<BuildingObject> BuildingObjects { get; set; }
    public virtual ICollection<Furniture> Furnitures { get; set; }
}
