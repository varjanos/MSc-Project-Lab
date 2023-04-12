using FloorPlanner.Common.Enums;
using FloorPlanner.Dal.Entities.AbstractClasses;

namespace FloorPlanner.Dal.Entities;

public class Furniture : HouseObject
{
    public string ImgPath { get; set; }
    public FurnitureType FurnitureType { get; set; }
}