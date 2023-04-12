using FloorPlanner.Common.Enums;
using FloorPlanner.Dal.Entities.AbstractClasses;

namespace FloorPlanner.Dal.Entities;

public class BuildingObject : HouseObject
{
    public int Height { get; set; }
    public BuildingObjectType BuildingObjectType { get; set; }
    public Direction Direction { get; set; }
}
