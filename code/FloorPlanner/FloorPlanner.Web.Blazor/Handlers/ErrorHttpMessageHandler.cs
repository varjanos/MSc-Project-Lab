using FloorPlanner.Web.Blazor.Models;
using FloorPlanner.Web.Blazor.Services.DisplayError;
using System.Text.Json;

namespace FloorPlanner.Web.Blazor.Handlers;

public class ErrorHttpMessageHandler : DelegatingHandler
{
    private readonly IDisplayErrorClientService _displayErrorService;

    public ErrorHttpMessageHandler(IDisplayErrorClientService? displayErrorService)
        => _displayErrorService = displayErrorService ?? throw new ArgumentNullException(nameof(displayErrorService));

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            await HandleErrorAsync(response, cancellationToken);
        }

        return response;
    }

    private async Task HandleErrorAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
        var errorDetails = JsonSerializer.Deserialize<ErrorDetails>(jsonString, jsonOptions)!;

        if (string.IsNullOrEmpty(errorDetails.Message))
        {
            errorDetails = new ErrorDetails
            {
                TranslationKey = "Errors.UnexpectedError",
                Title = "Errors.UnexpectedError",
                Message = "Errors.UnexpectedError",
                ServerTime = DateTime.Now,
                Status = 503,
            };
        }

        _displayErrorService.DisplayError(errorDetails);
    }
}