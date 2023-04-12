namespace FloorPlanner.Web.Blazor.Services.Progress;

public class ProgressClientService : IProgressClientService
{
    public event Action OnShow;
    public event Action OnHide;

    private int _Counter { get; set; } = 0;

    public void Show()
    {
        _Counter++;
        OnShow?.Invoke();
    }

    public void Hide()
    {
        if (_Counter > 0)
            _Counter--;

        if (_Counter == 0)
            OnHide?.Invoke();
    }
}