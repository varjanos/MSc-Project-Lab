using FloorPlanner.Web.Blazor.Models;

namespace FloorPlanner.Web.Blazor.Services.DisplayError
{
    public class DisplayErrorClientService : IDisplayErrorClientService
    {
        public event Action<ErrorDetails> OnDisplayError;

        public void DisplayError(ErrorDetails errorDetails)
            => OnDisplayError?.Invoke(errorDetails);
    }
}