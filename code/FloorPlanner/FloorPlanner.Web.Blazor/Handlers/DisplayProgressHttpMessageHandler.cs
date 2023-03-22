using FloorPlanner.Web.Blazor.Services.Progress;

namespace FloorPlanner.Web.Blazor.Handlers;

public class DisplayProgressHttpMessageHandler : DelegatingHandler
{
    private readonly IProgressClientService _progressService;

    public DisplayProgressHttpMessageHandler(IProgressClientService? progressService)
        => _progressService = progressService ?? throw new ArgumentNullException(nameof(progressService));

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        cancellationToken.Register(() => _progressService.Hide());

        _progressService.Show();
        var response = await base.SendAsync(request, cancellationToken);
        _progressService.Hide();

        return response;
    }
}