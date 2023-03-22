using FloorPlanner.Web.Blazor.Client;
using FloorPlanner.Web.Blazor.Interfaces;

namespace FloorPlanner.Web.Blazor.Services.Translations
{
    public interface ITranslationClientService : IInitialize
    {
        event EventHandler OnTranslationLoad;
        string GetCurrentLanguage();
        void LanguageChanged(string language, CancellationToken cancellationToken);
        string this[string key, params object[] args] { get; }
        IList<LanguageDto> GetSupportedLanguages();
        void TranslationUpdated(TranslationDto updatedTranslation);
    }
}