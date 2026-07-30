using Domain.Enum.Language;
using Domain.Interface.Language;
using RpgTurno.Resources;
using Service.Language;
using System.Globalization;
using System.Resources;

namespace RpgTurno.Language;

public class LanguageService : ILanguageService
{
    private readonly ResourceManager _resourceManager = Resource.ResourceManager;

    public CultureInfo CurrentCulture { get; private set; } = new("en");

    public void SetLanguage(LanguageType language)
    {
        CurrentCulture = LanguageCultureFactory.Create(language);
    }

    public string Get(string key)
    {
        return _resourceManager.GetString(key, CurrentCulture) ?? key;
    }
}
