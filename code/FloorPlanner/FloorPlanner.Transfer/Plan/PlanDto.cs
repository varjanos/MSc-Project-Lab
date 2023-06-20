namespace FloorPlanner.Transfer.Plan;

public class PlanDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BaseWidth { get; set; }
    public int BaseLength { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}
