using Domain.Enum.Language;

namespace Domain.Interface.Language;

public interface ILanguageService
{
    void SetLanguage(LanguageType language);
    string Get(string key);
}
