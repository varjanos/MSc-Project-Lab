namespace FloorPlanner.Dal.Entities;

public class Plan
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BaseWidth { get; set; }
    public int BaseLength { get; set; }
    public DateTimeOffset CreationDate { get; set; }

    public string UserProfileId { get; set; }
    public virtual UserProfile UserProfile { get; set; }
    public virtual ICollection<Floor> Floors { get; set; }
}
