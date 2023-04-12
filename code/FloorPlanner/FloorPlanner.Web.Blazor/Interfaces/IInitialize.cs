namespace FloorPlanner.Web.Blazor.Interfaces;

public interface IInitialize
{
    Task InitializeAsync(CancellationToken cancellationToken);
}