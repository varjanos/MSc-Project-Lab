namespace FloorPlanner.Web.Blazor.Models;

public class ErrorDetails
{
    public string Title { get; set; }
    public string Message { get; set; }
    public int Status { get; set; }
    public DateTime ServerTime { get; set; }
    public string TranslationKey { get; set; }
}