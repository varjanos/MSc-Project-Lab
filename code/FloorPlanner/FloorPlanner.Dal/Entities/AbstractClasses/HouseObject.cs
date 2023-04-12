namespace FloorPlanner.Dal.Entities.AbstractClasses;

public abstract class HouseObject : Position
{
    public int Id { get; set; }
    public int Width { get; set; }
    public int Length { get; set; }

    public int FloorId { get; set; }
    public virtual Floor Floor { get; set; }
}
