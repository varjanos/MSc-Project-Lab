namespace FloorPlanner.Web.Blazor.Services.Progress;

public interface IProgressClientService
{
    event Action OnShow;
    event Action OnHide;
    void Show();
    void Hide();
}