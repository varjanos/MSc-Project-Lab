namespace FloorPlanner.Web.Blazor.Models;

public class ErrorDetails
{
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public int Status { get; set; }
    public DateTime ServerTime { get; set; }
}