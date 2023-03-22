using FloorPlanner.Web.Blazor.Models;

namespace FloorPlanner.Web.Blazor.Services.DisplayError
{
    public interface IDisplayErrorClientService
    {
        event Action<ErrorDetails> OnDisplayError;
        void DisplayError(ErrorDetails errorDetails);
    }
}