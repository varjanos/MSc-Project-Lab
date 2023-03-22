using FloorPlanner.Web.Blazor.Client;
using System.Globalization;

namespace FloorPlanner.Web.Blazor.Services.Translations
{
    public class TranslationClientService : ITranslationClientService
    {
        private readonly ITranslationClient _translationClient;
        private readonly IUserProfileClient _userProfileClient;


        public event EventHandler OnTranslationLoad;

        private string _currentLanguage = string.Empty;
        private IList<TranslationDto> _allTranslations = new List<TranslationDto>();
        private Dictionary<string, string> _translations = new Dictionary<string, string>();
        private IList<LanguageDto> _supportedLanguages = new List<LanguageDto>();

        public TranslationClientService(ITranslationClient translationClient, IUserProfileClient userProfileClient)
        {
            _translationClient = translationClient ?? throw new ArgumentNullException(nameof(translationClient));
            _userProfileClient = userProfileClient ?? throw new ArgumentNullException(nameof(userProfileClient));
        }

        public string GetCurrentLanguage() => _currentLanguage;

        public IList<LanguageDto> GetSupportedLanguages()
        {
            return _supportedLanguages;
        }

        public string this[string key, params object[] args]
        {
            get
            {
                var found = _translations.TryGetValue(key, out var translation);
                return found && !string.IsNullOrEmpty(translation)
                    ? args?.Any() ?? false
                        ? string.Format(translation, args)
                        : translation
                    : key;
            }
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            var userprofile = await _userProfileClient.GetUserProfileDtoAsync(cancellationToken);
            _currentLanguage = userprofile.Language;
            await GetTranslationsAsync(cancellationToken);
            SetUsedTranslations(_currentLanguage);
        }

        public async void LanguageChanged(string language, CancellationToken cancellationToken)
        {
            _currentLanguage = language;
            SetCurrentCulture(_currentLanguage);
            SetUsedTranslations(_currentLanguage);
            await _userProfileClient.SetUserLanguageAsync(_currentLanguage, cancellationToken);
        }

        public void TranslationUpdated(TranslationDto updatedTranslation)
        {
            var translation = _allTranslations
                .Single(t =>
                    t.Language == updatedTranslation.Language &&
                    t.Key == updatedTranslation.Key);

            translation.Text = updatedTranslation.Text;
            translation.Description = updatedTranslation.Description;

            SetUsedTranslations(_currentLanguage);
        }

        private async Task GetTranslationsAsync(CancellationToken cancellationToken)
        {
            var translations = await _translationClient.GetAllTranslationsAsync(cancellationToken);
            _allTranslations = translations.Translations;
            _supportedLanguages = translations.SupportedLanguages;
        }

        private void SetCurrentCulture(string language)
        {
            var culture = new CultureInfo(language);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        private void SetUsedTranslations(string language)
        {
            _translations = _allTranslations
                .Where(t => t.Language == language)
                .ToDictionary(t => $"{t.Key}", t => t.Text);

            OnTranslationLoad?.Invoke(this, EventArgs.Empty);
        }
    }
}