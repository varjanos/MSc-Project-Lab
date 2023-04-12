using MudBlazor;

namespace FloorPlanner.Web.Blazor.Options;

public static class MudDialogOptions
{
    public static DialogOptions AuthDialogOptions = new DialogOptions {
        CloseOnEscapeKey = true, 
        MaxWidth = MaxWidth.ExtraSmall,
        FullWidth = true,
        CloseButton = true,
    };
}
